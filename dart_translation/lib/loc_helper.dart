/// Localization (Loc) file operations for LVLTool.
/// Translated from C# CoreCode/LocHelper.cs for Linux/CLI use.
/// Loc files: string_id_hash (4) + 2-byte size + UTF-16 string + 32-bit zero term.

import 'dart:io';
import 'dart:typed_data';

import 'bin_utils.dart';
import 'hash_helper.dart';
import 'package:dart_translation/lvltool_config.dart';

final _bodyBytes = Uint8List.fromList([0x42, 0x4f, 0x44, 0x59]);// BODY

class LocHelper {
  late List<int> _mData;
  Map<int, String>? _stringSet;
  Map<int, String>? _stringSet2;
  int _bodySizeLoc = -1;

  int get _bodySizeLocation {
    final bodyStr =  Uint8List.fromList([0x42, 0x4f, 0x44, 0x59]);
    final loc = getLocationOfGivenBytes(0x10, bodyStr, _mData, 0x30);
    _bodySizeLoc = loc + 4;
    return _bodySizeLoc;
  }

  int get _bodyEnd {
    final bodyLen = getNumberAtLocation(_bodySizeLocation, _mData);
    return _bodySizeLocation + bodyLen;
  }

  int get _bodyStart {
    final loc = getLocationOfGivenBytes(24, _bodyBytes, _mData, 20);
    return loc + 8;
  }

  /// [data] can be [Uint8List] or [Uint8List] (e.g. from [Chunk.getAssetData]).
  LocHelper(Uint8List data) {
    //_mData = Uint8List.fromList(data);
    _mData = List<int>.from(data);
  }

  Uint8List getUcfbData() {
    makeUcfb(_mData, _bodyEnd);
    return Uint8List.fromList(_mData);
  }

  Uint8List getRawData() => Uint8List.fromList(_mData);

  void _addBodySize(int size) {
    int bodySize = getNumberAtLocation(_bodySizeLocation, _mData);
    bodySize += size;
    writeNumberAtLocation(_bodySizeLocation, bodySize, _mData);
  }

  void addString(String stringId, String content) {
    int hashId = 0;
    if (stringId.length > 2 && stringId[0] == '0' && stringId[1] == 'x') {
      try {
        hashId = int.parse(stringId.substring(2), radix: 16);
      } catch (e) {
        print("Error! invalid string key:'$stringId'");
        rethrow;
      }
    } else {
      hashId = hashString(stringId);
    }
    int sz = (content.length * 2) + 2 + 4 + 4;
    bool sixZeros = false;
    if (sz % 4 != 0) {
      sz += 2;
      sixZeros = true;
    }
    if (_bodyEnd + sz > _mData.length) {
      _addBytes((_bodyEnd + sz - _mData.length).toInt());
    }
    int stringLoc = _bodyEnd - 4;
    writeNumberAtLocation(stringLoc, hashId, _mData);
    write2ByteNumberAtLocation(stringLoc + 4, sz, _mData);
    stringLoc += 6;
    for (int i = 0; i < content.length; i++) {
      write2ByteNumberAtLocation(stringLoc, content.codeUnitAt(i), _mData);
      stringLoc += 2;
    }
    writeNumberAtLocation(stringLoc, 0, _mData);
    if (sixZeros) write2ByteNumberAtLocation(stringLoc + 4, 0, _mData);
    _addBodySize(sz);
  }

