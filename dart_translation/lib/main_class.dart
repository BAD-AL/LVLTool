
// ignore_for_file: non_constant_identifier_names, prefer_is_empty, curly_braces_in_flow_control_structures

import 'dart:convert';
import 'dart:io';

import 'package:dart_translation/core_code.dart';
import 'package:dart_translation/lvltool_config.dart';
import 'package:path/path.dart' as p;


class MainClass {
  static String input_lvl ="";
  static String output_lvl ="";
  static String operation ="";
  static String platform ="";
  static String old_name ="";
  static String new_name = "";
  static String files ="";
  static String merge_strings_search_dir = "";
  static String loc_language ="";
  static String inner_lua_file ="";
  static String ModToolsDir ="";
  
static void RunMain(List<String> args) {
  
  if(args.length == 0){
    printHelp();
    return;
  }
  processArgs(args);

  switch(operation){
    case "ShowHelp":
      printHelp();
    break;
    case "Extract":
      if(input_lvl.length > 0){
        UcfbHelper helper = UcfbHelper();
        helper.fileName = input_lvl;
        helper.extractContents();
      } else{
        print("Error! No input file specified.");
      }
    break;
    case "Replace":
      if (output_lvl.length == 0) {
        output_lvl = input_lvl;
      }
      if (input_lvl.length > 0) {
          List<String> files_ = getFiles(files);
          String f = Munger.ensureMungedFile( files_[0]);
          String name = getFileNameOnly(f);
          Munger.verifyUcfbFile(f); // will throw exception if not valid
          UcfbHelper helper = UcfbHelper();
          helper.fileName = input_lvl;
          helper.initializeRead();
          Chunk? c;
          while ((c = helper.ripChunk(false)) != null) {
              if(verbose)
                print("info: Examining chunk ${c!.name}");
              if (c!.name == name) {
                  break;
              }
          }
          if(c != null){
            helper.replaceUcfbChunk(c,f,true);
            print("Replaced item ${c.name}");
            helper.saveData(output_lvl);
          } else{
            print("Could not find '$name' in '$f'. No action taken");
          }
      }
      else {
        print("Error! Must specify input file");
      }
    break;
    case "Rename":
      if (input_lvl.length == 0) {
        print("Error! Must specify input file");
      }
      else if (old_name.length == 0 || new_name.length == 0) {
        print("Error! Must specify old and new names");
      }
      else if (old_name.length != new_name.length) {
        print("Error! Old and new names must be the same length");
      }
      else {
        UcfbHelper helper = UcfbHelper();
        helper.fileName = input_lvl;
        helper.initializeRead();
        Chunk? c;
        while ((c = helper.ripChunk(false)) != null) {
          if (c!.name == old_name) {
            break;
          }
        }
        // ignore: unnecessary_null_comparison
        if (c != null) {
          int start = getLocationOfGivenBytes(c.start, latin1.encode(c.name), helper.data, 80);
          for (int i = 0; i < c.name.length; i++) {
              helper.data[start + i] = new_name.codeUnitAt(i);
          }
          helper.saveData(output_lvl);
          print("info: Changed ${c.name} to $new_name, saved to $output_lvl");
        }
        else {
            print("Could not find '$old_name'. No action taken");
        }
      }
    break;
    case "Add":
      if (output_lvl.length == 0) {
        output_lvl = input_lvl;
      }
      if (input_lvl.length > 0) {
          UcfbHelper helper = UcfbHelper();
          helper.fileName = input_lvl;
          List<String> files_ = getFiles(files);
          for (String f in files_) {
              String fileName = Munger.ensureMungedFile(f, platform);
              helper.addItemToEnd(fileName);
          }
          helper.saveData(output_lvl);
      }
      else {
        print("Error! Must specify input file");
      }
    break;
    case "ListContents":
      UcfbHelper listHelper = UcfbHelper();
      listHelper.fileName = input_lvl;
      listHelper.initializeRead();
      Chunk? cur;
      while ((cur = listHelper.ripChunk(false)) != null) {
          print("${cur!.name}.${cur.type}");
      }
    break;
    case "LuaList":
      String content = listLua(input_lvl, inner_lua_file);
      if(content.length > 0){
        print(content);
      } else{
        print("Error! Could not find script file '$inner_lua_file'");
      }
    break;
    case "ListStrings":
      CoreMerge cm = CoreMerge(input_lvl);
      cm.gatherStrings(input_lvl);
      String? results;
      if (loc_language.toLowerCase() != "all") {
        results = cm.getStrings(loc_language);
      } else {
        results = cm.getAllStrings();
      }

      if (results != null ) {
          print("Localization strings:\r\n$results");
      }
      else {
          print("Error: No localization strings found inside file: '$input_lvl'");
      }
    break;
    case "MergeCore":
      final searchDir = Directory(merge_strings_search_dir);

      // 1. Create a growable list of strings
      List<String> theFiles = [];
      // 2. Define the filenames we are looking for
      final targetNames = {
            'core.lvl', 'english.txt', 'spanish.txt', 'italian.txt', 'french.txt', 'german.txt', 'japanese.txt', 'uk_english.txt',
      };
      if (searchDir.existsSync()) {
        // listSync(recursive: true) finds everything in all subdirectories
        final allEntities = searchDir.listSync(recursive: true);

        for (var entity in allEntities) {
          if (entity is File) {
            // Get just the filename (e.g., "english.txt")
            String fileName = p.basename(entity.path);
            
            if (targetNames.contains(fileName)) {
              theFiles.add(entity.path);
            }
          }
        }
        //static void mergeLoc(String baseFileName, String saveFileName, List<String> files) 
        CoreMerge.mergeLoc(input_lvl, output_lvl, theFiles);
        print("info: Merged strings from ${theFiles.length} files into '$output_lvl'");
      } else {
        print("Error: Search directory '$merge_strings_search_dir' does not exist.");
        print("Target directory is '${searchDir.absolute.path}'");
        print("Current directory is '${Directory.current.absolute.path}'");
      }
    break;
  }
  
}

static String getFileNameOnly(String path) {
  // Get the last part of the path (the filename)
  //String fileName = File(path).absolute.path.split(Platform.pathSeparator).last;
  String fileName = p.basename(path);
  
  // Find the last dot and cut everything after it
  int lastDot = fileName.lastIndexOf('.');
  if (lastDot == -1) return fileName; // No extension found
  
  return fileName.substring(0, lastDot);
}

static List<String> getFiles(String files) {
    List<String> retVal = [];
    if (files.length > 0) {
        retVal = files.split(";");
    }
    return retVal;
}

static void processArgs(List<String>args){
  int i = 0;
  for (; i < args.length; i++) {
      switch (args[i].toLowerCase()) {
          case "-file":
              input_lvl = p.normalize(args[i + 1]);
              i++;
              break;
          case "-o":
              output_lvl = p.normalize(args[i + 1]); 
              i++;
              break;
          case "-r":
              operation = "Replace";
              files = args[i + 1];
              i++;
              break;
          case "-rename":
              operation = "Rename";
              old_name = args[i + 1];
              new_name = args[i + 2];
              i += 2;
              break;
          case "-a":
              operation = "Add";
              files = args[i + 1];
              i++;
              break;
          case "-e":
              operation = "Extract";
              break;
          case "-h":
          case "--help":
          case "/h":
          case "/?":
              operation = "ShowHelp";
              break;
          case "-p":
              platform = args[i + 1].toUpperCase();
              i++;
              break;
          case "-mod_tools_folder":
              ModToolsDir = p.normalize( args[i + 1]);
              i++;
              break;
          case "-l":
              operation = "ListContents";
              break;
          case "-verbose":
              verbose = true;
              break;
          case "-merge_strings":
              operation = "MergeCore";
              merge_strings_search_dir = p.normalize(args[i + 1]);
              i++;
              break;
          case "-list_strings":
              operation = "ListStrings";
              try{
                loc_language = args[i + 1];
              } catch(e){
                print("Error: Must specify language after -list_strings");
                exit(0);
              }
              i++;
              break;
          case "-ll":
              operation = "LuaList";
              inner_lua_file = args[i + 1];
              i++;
              break;
      }
  }
}

static void printHelp(){
  String help = 
"""LVLTool
Use at the command line with the following arguments:
 -file <lvl file>  The file to operate on
 -r <munged file(s)>  to replace munged files in the target .lvl file.
 -a <munged file(s)>  to add munged files at the end of the target .lvl file.
 -o <lvl file>  The output file name. (default is input lvl file)
 -e             Extract contents
 -rename  <old_name> <new_name>  Rename a UCFB chunk (names must be same size).
 -l List the contents of the munged/lvl file.
 -ll <inner_file_name> print out a lua listing of the target file (must be a script, mod_tools_folder dependent)
 -list_strings (all|english|spanish|italian|french|german|japanese|uk_english)
 -merge_strings <search_dir> Read in the '-file' arg, add the strings found in the 'core.lvl's 
               under 'search_dir' save to file specified with the '-o' arg.

 -verbose       Display more info as operations are occuring.
 -h --help  /? Print help message

Examples:
    (Replace shell_interface script inside shell.lvl)
LVLTool -file shell.lvl -r shell_interface.script
 
    (extract all (munged) contents from shell.lvl)
LVLTool -file shell.lvl -e
 
    (Add XXXg_con and XXXc_con to the end of mission.lvl)
LVLTool -file mission.lvl -a XXXg_con.script;XXXc_con.script

    (show all strings in the given .lvl)
LVLTool -file core.lvl -list_strings all
    (show all english strings in the given .lvl)
LVLTool -file core.lvl -list_strings english

    (add the strings from core.lvl files under 'top_folder_with_cores', to 'base.core.lvl', save as 'core.lvl' )
LVLTool -file base.core.lvl -o core.lvl -merge_strings top_folder_with_cores
""";
  print(help);
}


static String listLua(String inputLvlName, String innerLuaFile) {
    String retVal = "";
    String fileToFind = innerLuaFile;
    if(fileToFind.endsWith(".scr_"))
        fileToFind = fileToFind.replaceAll(".scr_", "");
    if (fileToFind.endsWith(".lvl_"))
        fileToFind = fileToFind.replaceAll(".lvl_", "");

    var ucfbFileHelper = new UcfbHelper();
    ucfbFileHelper.fileName = inputLvlName;
    ucfbFileHelper.initializeRead();
    Chunk? cur;
    while ((cur = ucfbFileHelper.ripChunk(false)) != null)
    {
        if( fileToFind == cur!.name && cur.type == "scr_") // what about if it's inside an inner .lvl file?
        {
            retVal = getLuacListing(cur);
            break;
        }
        else if (fileToFind == cur.name && cur.type == "lvl_")
        {
            String tmpFile = "tmp.lvl";
            UcfbHelper.saveFileUCFB(tmpFile, cur.data);
            retVal = listLua(tmpFile, fileToFind);
            File(tmpFile).delete();
            break;
        }
    }
    return retVal;
}
}