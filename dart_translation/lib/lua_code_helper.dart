/// Lua script chunk handling and decompilation for LVLTool.
/// Translated from C# CoreCode/LuaCodeHelper.cs for Linux/CLI use.
/// UI (prompts for multiple matches) is replaced by optional callback or first match.

import 'dart:io';

import 'ucfb_helper.dart';
import 'package:dart_translation/lvltool_config.dart';

List<String>? _allLuaFiles;

void resetFileCache() {
  _allLuaFiles = null;
}

/// Callback when multiple source files match (e.g. return selected path). If null, first match is used.
String? Function(String message, String title, List<String> choices, String defaultChoice)?
    promptUserForSelection;

String getCodeSummary(Chunk chunk) {
  String retVal = '';
  String? sourceFileName;
  final sourceFileNames = findSourceFile(chunk.name);
  if (sourceFileNames.length == 1) {
    sourceFileName = sourceFileNames[0];
  } else if (sourceFileNames.length > 1) {
    if (promptUserForSelection != null) {
      sourceFileName = promptUserForSelection!(
          'Multiple matches found', 'Choose version:', sourceFileNames, sourceFileNames[0]);
    } else {
      sourceFileName = sourceFileNames[0];
    }
  }
  if (sourceFileName != null && sourceFileName.isNotEmpty) {
    final code = File(sourceFileName).readAsStringSync();
    final sz = luacCodeSize(sourceFileName);
    final body = chunk.getBodyData();
    if (body != null && (body.length - 1) == sz) {
      retVal += '\n-- ********* LUAC Code Size MATCH!!! ***********';
      final tmpPath = pathJoin(tmpDir, 'tmp.luac');
      if (File(tmpPath).existsSync()) {
        final b = File(tmpPath).readAsBytesSync();
        int i = 0;
        for (i = 0; i < body.length - 1 && i < b.length; i++) {
          if (b[i] != body[i]) break;
        }
        if (i == body.length - 1) retVal += '\n-- ********* Binary Equal !!! ***********';
      }
    }
    retVal += '\n-- $sourceFileName\n-- PC luac code size = $sz; PC code:\n$code';
  } else {
    retVal = 'File not found: ${chunk.name}';
  }
  return retVal;
}

String lookupPCcode(String fileName) {
  final sourceFileNames = findSourceFile(fileName);
  if (sourceFileNames.length == 1) return sourceFileNames[0];
  if (sourceFileNames.length > 1 && promptUserForSelection != null) {
    final chosen = promptUserForSelection!(
        'Multiple matches; use \'${sourceFileNames[0]}\'?', 'Multiple matches', sourceFileNames, sourceFileNames[0]);
    return chosen ?? sourceFileNames[0];
  }
  return sourceFileNames.isNotEmpty ? sourceFileNames[0] : '';
}

List<String> findSourceFile(String? fileName) {
  final retVal = <String>[];
  if (fileName == null) return retVal;
  if (_allLuaFiles == null) {
    while (!Directory(modToolsDir).existsSync()) {
      if (modToolsDir.isEmpty) return retVal;
    }
    _allLuaFiles = [];
    if (luaSourceDir == null || luaSourceDir!.isEmpty) {
      final dirs = [
        pathJoin(modToolsDir, 'assets/Shell'),
        pathJoin(modToolsDir, 'data/Shell/'),
        pathJoin(modToolsDir, 'data/Common/'),
        pathJoin(modToolsDir, 'TEMPLATE/Common/scripts/'),
        pathJoin(modToolsDir, 'space_template/'),
      ];
      for (String d in dirs) {
        if (Directory(d).existsSync()) {
          _allLuaFiles!.addAll(Directory(d).listSync(recursive: true).where((e) => e.path.toLowerCase().endsWith('.lua')).map((e) => e.path));
        }
      }
      if (_allLuaFiles!.isEmpty && Directory(modToolsDir).existsSync()) {
        _allLuaFiles!.addAll(Directory(modToolsDir).listSync(recursive: true).where((e) => e.path.toLowerCase().endsWith('.lua')).map((e) => e.path));
      }
    } else if (Directory(luaSourceDir!).existsSync()) {
      _allLuaFiles!.addAll(Directory(luaSourceDir!).listSync(recursive: true).where((e) => e.path.toLowerCase().endsWith('.lua')).map((e) => e.path));
      print('info: Found ${_allLuaFiles!.length} lua files.');
    } else {
      throw FileSystemException('Could not locate alternate LUA Source Dir \'$luaSourceDir\', Please enter a valid folder');
    }
  }
  final searchFor = fileName.toLowerCase().endsWith('.lua') ? fileName : '$fileName.lua';
  for (String file in _allLuaFiles!) {
    if (file.toLowerCase().endsWith(searchFor.toLowerCase())) retVal.add(file);
  }
  return retVal;
}

int luacCodeSize(String luaSourceFile) {
  int retVal = -1;
  final tmpPath = pathJoin(tmpDir, 'tmp.luac');
  if (runCommand != null) {
    final result = runCommand!(luac, ' -s -o $tmpPath $luaSourceFile', true);
    if (result.length < 10) {
      if (File(tmpPath).existsSync()) {
        retVal = File(tmpPath).lengthSync();
      }
    }
  }
  return retVal;
}

String getLuacListing(Chunk c) {
  final bodyData = c.getBodyData();
  if (bodyData == null) return '';
  final fileName = pathJoin(tmpDir, 'decompile.luac');
  final fs = File(fileName).openSync(mode: FileMode.write);
  fs.writeFromSync(bodyData.sublist(0, bodyData.length - 1));
  fs.closeSync();
  String output = runCommand != null ? runCommand!(luac, '-l $fileName', true) : '';
  return '\n-- ${c.name}\n-- luac -l listing \n$output';
}

String? decompile(Chunk c) {
  String? retVal;
  final targetFile = pathJoin(tmpDir, 'tmp.lua');
  if (File(targetFile).existsSync()) File(targetFile).deleteSync();
  final bodyData = c.getBodyData();
  if (bodyData == null) return null;
  final fileName = pathJoin(tmpDir, 'tmp.luac');
  final fs = File(fileName).openSync(mode: FileMode.write);
  fs.writeFromSync(bodyData.sublist(0, bodyData.length - 1));
  fs.closeSync();
  if (luac.toLowerCase().contains('bfbuilder')) {
    retVal = _decompileBF1Code(fileName);
  } else {
    const prog = 'SWBF2CodeHelper.exe';
    final args = ' $fileName';
    if (runCommand != null) {
      runCommand!(prog, args, true);
    }
    if (!File(targetFile).existsSync()) {
      return 'Error! Could not find output!\n${runCommand != null ? runCommand!(prog, args, true) : ''}';
    }
    retVal = File(targetFile).readAsStringSync();
  }
  return retVal;
}

String _decompileBF1Code(String fileName) {
  if (runCommand == null) return '';
  final output = runCommand!(luac, ' -l $fileName', true);
  final listFile = pathJoin(tmpDir, 'luac.list');
  File(listFile).writeAsStringSync(output);
  const prog = 'LuaDC1.exe';
  final args = '$listFile -o ${pathJoin(tmpDir, 'tmp.lua')}';
  runCommand!(prog, args, true);
  return File(pathJoin(tmpDir, 'tmp.lua')).readAsStringSync();
}