  int setString(String stringId, String newValue) {
    int hashId = getHashId(stringId);
    final sb = StringBuffer();
    int stringLoc = getStringAt(hashId, sb);
    String prevValue = sb.toString();
    if (stringLoc > 0 && prevValue != newValue) {
      int oldSz = get2ByteNumberAtLocation(stringLoc + 4, _mData);
      int sz = (newValue.length * 2) + 2 + 4 + 4;
      if (sz % 4 != 0) sz += 2;
      bool sixZeros = false;
      int diff = sz - oldSz;
      if (diff % 4 != 0) {
        diff += 2;
        sixZeros = true;
      }
      int dataEnd = _mData.length + diff;
      if (diff > 0) {
        _addBytes(diff);
        _shiftDataDown(stringLoc, diff, dataEnd);
      } else if (diff < 0) {
        _shiftDataUp(stringLoc, -diff, dataEnd);
      }
      writeNumberAtLocation(stringLoc, hashId, _mData);
      write2ByteNumberAtLocation(stringLoc + 4, sz, _mData);
      stringLoc += 6;
      for (int i = 0; i < newValue.length; i++) {
        write2ByteNumberAtLocation(stringLoc, newValue.codeUnitAt(i), _mData);
        stringLoc += 2;
      }
      writeNumberAtLocation(stringLoc, 0, _mData);
      if (sixZeros) write2ByteNumberAtLocation(stringLoc + 4, 0, _mData);
      _addBodySize(diff);
    }
    return stringLoc;
  }

  void _addBytes(int numBytes) {
    for (int i = 0; i < numBytes; i++) _mData.add(0);
  }

  void setByte(int loc, int b) {
    _mData[loc] = b;
  }

  void _shiftDataDown(int startIndex, int amount, int dataEnd) {
    for (int i = dataEnd - amount; i > startIndex; i--) {
      setByte(i, _mData[i - amount]);
    }
  }

  void _shiftDataUp(int startIndex, int amount, int dataEnd) {
    for (int i = startIndex; i < dataEnd; i++) {
      setByte(i, _mData[i + amount]);
    }
  }

  String getAllStrings() {
    _stringSet = {};
    _stringSet2 = {};
    final sb = StringBuffer();
    int stringLoc = _bodyStart;
    int currentHash = 0;
    int sz = 0;
    int byteStart = 0;
    int byteEnd = 0;
    String stringId = '';
    final cur = StringBuffer();
    while ((currentHash = getNumberAtLocation(stringLoc, _mData)) > 0) {
      stringId = getStringFromHash(currentHash) ?? '0x${currentHash.toRadixString(16)}';
      sb.write(stringId);
      sb.write('="');
      sz = get2ByteNumberAtLocation(stringLoc + 4, _mData);
      byteStart = stringLoc + 6;
      byteEnd = stringLoc + sz - 1;
      cur.clear();
      for (int i = byteStart; i < byteEnd; i += 2) {
        int ch = get2ByteNumberAtLocation(i, _mData);
        if (ch == 0x22) {
          sb.write(r'\"');
          cur.write(r'\"');
        } else if (ch == 0x5c) {
          sb.write(r'\\');
          cur.write(r'\\');
        } else if (ch != 0) {
          sb.write(String.fromCharCode(ch));
          cur.write(String.fromCharCode(ch));
        }
      }
      if (currentHash != 0xffffffff) {
        if (_stringSet!.containsKey(currentHash)) {
          if (!_stringSet2!.containsKey(currentHash)) {
            _stringSet2![currentHash] = cur.toString();
          } else {
            print('Sorry, not showing 3+ instances of string 0x${currentHash.toRadixString(16)}:\'${cur.toString()}\'');
          }
        } else {
          _stringSet![currentHash] = cur.toString();
        }
      }
      sb.write('"\n');
      stringLoc = _nextStringLoc(stringLoc);
      if (stringLoc + 10 > _mData.length) break;
    }
    return sb.toString();
  }

  String getString(String stringId) {
    int hash = getHashId(stringId);
    final builder = StringBuffer();
    getStringAt(hash, builder);
    return builder.toString();
  }

  static int getHashId(String stringId) {
    int hash = 0;
    if (stringId.toLowerCase().startsWith('0x')) {
      try {
        hash = int.parse(stringId.substring(2), radix: 16);
      } catch (_) {}
    } else {
      hash = hashString(stringId);
    }
    return hash;
  }

