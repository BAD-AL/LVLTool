 LVLTool
	 The follow on to SWBF2_Tool 
	 LVLTool is better because it allows you to replace most munged items in a .lvl file (they 
	 do not have to be the same size).
	 If the item doesn't show up in the listbox, then LVLTool doesn't know how to replace it.
	 
 Major Features:
	 1. Extract contents of a .lvl to the 'Munged' items that compose a .lvl file (.script, .texture ...)
	 2. Replace an item in the .lvl file. Choose an already munged file or a .lua or .tga file.
		 If you choose a .lua file, the program will execute ScriptMunge and add it (if successful).
		 If you choose a .tga file, you'll be prompted for the platform and then the program will run
		 the texturemunge program and add the resulting .texture file (if succesful).
	 3. Add an item to the end of the .lvl file, same rules apply as for replacing an item.
	 4. Show Lua code listing.
	 5. Attempt to show decompiled lua code (relies on SWBF2CodeHelper.exe, currently Lua 5.0.2 only)
	 
 
 Important usage notes:
	 Be sure to save your .lvl file once you are done replacing/adding files.
	 If you are looking for an item and don't see it, the item may be in an embedded lvl file;
	 do an 'extract all' and work on extracted .lvl file, then replace it in the base lvl.
 
 