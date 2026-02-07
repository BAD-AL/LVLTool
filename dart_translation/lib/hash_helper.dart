/// Hash (FNV) and dictionary lookup for LVLTool.
/// Translated from C# CoreCode/HashHelper.cs for Linux/CLI use.

import 'dart:convert';
import 'dart:io';
import 'dictionary.dart';

import 'package:dart_translation/lvltool_config.dart';

/// FNV-1a style hash for strings (lowercased per-byte).
int hashString(String input) {
  return hashStringFromChars(input.runes.toList());
}

int hashStringFromChars(List<int> input) {
  const int fnvPrime = 16777619;
  const int offsetBasis = 2166136261;
  int hash = offsetBasis;
  for (int i = 0; i < input.length; i++) {
    int c = input[i] & 0xff;
    c |= 0x20;
    hash ^= c;
    hash = (hash * fnvPrime) & 0xFFFFFFFF;
  }
  return hash & 0xFFFFFFFF;
}

String? _dictionaryFile;
Map<int, String>? _hashes;

String get _dictionaryPath {
  if (_dictionaryFile != null) return _dictionaryFile!;
  if (File('dictionary.txt').existsSync()) {
    _dictionaryFile = 'dictionary.txt';
    return _dictionaryFile!;
  }
  // Fallback: same dir as executable (or current dir)
  _dictionaryFile = 'dictionary.txt';
  return _dictionaryFile!;
}

String? reverseLookup(int key) {
  if (_hashes == null) _readDictionary();
  return _hashes![key];
}

void addHashedString(String input) {
  if (_hashes == null) _readDictionary();
  int h = hashString(input);
  if (!_hashes!.containsKey(h)) _hashes![h] = input;
}

String? getStringFromHash(int hash) {
  if (_hashes == null) _readDictionary();
  return _hashes![hash];
}

/// Adds strings to the dictionary file. Returns count added.
int addStringsToDictionary(List<String> stringsToAdd) {
  if (_hashes == null) _readDictionary();
  int count = 0;
  final sb = StringBuffer();
  for (String tmp in stringsToAdd) {
    String str = tmp.trim();
    if (str.length > 2 && str[0] != '0' && str[1] != 'x') {  // don't add hex numbers
      int hashId = hashString(str);
      if (!_hashes!.containsKey(hashId)) {
        addHashedString(str);
        count++;
        sb.writeln(str);
      }
    }
  }
  if (sb.length > 0) {
    try {
      final f = File(_dictionaryPath);
      final sink = f.openWrite(mode: FileMode.append);
      sink.write(sb.toString());
      sink.close();
    } catch (e) {
      print('Error updating $_dictionaryPath: $e');
    }
  }
  return count;
}

void _readDictionary() {
  _hashes = <int, String>{};
  try {
    // read in all internal keys
    for (String line in embedded_dictionary.split('\n')) {
        addHashedString(line.trim());
    }
    // if we have an external dictionary, read that too
    if (File(_dictionaryPath).existsSync()) {
      print("info: Reading '$_dictionaryPath...");
      final lines = File(_dictionaryPath).readAsLinesSync();
      for (String line in lines) {
        addHashedString(line);
      }
    } 
  } catch (e) {
    print('Error processing Dictionary. linesProcessed: ${_hashes!.length} \'$e\'');
  }
}

/// Callback for appending match output (e.g. console or UI).
void Function(String s)? appendStringCallback;

void printMatches(int hash, String possibleCharacters, int targetStringSize,
    String prefix) {
  final buffer = List<int>.filled(targetStringSize, 0);
  final prefixRunes = prefix.runes.toList();
  for (int i = 0; i < prefix.length && i < targetStringSize; i++) {
    buffer[i] = prefixRunes[i];
  }
  _generateStringsAndCheckHash(
      prefix.length, targetStringSize, hash, possibleCharacters, buffer);
}

void _generateStringsAndCheckHash(int index, int targetStringSize, int targetHash,
    String possibleCharacters, List<int> bufferToUse) {
  if (index == targetStringSize) {
    if (hashStringFromChars(bufferToUse) == targetHash) {
      final str = 'Match found: ${String.fromCharCodes(bufferToUse)}\n';
      if (appendStringCallback != null) {
        appendStringCallback!(str);
      } else {
        stdout.write(str);
      }
    }
  } else {
    for (int i = 0; i < possibleCharacters.length; i++) {
      bufferToUse[index] = possibleCharacters.codeUnitAt(i);
      _generateStringsAndCheckHash(
          index + 1, targetStringSize, targetHash, possibleCharacters, bufferToUse);
    }
  }
}

void writeDictionary() {
  final sb = StringBuffer();
  for (var e in _hashes!.entries) {
    if (e.value.indexOf('.') > -1) {
      sb.writeln(e.value.toLowerCase());
    } else {
      sb.writeln(e.value);
    }
  }
  File(_dictionaryPath).writeAsStringSync(sb.toString());
}