  int getStringAt(int hashId, StringBuffer sb) {
    int stringLoc = _bodyStart;
    int occurrence = 0;
    int targetOccurrence = 1;
    if (_stringSet2 != null && _stringSet2!.containsKey(hashId)) targetOccurrence++;
    int currentHash = 0;
    while (true) {
      currentHash = getNumberAtLocation(stringLoc, _mData);
      if (currentHash == hashId) {
        occurrence++;
        if (occurrence == targetOccurrence) break;
      }
      stringLoc = _nextStringLoc(stringLoc);
      if (stringLoc + 10 > _mData.length) return -1;
    }
    int sz = get2ByteNumberAtLocation(stringLoc + 4, _mData);
    int byteStart = stringLoc + 6;
    int byteEnd = stringLoc + sz - 1;
    for (int i = byteStart; i < byteEnd; i += 2) {
      int ch = get2ByteNumberAtLocation(i, _mData);
      if (ch != 0) sb.write(String.fromCharCode(ch));
    }
    return stringLoc;
  }

  void disableString(String stringId, int targetOccurrence) {
    int hashId = getHashId(stringId);
    int location = getStringLoc(hashId, targetOccurrence);
    if (location > -1) {
      writeNumberAtLocation(location, 0xFFFFFFFF, _mData);
    } else {
      print('Could not find stringId: $stringId');
    }
  }

  int getStringLoc(int hashId, int targetOccurrence) {
    int stringLoc = _bodyStart;
    int occurrence = 0;
    int currentHash = 0;
    while (true) {
      currentHash = getNumberAtLocation(stringLoc, _mData);
      if (currentHash == hashId) {
        occurrence++;
        if (occurrence == targetOccurrence) break;
      }
      stringLoc = _nextStringLoc(stringLoc);
      if (stringLoc + 10 > _mData.length) return -1;
    }
    return stringLoc;
  }

  int _nextStringLoc(int stringLoc) {
    int sz = get2ByteNumberAtLocation(stringLoc + 4, _mData);
    return sz + stringLoc;
  }

  void saveToUcfbFile(String filename) {
    File(filename).writeAsBytesSync(getUcfbData());
  }

  static String? _getKey(int pos, String text) {
    final sb = StringBuffer();
    const skipThese = '\r\n" ';
    for (int i = pos; i < text.length; i++) {
      if (text[i] == '=') break;
      if (!skipThese.contains(text[i])) sb.write(text[i]);
    }
    return sb.toString().trim().isEmpty ? null : sb.toString().trim();
  }

  static String? _parseStringValue(int pos, String text) {
    final sb = StringBuffer();
    int index = text.indexOf('"', pos);
    String prev = '';
    if (index > -1) {
      for (int i = index + 1; i < text.length; i++) {
        if (text[i] == '"' && prev != '\\') break;
        if (text[i] == '\\' && prev == '\\') prev = ' ';
        else {
          sb.write(text[i]);
          prev = text[i];
        }
      }
      return sb.toString();
    }
    return null;
  }

  void addNewStrings(String text) {
    applyText(text, addOnly: true, skipEmptyStrings: true);
  }

  void applyText(String text, {bool addOnly = false, bool skipEmptyStrings = false}) {
    getAllStrings();
    int pos = 0;
    final stringsToAdd = <String, String>{};
    final stringsToSet = <String, String>{};
    while (pos < text.length) {
      String? key = _getKey(pos, text);
      if (key == null || key.isEmpty) {
        pos++;
        continue;
      }
      int stringId = 0;
      if (key.length > 2 && key[0] == '0' && key[1] == 'x') {
        try {
          stringId = int.parse(key.substring(2), radix: 16);
        } catch (e) {
          print("Error! invalid string key:'$key'");
          rethrow;
        }
      } else {
        stringId = hashString(key);
      }
      pos += key.length;
      String? value = _parseStringValue(pos, text);
      if (value == null) {
        pos++;
        print("Warning! No value found for key:'$key'");
        continue;
      }
      if (_stringSet!.containsKey(stringId)) {
        if (_stringSet![stringId] != value) {
          stringsToSet[key] = value;
        }
      } else {
        stringsToAdd[key] = value;
      }
      pos += value.length;
      pos = text.indexOf('\n', pos);
      if (pos == -1) break;
      pos++;
    }
    if (!addOnly) {
      for (String k in stringsToSet.keys) {
        setString(k, stringsToSet[k]!);
      }
    }
    for (String k in stringsToAdd.keys) {
      if (skipEmptyStrings && stringsToAdd[k] == '') continue;
      addString(k, stringsToAdd[k]!);
      if (verbose) print("info: adding string:'[$k]: '${stringsToAdd[k]}'");
    }
    if (stringsToAdd.isNotEmpty) {
      addStringsToDictionary(stringsToAdd.keys.toList());
    }
  }

