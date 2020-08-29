## LVLTool
https://github.com/BAD-AL/LVLTool (MIT License)

LVLTool is a program useful for Star Wars Battlefront (2004) and Star Wars Battlefront (2005).
It takes apart, adds to, replaces items from the .lvl files that these games use.

It is the follow on to SWBF2_Tool.exe (released in 2018)
LVLTool is better because it allows you to replace most munged items in a .lvl file (they 
do not have to be the same size).
If the item doesn't show up in the listbox, then LVLTool doesn't know how to replace it.

### Major Features:
1. Extract all contents of a .lvl to the 'Munged' items that compose a .lvl file (.script, .texture ...)
2. Replace an item in the .lvl file. Choose an already munged file or a .lua or .tga file.
   * If you choose a .lua file, the program will execute ScriptMunge and add it (if successful).
   * If you choose a .tga file, you'll be prompted for the platform and then the program will run
   * The texturemunge program and add the resulting .texture file (if succesful).
3. Add an item to the end of the .lvl file, same rules apply as for replacing an item.
4. Show Lua code listing (SWBF & SWBFII).
5. Attempt to show decompiled lua code (relies on SWBF2CodeHelper.exe and Phantom5674's LuaDc1.exe https://github.com/phantom567459/BF1LuaDC)
   * Code changes that I made to this program can be found at: https://github.com/BAD-AL/BF1LuaDC


### Important usage notes:
* Be sure to save your .lvl file once you are done replacing/adding items.
* If you are looking for an item and don't see it, the item may be in an embedded lvl file; do an 'extract all' and work on extracted .lvl file, then replace it in the base lvl.
* Use "File->Enter BF modtools dir" To set the path for your mod tools folder (needed to show PC lua code, munge files and list lua scripts) (it is intended that this work with swbf1, but support is currently limited).
   * Ensure "BFBuilder" is in the mod tools path if you are working on BF1 files.
* When adding or replacing an item in the lvl file, you'll be prompted to navigate to a file (for lua, compiling and inserting what is in the text area is not supported).
* SWBF1 lvl files are supported (you can extract all, replace and add items), but lua listing, munging, showing PC code doesn't work yet (Actually, if you had the lua code for SWBF1 in your selected mod tools folder it would work).

