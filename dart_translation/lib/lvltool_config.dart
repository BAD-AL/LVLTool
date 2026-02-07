/// Configuration and environment for LVLTool Dart core.
/// Replaces Program.* from the C# app so the Dart code can run on Linux/CLI.

import 'dart:io';

/// Callback for showing messages to the user (e.g. stderr, dialog).
void Function(String message)? messageUser = (String message) {
  stderr.writeln(message);
};

/*// Dart's Process.runSync parameters:
ProcessResult runSync(
  String executable,
  List<String> arguments, {
  String? workingDirectory,
  Map<String, String>? environment,
  bool includeParentEnvironment = true,
  bool runInShell = false,
  Encoding? stdoutEncoding = systemEncoding,
  Encoding? stderrEncoding = systemEncoding,
  })
*/

/// Runs an external command and returns combined stdout and stderr.
String runCommand(String programName, String args, bool includeStdErr) {
  print("info: Running command: $programName $args");

  final progToRun = File(programName);
  Map<String, String> environment = Map.from(Platform.environment);

  if (!progToRun.existsSync()) {
    print("Could not find '$programName'");
  } else {
    // Make PATH variable awesome
    // Note: C# uses ';' on Windows, but Dart/Linux uses ':'. 
    // Platform.pathSeparator helps, but for PATH specifically, we check the OS.
    final separator = Platform.isWindows ? ';' : ':';
    final directoryName = progToRun.parent.path;
    final currentPath = environment['PATH'] ?? environment['Path'] ?? '';
    
    environment['PATH'] = "$directoryName$separator$currentPath";
  }

  // Dart expects arguments as a List. 
  // Simple split by space (note: this doesn't handle quoted spaces like C# might)
  List<String> argList = args.isEmpty ? [] : args.split(' ');

  // Process.runSync is the equivalent of WaitForExit()
  ProcessResult result = Process.runSync(
    programName,
    argList,
    environment: environment,
    runInShell: false, // Equivalent to UseShellExecute = false
    stdoutEncoding: systemEncoding, // Uses the system's default encoding
    stderrEncoding: systemEncoding,
  );

  String output = result.stdout.toString();
  String err = result.stderr.toString();

  if (includeStdErr) {
    // Using \n for Dart convention, but kept \r\n to match your C# output
    output = "$output\r\n$err";
  }

  return output;
}

/// Temporary directory path (with trailing separator). Created if needed.
String get tmpDir {
  const d = './temp/';
  Directory(d).createSync(recursive: true);
  return d;
}

/// Mod tools directory (e.g. BF2_ModTools). Set by app.
String modToolsDir = 'C:/BF2_ModTools';

/// Lua source directory override. If null, use ModToolsDir subdirs.
String? luaSourceDir;

/// Path to luac executable.
String get luac {
  // Prefer same dir, then mod tools.
  if (Platform.isWindows) {
    String loc_1 = '$modToolsDir/ToolsFL/bin/luac.exe';
    String loc_2 = './bin/luac.exe';
    String loc_3 = './luac.exe';
    final p = '$modToolsDir/ToolsFL/bin/Luac.exe';
    if (File(p).existsSync()) return p;
    final p2 = './luac.exe';
    if (File(p2).existsSync()) return p2;
  } else {
    String loc_1 = './luac';
    String loc_2 = './bin/luac';
    if (File(loc_1).existsSync()) return loc_1;
    if (File(loc_2).existsSync()) return loc_2;
  }
  print('Warning: luac not found.');
  return '';
}

bool verbose = false;

/// Normalize path separator for current platform.
String pathJoin(String a, String b) {
  a = a.replaceAll('\\', '/');
  b = b.replaceAll('\\', '/');
  if (a.endsWith('/')) return a + b;
  return '$a/$b';
}