  static List<int> getBinaryLocData(String text, List<String> errors) {
    final retVal = <int>[];
    final myStrings = parseStrings(text, errors);
    for (LocalizedString element in myStrings) {
      retVal.addAll(element.getLocBytes());
    }
    return retVal;
  }

  static List<LocalizedString> parseStrings(String text, List<String> errors) {
    final retVal = <LocalizedString>[];
    int pos = 0;
    while (pos < text.length) {
      String? key = _getKey(pos, text);
      if (key == null || key.isEmpty) {
        pos++;
        continue;
      }
      if (key.contains('"') || key.contains('\n')) {
        print('ParseStrings error! Key = $key');
      }
      pos += key.length;
      String? value = _parseStringValue(pos, text);
      if (value == null) {
        pos++;
        errors.add("Warning! No value found for key:'$key'");
        continue;
      }
      retVal.add(LocalizedString(key, value));
      pos += value.length;
      pos = text.indexOf('\n', pos);
      if (pos == -1) break;
      pos++;
    }
    return retVal;
  }

  String getStringsAsCfg(List<String> errors) {
    String input = getAllStrings();
    List<LocalizedString> stringList = parseStrings(input, errors);
    final sb = StringBuffer();
    String currentScope = '';
    String lastScope = '';
    sb.writeln('DataBase()');
    sb.writeln('{');
    for (int i = 0; i < stringList.length; i++) {
      if (stringList[i].stringId.indexOf('0x') == -1) {
        currentScope = _getStringScope(stringList[i].stringId) ?? '';
        if (currentScope.toLowerCase() != lastScope.toLowerCase()) {
          _appendScope(currentScope, lastScope, sb);
        }
        _appendString(stringList[i], sb);
        lastScope = currentScope;
      } else {
        final errorMsg = 'Skipping ${stringList[i].stringId}="${stringList[i].content}"\n';
        errors.add(errorMsg);
        stdout.write(errorMsg);
      }
    }
    final parts = lastScope.split('.');
    for (int j = parts.length - 1; j > -1; j--) {
      sb.write(' ' * (2 + j * 2));
      sb.writeln('}');
    }
    sb.write('}');
    return sb.toString();
  }

  void _appendString(LocalizedString ls, StringBuffer builder) {
    int size = 4 + 2 * ls.content.length;
    int index = ls.stringId.lastIndexOf('.');
    String id = index >= 0 ? ls.stringId.substring(index + 1) : ls.stringId;
    List<String> parts = ls.stringId.split('.');
    String indent = ' ' * (parts.length * 2);
    String indentInner = indent + '  ';
    builder.write(indent);
    builder.writeln('VarBinary("$id")');
    builder.write(indent);
    builder.writeln('{');
    builder.write(indentInner);
    builder.writeln('Size($size);');
    builder.write(indentInner);
    builder.write('Value("');
    String result = _getCrazyPrefix(ls.content) + _convertToStupidFormat(ls.content);
    for (int i = 0; i < result.length; i++) {
      if (i != 0 && i % 64 == 0) {
        builder.write('");\n');
        builder.write(indentInner);
        builder.write('Value("');
      }
      builder.write(result[i]);
    }
    builder.writeln('");');
    builder.write(indent);
    builder.writeln('}');
  }

  static String _convertToStupidFormat(String input) {
    final sb = StringBuffer();
    for (int i = 0; i < input.length; i++) {
      String current = input.codeUnitAt(i).toRadixString(16).padLeft(4, '0').toUpperCase();
      String flipped = current[3] + current[2] + current[1] + current[0];
      sb.write(flipped);
    }
    return sb.toString();
  }

  static String _getCrazyPrefix(String input) {
    String len = '${2 * input.length}00000000';
    return len.length >= 8 ? len.substring(0, 8) : len.padLeft(8, '0');
  }

