/// Merges localization strings from core.lvl files into a base core.lvl.
/// Translated from C# CoreCode/CoreMerge.cs for Linux/CLI use.

import 'dart:io';

import 'loc_helper.dart';
import 'ucfb_helper.dart';
import 'package:dart_translation/lvltool_config.dart';

class CoreMerge {
  UcfbHelper? _coreUcfbFileHelper;
  final Map<String, StringBuffer> _stringsToAdd = {};

  CoreMerge(String baseCoreFile) {
    _coreUcfbFileHelper = UcfbHelper();
    _coreUcfbFileHelper!.fileName = baseCoreFile;
    _coreUcfbFileHelper!.initializeRead();
  }

  void gatherStrings(String file) {
    if (file.toLowerCase().endsWith('.lvl')) {
      final ucfbFileHelper = UcfbHelper();
      ucfbFileHelper.fileName = file;
      ucfbFileHelper.initializeRead();
      Chunk? cur;
      while ((cur = ucfbFileHelper.ripChunk(false)) != null) {
        if (cur != null &&  cur.type == 'Locl') {
          cur.locHelper = LocHelper(cur.getAssetData());
          final allStrings = cur.locHelper!.getAllStrings();
          if (_stringsToAdd.containsKey(cur.name)) {
            final sb = _stringsToAdd[cur.name]!;
            final s = sb.toString();
            if (s.isNotEmpty && s.codeUnitAt(s.length - 1) != 10) {
              sb.writeln();
            }
            sb.write(allStrings);
          } else {
            _stringsToAdd[cur.name] = StringBuffer(allStrings);
          }
        }
      }
    } else if (file.toLowerCase().endsWith('.txt')) {
      final sep = file.contains('/') ? '/' : '\\';
      final lastSlash = file.lastIndexOf(sep);
      if (lastSlash > -1) {
        final langName = file.substring(lastSlash + 1).replaceAll('.txt', '');
        final allStrings = File(file).readAsStringSync();
        if (_stringsToAdd.containsKey(langName)) {
          final sb = _stringsToAdd[langName]!;
          final s = sb.toString();
          if (s.isNotEmpty && s.codeUnitAt(s.length - 1) != 10) {
            sb.writeln();
          }
          sb.write(allStrings);
        } else {
          _stringsToAdd[langName] = StringBuffer(allStrings);
        }
      }
    } else {
      print("info: skipping file '$file'");
    }
  }

  String? getStrings(String language) {
    return _stringsToAdd[language]?.toString();
  }

  String? getAllStrings() {
    if (_stringsToAdd.isEmpty) return null;
    final retVal = StringBuffer();
    for (String key in _stringsToAdd.keys) {
      retVal.writeln('//Language: $key');
      retVal.write(_stringsToAdd[key].toString());
    }
    return retVal.toString();
  }

  void _addGatheredStrings() {
    if (_stringsToAdd.isEmpty) return;
    Chunk? cur;
    final locChunks = <Chunk>[];
    while ((cur = _coreUcfbFileHelper!.ripChunk(false)) != null) {
      if ( cur != null && cur.type == 'Locl') locChunks.add(cur);
    }
    for (int i = locChunks.length - 1; i > -1; i--) {
      final c = locChunks[i];
      if (_stringsToAdd.containsKey(c.name)) {
        final helper = LocHelper(c.getAssetData());
        helper.addNewStrings(_stringsToAdd[c.name].toString());
        _coreUcfbFileHelper!.replaceUcfbChunkBytes(c, helper.getUcfbData(), true);
      }
    }
  }

  void save(String fileName) {
    _addGatheredStrings();
    _coreUcfbFileHelper!.saveData(fileName);
  }

  static void mergeLoc(String baseFileName, String saveFileName, List<String> files) {
    final cm = CoreMerge(baseFileName);
    for (String file in files) {
      print("info: Gathering strings from '$file'");
      try {
        cm.gatherStrings(file);
      } catch (ex, stack) {
        if (messageUser != null) {
          messageUser!('Exception encountered while processing file $file\n$ex\n$stack');
        }
      }
    }
    cm.save(saveFileName);
  }
}
