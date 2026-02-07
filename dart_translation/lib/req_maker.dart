/// Generates .req file content for LVL extraction.
/// Translated from C# CoreCode/ReqMaker.cs for Linux/CLI use.

import 'dart:io';

import 'package:dart_translation/lvltool_config.dart';

String getReq(String directory) {
  final builder = StringBuffer();
  builder.writeln('ucft');
  builder.writeln('{');
  _addFiles(directory, builder, 'config');
  _addFiles(directory, builder, 'script');
  _addFiles(directory, builder, 'texture');
  _addFiles(directory, builder, 'loc');
  _addFiles(directory, builder, 'lvl');
  builder.writeln('}');
  return builder.toString();
}

void _addFiles(String directory, StringBuffer builder, String type) {
  final pattern = '*.$type';
  final files = _getFiles(directory, type);
  if (files.isNotEmpty) {
    builder.writeln('\tREQN');
    builder.writeln('\t{');
    builder.writeln('\t\t"$type"');
    for (String file in files) {
      final base = file.replaceAll('.$type', '');
      int lastSlash = base.lastIndexOf('/') + 1;
      if(lastSlash == 0) {
        lastSlash = base.lastIndexOf('\\') + 1;
      }
      final name = lastSlash > 0 ? base.substring(lastSlash) : base;
      builder.writeln('\t\t"$name"');
    }
    builder.writeln('\t}');
  }
}

List<String> _getFiles(String directory, String extension) {
  final dir = Directory(directory);
  if (!dir.existsSync()) return [];
  return dir
      .listSync()
      .where((e) => e.path.endsWith('.$extension'))
      .map((e) => e.path)
      .toList();
}
