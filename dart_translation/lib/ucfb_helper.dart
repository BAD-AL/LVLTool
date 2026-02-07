/// UCFB (Zero Engine) file extraction and chunk handling.
/// Translated from C# CoreCode/UcfbHelper.cs for Linux/CLI use.

import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';

import 'bin_utils.dart';
import 'hash_helper.dart';
import 'loc_helper.dart';
import 'package:dart_translation/lvltool_config.dart';
import 'req_maker.dart';
import 'munger.dart';

final _UCFB = Uint8List.fromList([0x75, 0x63, 0x66, 0x62]); // 'ucfb'
final _NAME = Uint8List.fromList([0x4E, 0x41, 0x4D, 0x45]); // 'NAME'
final _INFO = Uint8List.fromList([0x49, 0x4E, 0x46, 0x4F]); // 'INFO'

final _sPopularSuffixes = [
  '_1ctf', '_1flag', '_Buildings', '_Buildings01', '_Buildings02',
  '_CP-Assult', '_CP-Conquest', '_CP-VehicleSpawns', '_CPs', '_CommonDesign',
  '_CW-Ships', '_GCW-Ships', '_Damage', '_Damage01', '_Damage02', '_Death',
  '_DeathRegions', '_Design', '_Design001', '_Design002', '_Design01',
  '_Design02', '_Design1', '_Design2', '_Doors', '_Layer000', '_Layer001',
  '_Layer002', '_Layer003', '_Layer004', '_Light_RG', '_NewObjective',
  '_Objective', '_Platforms', '_Props', '_RainShadow', '_Roids', '_Roids01',
  '_Roids02', '_Shadow_RGN', '_Shadows', '_Shields', '_SoundEmmiters',
  '_SoundRegions', '_SoundSpaces', '_SoundTriggers', '_Temp', '_Tree',
  '_Trees', '_Vehicles', '_animations', '_campaign', '_collision', '_con',
  '_conquest', '_ctf', '_deathreagen', '_droids', '_eli', '_flags', '_gunship',
  '_hunt', '_invisocube', '_light_region', '_objects01', '_objects02',
  '_reflections', '_rumble', '_rumbles', '_sound', '_tdm', '_trees',
  '_turrets', '_xl', '_sniper',
];

const _fileTypeExtensions = {
  'mcfg': '.config',
  'snd_': '.config',
  'zaf_': '.zafbin',
  'zaa_': '.zaabin',
  'lvl_': '.lvl',
  'Locl': '.loc',
  'tex_': '.texture',
  'LuaP': '.script',
  'scr_': '.script',
  'SHDR': '.shader',
  'entc': '.class',
  'skel': '.model',
  'ANIM': '.anims',
  'bnd_': '.boundary',
  'plan': '.congraph',
  'fx__': '.envfx',
  'lght': '.light',
  'wrld': '.world',
};

class UcfbHelper {
  Uint8List? _data;
  String _fileName = '';
  int _currentPos = 0;
  final List<String> _manifest = [];
  int _planFileCount = 0;

  Uint8List get data => _data ?? Uint8List(0);

  String get fileName => _fileName;
  set fileName(String value) {
    if (_fileName != value) {
      _fileName = value;
      _onFileNameChanged(_fileName);
    }
  }

  void _onFileNameChanged(String path) {
    _data = Uint8List.fromList(File(path).readAsBytesSync());
    final sep = path.contains('/') ? '/' : '\\';
    final lastSlash = path.lastIndexOf(sep);
    if (lastSlash > -1) {
      String baseFileName = path.substring(lastSlash + 1).replaceAll('.lvl', '');
      addHashedString(baseFileName);
      addHashedString('mapname.description.$baseFileName');
      addHashedString('mapname.name.$baseFileName');
      for (String suffix in _sPopularSuffixes) {
        addHashedString(baseFileName + suffix);
      }
    }
  }

  String _getLvlFileName() {
    final sep = _fileName.contains('/') ? '/' : '\\';
    return _fileName.substring(_fileName.lastIndexOf(sep) + 1);
  }