  void _appendScope(String currentScope, String lastScope, StringBuffer sb) {
    List<String> currentParts = currentScope.split('.');
    List<String> lastParts = lastScope.split('.');
    int i = 0;
    while (i < currentParts.length && i < lastParts.length && lastParts[i] == currentParts[i]) {
      i++;
    }
    if (lastScope.isNotEmpty) {
      for (int j = lastParts.length - 1; j > -1; j--) {
        String lastWord = lastParts.sublist(0, j + 1).join('.');
        String currentWord = j < currentParts.length ? currentParts.sublist(0, j + 1).join('.') : '';
        if (currentWord.toLowerCase() != lastWord.toLowerCase()) {
          sb.write(' ' * (2 + i * 2));
          sb.writeln('}');
        }
      }
    }
    for (; i < currentParts.length; i++) {
      String indent = ' ' * ((i + 1) * 2);
      sb.writeln('${indent}VarScope("${currentParts[i]}")');
      sb.writeln('$indent{');
    }
  }

  static String? _getStringScope(String p) {
    int index = p.lastIndexOf('.');
    if (index > -1) return p.substring(0, index);
    return null;
  }

  static String getVarBinaryObjectPaths(String input) {
    final builder = StringBuffer();
    final lines = input.split('\n');
    final dudes = <String>[];
    for (int i = 0; i < lines.length; i++) {
      String trimmedLine = lines[i].trim();
      if (trimmedLine == '{') {
        // nesting level increased
      } else if (trimmedLine == '}') {
        if (dudes.isNotEmpty) dudes.removeLast();
      } else if (trimmedLine.startsWith('VarScope("')) {
        final scopeName = trimmedLine.substring(10).replaceAll('"', '').replaceAll(')', '').trim();
        dudes.add(scopeName);
      } else if (trimmedLine.startsWith('VarBinary("')) {
        try {
          final rest = trimmedLine.substring(10).split(RegExp(r'"\)?'));
          if (rest.isNotEmpty) {
            dudes.add(rest[0]);
            builder.writeln(dudes.join('.'));
          }
        } catch (_) {}
      }
    }
    return builder.toString();
  }
}

/// String multiplication for indent.
extension _StringRepeat on String {
  String operator *(int n) => List.filled(n, this).join();
}

void makeUcfb(List<int> data, int bodyEnd) {
  int end = bodyEnd;
  while (data.length > end) data.removeLast();
  if (data.isEmpty || data[0] != 0x75) {
    data.insertAll(0, [0, 0, 0, 0, 0, 0, 0, 0]);
    data[0] = 0x75;
    data[1] = 0x63;
    data[2] = 0x66;
    data[3] = 0x62;
  }
  int rem = (data.length - 1) % 4;
  switch (rem) {
    case 0:
      data.addAll([0, 0, 0]);
      break;
    case 1:
      data.addAll([0, 0]);
      break;
    case 2:
      data.add(0);
      break;
  }
  int sz = data.length - 8;
  writeNumberAtLocation(4, sz, data);
  writeNumberAtLocation(0xc, sz - 8, data);
}

class LocalizedString {
  String stringId;
  String content;
  LocalizedString(this.stringId, this.content);

  Uint8List getLocBytes() {
    String stringData = content.replaceAll('\\\\', '\\').replaceAll('\\"', '"').replaceAll('\r\n', '\n').replaceAll('\n', '\r\n');
    if (stringData.isEmpty) stringData = ' ';
    int hashId = LocHelper.getHashId(stringId);
    int sz = (stringData.length * 2) + 2 + 4 + 4;
    if (sz % 4 != 0) sz += 2;
    final mData = Uint8List(sz);
    int stringLoc = 0;
    writeNumberAtLocation(stringLoc, hashId, mData);
    write2ByteNumberAtLocation(stringLoc + 4, sz, mData);
    stringLoc += 6;
    for (int i = 0; i < stringData.length; i++) {
      write2ByteNumberAtLocation(stringLoc, stringData.codeUnitAt(i), mData);
      stringLoc += 2;
    }
    writeNumberAtLocation(stringLoc, 0, mData);
    return Uint8List.fromList(mData);
  }
}
