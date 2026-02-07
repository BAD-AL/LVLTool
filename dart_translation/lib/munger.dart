/// Munging (texture, script, config) for LVLTool.
/// Translated from C# CoreCode/Munger.cs for Linux/CLI use.
/// UI (file dialogs) is not available; callers pass file paths.

import 'dart:io';
import 'dart:typed_data';

import 'bin_utils.dart';
import 'package:dart_translation/lvltool_config.dart';

final _mungableExtensions = [
  '.tga', '.lua', '.mcfg', '.fx', '.prp', '.bnd', '.snd', '.mus',
  '.combo', '.sanm', '.hud', '.cfg', '.pth', '.sky', '.lgt', '.pvs', '.tsr',
];

bool isMungableFile(String file) {
  final lastDot = file.lastIndexOf('.');
  if (lastDot < 0) return false;
  final suffix = file.substring(lastDot).toLowerCase();
  return _mungableExtensions.contains(suffix);
}

/// Returns munged file path, or same path if not mungable. Throws if not UCFB.
String ensureMungedFile(String fileName, [String platform = 'pc']) {
  String retVal = fileName;
  if (isMungableFile(fileName)) {
    retVal = mungeFile(fileName, platform)!;
  }
  verifyUcfbFile(retVal);
  return retVal;
}

String? mungeFile(String file, [String platform = 'pc']) {
  final lastDot = file.lastIndexOf('.');
  if (lastDot < 0) return null;
  final suffix = file.substring(lastDot).toLowerCase();
  if (!Directory(tmpDir).existsSync()) {
    Directory(tmpDir).createSync(recursive: true);
  }
  switch (suffix) {
    case '.tga':
      return _textureMunge(file, platform);
    case '.lua':
      return _scriptMunge(file);
    case '.mcfg':
    case '.fx':
    case '.prp':
    case '.bnd':
    case '.snd':
    case '.mus':
    case '.combo':
    case '.sanm':
    case '.hud':
    case '.cfg':
    case '.pth':
    case '.sky':
    case '.lgt':
    case '.pvs':
    case '.tsr':
      return _configMunge(file);
    default:
      return null;
  }
}

String? _textureMunge(String file, String platform) {
  if (!File(file).existsSync()) return null;
  final sep = file.contains('/') ? '/' : '\\';
  final lastSep = file.lastIndexOf(sep);
  final tmpFile = pathJoin(tmpDir, lastSep >= 0 ? file.substring(lastSep + 1) : file);
  File(file).copySync(tmpFile);
  String prog = pathJoin(modToolsDir, 'ToolsFL/bin/pc_TextureMunge.exe');
  if (!File(prog).existsSync()) {
    prog = pathJoin(modToolsDir, 'ToolsFL/bin/TextureMunge.exe');
  }
  if (!File(prog).existsSync()) {
    throw Exception(
        'Could not find texture munge program; ensure modtools directory is set correctly');
  }
  final args = '-sourcedir $tmpDir -platform $platform -inputfile ${tmpFile.split(sep).last} -outputdir $tmpDir';
  if (runCommand != null) {
    runCommand!(prog, args, true);
  }
  final outputFile = tmpFile.replaceAll('.tga', '.texture');
  return File(outputFile).existsSync() ? outputFile : null;
}

String? _scriptMunge(String file) {
  if (!File(file).existsSync()) return null;
  final sep = file.contains('/') ? '/' : '\\';
  final lastSep = file.lastIndexOf(sep);
  final tmpFile = pathJoin(tmpDir, lastSep >= 0 ? file.substring(lastSep + 1) : file);
  File(file).copySync(tmpFile);
  final prog = pathJoin(modToolsDir, 'ToolsFl/bin/ScriptMunge.exe');
  final args = '-sourcedir . -platform pc -inputfile $tmpFile -outputdir $tmpDir';
  if (runCommand != null) {
    runCommand!(prog, args, true);
  }
  final outputFile = tmpFile.replaceAll('.lua', '.script');
  return File(outputFile).existsSync() ? outputFile : null;
}

String? _configMunge(String file) {
  if (!File(file).existsSync()) return null;
  final sep = file.contains('/') ? '/' : '\\';
  final lastSep = file.lastIndexOf(sep);
  final tmpFile = pathJoin(tmpDir, lastSep >= 0 ? file.substring(lastSep + 1) : file);
  File(file).copySync(tmpFile);
  final prog = pathJoin(modToolsDir, 'ToolsFl/bin/ConfigMunge.exe');
  final args = getConfigMungeArgs(tmpFile);
  int extIndex = args.indexOf('-ext ');
  String outputExt = '.config';
  if (extIndex > -1) {
    outputExt = '.${args.substring(extIndex + 5).trim()}';
  }
  if (verbose) print('info: ConfigMunge; outputExt= $outputExt');
  if (runCommand != null) {
    runCommand!(prog, args, true);
  }
  final inputExt = file.contains('.') ? file.substring(file.lastIndexOf('.')) : '';
  final outputFile = tmpFile.replaceAll(inputExt, outputExt);
  return File(outputFile).existsSync() ? outputFile : null;
}

String getConfigMungeArgs(String fileName) {
  final bf1HashStrings = ['.mcfg', '.snd', '.mus', '.prp', '.bnd', '.tsr'];
  final bf2HashStrings = ['.mcfg', '.snd', '.mus', '.prp', '.bnd'];
  final bf1ChunkIds = {'.sky': 'sky', '.prp': 'prp', '.fx': 'fx', '.bnd': 'bnd'};
  final bf2ChunkIds = {
    '.cfg': 'load', '.pth': 'path', '.sky': 'sky', '.fx': 'fx',
    '.prp': 'prp', '.bnd': 'bnd', '.lgt': 'lght', '.pvs': 'PORT'
  };
  final bf1Exts = {'.fx': 'envfx', '.prp': 'prop', '.bnd': 'boundary'};
  final bf2Exts = {
    '.cfg': 'config', '.pth': 'path', '.fx': 'envfx', '.prp': 'prop',
    '.bnd': 'boundary', '.lgt': 'light', '.pvs': 'povs'
  };
  final ext = fileName.contains('.') ? fileName.substring(fileName.lastIndexOf('.')) : '';
  String retVal = '-inputfile *$ext -continue -platform pc -sourcedir $tmpDir -outputdir $tmpDir ';
  final isBF2 = _isCurrentConfigBF2();
  if (isBF2) {
    if (bf2HashStrings.contains(ext)) retVal += '-hashstrings ';
    if (bf2ChunkIds.containsKey(ext)) retVal += '-chunkid ${bf2ChunkIds[ext]} ';
    if (bf2Exts.containsKey(ext)) retVal += '-ext ${bf2Exts[ext]} ';
  } else {
    if (bf1HashStrings.contains(ext)) retVal += '-hashstrings ';
    if (bf1ChunkIds.containsKey(ext)) retVal += '-chunkid ${bf1ChunkIds[ext]} ';
    if (bf1Exts.containsKey(ext)) retVal += '-ext ${bf1Exts[ext]} ';
  }
  return retVal;
}

bool _isCurrentConfigBF2() {
  final checkFor = pathJoin(modToolsDir, 'ToolsFL/bin/swbf2rm.exe');
  return File(checkFor).existsSync();
}

void verifyUcfbFile(String filename) {
  final f = File(filename).openSync();
  final buff = f.readSync(8);
  f.closeSync();
  final ucfb = Uint8List.fromList([0x75, 0x63, 0x66, 0x62]);
  if (!binCompare(ucfb, buff, 0)) {
    throw Exception('Error! File is not UCFB! $filename');
  }
}