  String _getExtractDir() {
    final lastDot = _fileName.lastIndexOf('.');
    final path = lastDot > 0 ? _fileName.substring(0, lastDot) : _fileName;
    return pathJoin(path, '');
  }

  String _createExtractDir() {
    final path = _getExtractDir();
    Directory(path).createSync(recursive: true);
    return path;
  }

  String _getFileName(String name, String fileType, bool withDir) {
    String suffix = _fileTypeExtensions[fileType] ?? '.$fileType';
    String prefix = withDir ? _getExtractDir() : '';
    String fileName = prefix + name + suffix;
    int num = 0;
    while (_manifest.indexOf(fileName) > -1) {
      num++;
      fileName = prefix + name + '_$num' + suffix;
    }
    if (fileName.contains('xml.shader')) {
      fileName = fileName.replaceAll('xml.shader', 'shader');
    }
    return fileName;
  }

  static void saveFileUCFB(String name, Uint8List data) {
    final encodedLen = encodeNumber(data.length);
    final buffer = <int>[
      0x75, 0x63, 0x66, 0x62, // ucfb
      encodedLen[0], encodedLen[1], encodedLen[2], encodedLen[3],
    ];
    try {
      final f = File(name).openSync(mode: FileMode.write);
      f.writeFromSync(buffer);
      f.writeFromSync(data);
      f.closeSync();
    } catch (e) {
      if (messageUser != null) {
        messageUser!('Error Saving file: $name\n$e');
      }
    }
  }

  void _consumeBytes(int num) {
    _currentPos += num;
  }

  int _readNumber() {
    final retVal = getNumberAtLocation(_currentPos, _data!);
    _consumeBytes(4);
    return retVal;
  }

  int _peekNumber(int location) {
    return getNumberAtLocation(location, _data!);
  }

  String _peekChunkType() {
    return getByteString(_currentPos, _data!, 4);
  }

  Uint8List _readBytes(int numBytes) {
    int num = numBytes;
    if (num > _data!.length - _currentPos) {
      num = _data!.length - _currentPos;
    }
    final retVal = Uint8List.fromList(_data!.sublist(_currentPos, _currentPos + num));
    _consumeBytes(num);
    return retVal;
  }

  void _alignHead() {
    _consumeBytes(_currentPos % 4);
  }

  String _peekName(int location, String chunkType) {
    String name = '';
    if (chunkType == 'plan') {
      return _getLvlFileName().replaceAll('.lvl', '');
    }
    int loc = -1;
    if (chunkType == 'lvl_') {
      final hash = getNumberAtLocation(location + 8, _data!);
      final str = getStringFromHash(hash);
      name = str ?? '0x${hash.toRadixString(16)}';
    }
    final nameBytes = ascii.encode('NAME');
    final typeBytes = ascii.encode('TYPE');
    if (chunkType == 'entc') {
      loc = getLocationOfGivenBytes(location, typeBytes, _data!, 80);
    } else {
      loc = getLocationOfGivenBytes(location, nameBytes, _data!, 80);
    }
    if (loc > -1) {
      int nameLen = _data![loc + 4] - 1;
      if (loc > 0 && nameLen > 0) {
        name = latin1.decode(_data!.sublist(loc + 8, loc + 8 + nameLen));
        final zeroTest = name.indexOf('\u0000');
        if (zeroTest > -1) name = name.substring(0, zeroTest);
      }
      final hexNameTypes = ['mcfg', 'sanm', 'fx__'];
      if (nameLen == 3 && (hexNameTypes.contains(chunkType) || _hasBadCharacters(name))) {
        final hash = getNumberAtLocation(loc + 8, _data!);
        final str = getStringFromHash(hash);
        name = str ?? '0x${hash.toRadixString(16)}';
      }
    }
    return name;
  }

  bool _hasBadCharacters(String tst) {
    if (tst.indexOf('?') > -1) return true;
    for (int i = 0; i < tst.length; i++) {
      final c = tst[i];
      if (!_isLetterOrDigit(c) && c != '_') return true;
    }
    return false;
  }

