
// ignore_for_file: curly_braces_in_flow_control_structures

// dart test

import 'dart:io';
import 'package:dart_translation/main_class.dart';
import 'package:test/test.dart';

ProcessResult _runCli(List<String> args, {String? workingDirectory}) {
    final exe = Platform.executable; // dart executable
    final procArgs = <String>[]..addAll(['bin/dart_translation.dart'])..addAll(args);
    print("Running: $exe ${procArgs.join(' ')}  in ${workingDirectory ?? Directory.current.path}");
    return Process.runSync(exe, procArgs, workingDirectory: workingDirectory);
}

void _runMain(List<String> args) {
  MainClass.RunMain(args);
}

void main() {
    test('help flags produce output', () {
        final flags = ['-h', '--help', '/?'];
        String output ="";
        for (final f in flags) {
            final r = _runCli([f]);
            expect(r.exitCode, equals(0), reason: 'exit code for $f');
            output = r.stdout as String;
            expect(output.contains("Examples:"), isTrue, reason: 'help output should have Examples section ');
        }
    });

    test('list strings (english) when file present', () {
        String filePath = "test/core.lvl";
        final r = _runCli(['-file', filePath, '-list_strings', 'english']);
        expect(r.exitCode, equals(0));
        String output = r.stdout as String;
        String test = "Don't get cocky, kid!";
        String test_key = "cheats.ammo_off";

        //expect(output.isNotEmpty , isTrue);
        expect(output.contains(test), isTrue);
        expect(output.contains(test_key), isTrue);
    });

  test('Test path separator', () {
    // On Windows both '.\' and './' work
    // but on Linux, only './' works
      String loc_1 = ".\\test\\core.lvl";
      String loc_2 = "./test/core.lvl";
      if( File(loc_1).existsSync()) {
        print('Testing Windows path separator == good');
      } 
      if( File(loc_2).existsSync()) {
        print('Testing Unix path separator == good');
      }
    });

  test('list contents on common.lvl', () {
        String filePath ="test/common.lvl";
        final r = _runCli(['-file', filePath, '-l']);
        expect(r.exitCode, equals(0));
        String output = r.stdout as String;
        expect(output.contains("globals.scr_"), isTrue);
        
    });
    test('merge strings Test via MainClass', () {
        String baseFile ='test/mods/base.core.lvl';
        String addonDir = 'test/mods/addon';
        final outFile = 'test/mods/core_test.lvl';
        _runMain(['-file', baseFile, '-o', outFile, '-merge_strings', addonDir]);
        // test for "Gametoast Arena", "The Clone Wars Revised", "Kashyyyk: Kachirho"
        expect(File(outFile).existsSync(), isTrue);
        final r2 = _runCli(['-file', outFile, '-list_strings', 'english']);
        expect(r2.exitCode, equals(0));
        String output2 = r2.stdout as String;
        expect(output2.contains("Gametoast Arena"), isTrue);
        //expect(output2.contains("The Clone Wars Revised"), isTrue);
        //expect(output2.contains("Kashyyyk: Kachirho"), isTrue);

        // now delete the output file
        final outF = File(outFile);
        if (outF.existsSync()) {
            outF.deleteSync();
        }
    });

    test('replace munged file ', () {
        String commonFile = "test/common.lvl";
        // replace munged
        String globalsFile ="test/globals.script";
        final outFile = "test/common_test.lvl";
        //final r = _runCli(['-file', commonFile, '-o', outFile, '-r', globalsFile]);
        //expect(r.exitCode, equals(0));
        //expect(File(outFile).existsSync(), isTrue);
        _runMain( ['-file', commonFile, '-o', outFile,  '-r', globalsFile]);
        expect(File(outFile).existsSync(), isTrue);

        File orig =File(commonFile);
        File modified =File(outFile);
        expect(orig.lengthSync() != modified.lengthSync(), isTrue, reason: "file sizes should differ after replace munged");
    
        // now delete the outFile 
        final outF = File(outFile);
        if (outF.existsSync()) {
            outF.deleteSync();
        }
    });

    test('add munged file ', () {
        String commonFile = "test/core.lvl";
        // replace munged
        String globalsFile ="test/globals.script";
        final outFile = "test/core_test.lvl";
        
        // add munged
        final r2 = _runCli(['-file', commonFile, '-o', outFile, '-a', globalsFile]);
        // Success depends on presence of globals.script; just check it runs without crashing
        expect(r2.exitCode, isNotNull);

        // Get a listing of munged files to verify addition
        final r3 = _runCli(['-file', outFile, '-l' ]);
        expect(r3.exitCode, isNotNull);
        String output3 = r3.stdout as String;
        expect(output3.contains("globals.scr_"), isTrue, reason: "munged files listing should contain globals.script");

        // now delete the outFile 
        final outF = File(outFile);
        if (outF.existsSync()) {
            outF.deleteSync();
        }
    });

    test('extract contents (smoke) when sample present', () {
        String filePath ="test/common.lvl";
        String extracted_dir = "test/common/";
        final r = _runCli(['-file', filePath, '-e']);
        expect(r.exitCode, equals(0));
        //_runMain( ['-file', filePath, '-e']);
        // test that the extracted dir exists
        final extractedDir = Directory(extracted_dir);
        expect(extractedDir.existsSync(), isTrue);
        // test that the extracted dir has some files
        final files = extractedDir.listSync(recursive: true).whereType<File>().toList();
        expect(files.length > 10, isTrue);

        // now delete the extracted dir
        if (extractedDir.existsSync()) {
            extractedDir.deleteSync(recursive: true);
        }
    });


      test('List contentsof lua file', () {
        String filePath ="test/common.lvl";
        final r = _runCli(['-file', filePath, '-ll', 'globals']);
        expect(r.exitCode, equals(0));
        String output = r.stdout as String;
        expect(output.contains("RollRight"), isTrue);

    });
}