  bool _isLetterOrDigit(String c) {
    if (c.isEmpty) return false;
    final code = c.codeUnitAt(0);
    return (code >= 0x30 && code <= 0x39) ||
        (code >= 0x41 && code <= 0x5A) ||
        (code >= 0x61 && code <= 0x7A);
  }

  /// Returns path to extraction folder.
  String? extractContents() {
    _currentPos = 0;
    String retVal = _createExtractDir();
    if (!binCompare(_UCFB, _data!, _currentPos)) {
      if (messageUser != null) {
        messageUser!('ERROR! This is not a UCF file: $_fileName');
      }
      return null;
    }
    int bytesLeftInFile = initializeRead();
    if (_data!.length - (_currentPos + bytesLeftInFile) != 0) {
      if (messageUser != null) {
        messageUser!('WARNING! ucfb length data kinda sketchy: $_fileName');
      }
    }
    _manifest.clear();
    try {
      while (ripChunk(true) != null) {}
    } catch (e) {
      if (e is RangeError) {} else rethrow;
    }
    _writeManifest();
    if (_fileName.endsWith('.lvl')) {
      String reqFileName = _fileName.replaceAll('.lvl', '.req');
      final sep = reqFileName.contains('/') ? '/' : '\\';
      final lastSlash = reqFileName.lastIndexOf(sep);
      reqFileName = lastSlash >= 0
          ? pathJoin(retVal, reqFileName.substring(lastSlash + 1))
          : pathJoin(retVal, reqFileName);
      final req = getReq(retVal);
      File(reqFileName).writeAsStringSync(req);
    }
    return retVal;
  }

  void _writeManifest() {
    final builder = StringBuffer();
    for (String entry in _manifest) {
      builder.writeln(entry);
    }
    final fileName = _getFileName('__manifest', 'txt', true);
    File(fileName).writeAsStringSync(builder.toString());
  }

  Chunk? ripChunk(bool saveChunk) {
    final ct = _peekChunkType();
    if (ct.isEmpty) return null;
    int chunkLen = 0;
    String? name;
    try {
      chunkLen = _peekNumber(_currentPos + 4);
      name = _peekName(_currentPos + 8, ct);
    } catch (e) {
      stderr.writeln('RipChunk Error: $e');
      return null;
    }
    final retVal = Chunk();
    retVal.type = ct;
    retVal.length = chunkLen;
    retVal.name = name;
    retVal.start = _currentPos;
    Uint8List data;
    if (ct == 'lvl_') {
      _consumeBytes(16);
      data = _readBytes(chunkLen + 8);
    } else {
      data = _readBytes(chunkLen + 8);
    }
    retVal.data = data;
    _alignHead();
    String outFileName = _getFileName(name, ct, true);
    if (ct == 'plan') {
      outFileName = outFileName.replaceAll('.plan', '_$_planFileCount.plan');
      _planFileCount++;
    }
    if (saveChunk) {
      saveFileUCFB(outFileName, data);
    }
    if (_manifest.indexOf(outFileName) > -1 && saveChunk) {
      print('Warning: overwriting file $outFileName');
    }
    _manifest.add(outFileName);
    return retVal;
  }

  void replaceUcfbChunk(Chunk chk, String mungedFileName, bool replaceFirstOnly) {
    final mungedFileBytes = File(mungedFileName).readAsBytesSync();
    replaceUcfbChunkBytes(chk, mungedFileBytes, replaceFirstOnly);
  }

  void replaceUcfbChunkBytes(
      Chunk chk, Uint8List mungedFileBytes, bool replaceFirstOnly) {
    initializeRead();
    String curType = _peekChunkType();
    String curName = _peekName(_currentPos + 8, curType);
    String cur = '$curName.$curType';
    String target = '${chk.name}.${chk.type}';
    Chunk? c;
    do {
      curType = _peekChunkType();
      if (curType.isEmpty) break;
      curName = _peekName(_currentPos + 8, curType);
      cur = '$curName.$curType';
      if (target.toLowerCase() == cur.toLowerCase()) {
        final chunkStart = _currentPos;
        final chunkEnd = _peekNumber(_currentPos + 4) + _currentPos + 8;
        final mungedContentLength = mungedFileBytes.length - 8;
        final chunkLen = chunkEnd - chunkStart;
        final difference = (chunkLen - mungedContentLength).toInt();
        final newLength = _data!.length - difference;
        List<int> newData = List<int>.filled(newLength, 0);
        for (int i = 0; i < chunkStart; i++) newData[i] = _data![i];
        for (int i = 0; i < mungedFileBytes.length - 8; i++) {
          newData[chunkStart + i] = mungedFileBytes[8 + i];
        }
        final nextStart = chunkStart + mungedFileBytes.length - 8;
        final tailLen = _data!.length - chunkEnd;
        if (tailLen > 0) {
          for (int i = 0; i < tailLen; i++) {
            newData[nextStart + i] = _data![chunkEnd + i];
          }
        }
        _data = Uint8List.fromList(newData);
        final encodedLen = encodeNumber(_data!.length - 8);
        _data![4] = encodedLen[0];
        _data![5] = encodedLen[1];
        _data![6] = encodedLen[2];
        _data![7] = encodedLen[3];
        if (replaceFirstOnly) break;
      }
      c = ripChunk(false);
    } while (c != null);
  }

  int initializeRead() {
    _currentPos = 0;
    _planFileCount = 0;
    _consumeBytes(4);
    return _readNumber();
  }

  void saveData(String fileName) {
    File(fileName).writeAsBytesSync(_data!);
    print("Saved file: '$fileName'");
  }

  void addItemToEnd(String mungedFile) {
    verifyUcfbFile(mungedFile);
    Uint8List newData = File(mungedFile).readAsBytesSync();
    List<int> bigData = List<int>.from(_data!);
    final newDataStart = bigData.length;
    bigData.addAll(newData);
    bigData.removeRange(newDataStart, newDataStart + 8);
    _data = Uint8List.fromList(bigData);
    final headerLength = _data!.length - 8;
    _data![4] = headerLength & 0x000000FF;
    _data![5] = (headerLength & 0x0000FF00) >> 8;
    _data![6] = (headerLength & 0x00FF0000) >> 16;
    _data![7] = (headerLength & 0xFF000000) >> 24;
    print("info: Added '$mungedFile'");
  }
}

class Chunk {
  String name = '';
  String type = '';
  int length = 0;
  int start = 0;
  Uint8List data = Uint8List(0);

  LocHelper? locHelper;

  @override
  String toString() => '$name.$type';

  Uint8List getAssetData() => data;

  Uint8List? getBodyData() {
    final bodyBytes = Uint8List.fromList([0x42, 0x4f, 0x44, 0x59]); // BODY
    final loc = getLocationOfGivenBytes(0, bodyBytes, data, 80);
    if (loc > -1) {
      final bodyLen = getNumberAtLocation(loc + 4, data);
      final bodyStart = loc + 8;
      final bodyData = data.sublist(bodyStart, bodyStart + bodyLen);
      return Uint8List.fromList(bodyData);
    }
    return null;
  }

  String getSummary() {
    String retVal = 'Name:   $name\nType:   $type\nStart:  0x${start.toRadixString(16)}\n'
        'Length: 0x${length.toRadixString(16)}\nSize:   ${(length / 1024).toStringAsFixed(1)} kb';
    final infoBytes = utf8.encode('INFO');
    final loc = getLocationOfGivenBytes(0, infoBytes, data, 80);
    if (loc > -1) {
      final info = getNumberAtLocation(loc + 4, data);
      retVal += '\nINFO:   0x${info.toRadixString(16)}';
    }
    if (name.startsWith('0x')) {
      final hash = int.parse(name.substring(2), radix: 16);
      final unHashed = getStringFromHash(hash);
      if (unHashed != null) {
        retVal += "hash lookup for: $name>'$unHashed' \n";
      } else {
        retVal += 'Unknown name hash\n';
      }
    }
    return retVal;
  }
}
