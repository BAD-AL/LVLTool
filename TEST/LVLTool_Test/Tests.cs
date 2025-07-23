using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LVLTool_Test
{
    public class Tests
    {
        /*
Tests exist for the following functionality:

1. Replacing
	* Replace a .texture with .tga for BF1 .lvl
	* Replace a .texture wuth .tga for BF2 .lvl
	* Replace a .script (with lua) for BF1 .lvl
	* Replace a .script (with lua) for BF2 .lvl
2. Adding
	* Add a .script file to a BF1 .lvl
	* Add a .script file to a BF2 .lvl
	* Add a .script (from lua) file to a BF1 .lvl
	* Add a .script (from lua) file to a BF2 .lvl
    * Add a .config (munged) file to a BF1 .lvl
	* Add a .config (munged) file to a BF2 .lvl
    * Add a .mcfg (force config munge) file to a BF1 .lvl
	* Add a .mcfg (force config munge) file to a BF2 .lvl
         
3. Listing
	* List contents of a BF1 .lvl
	* List contents of a BF2 .lvl

4. Rename
	* Rename a chunk of a BF1 .lvl file
	* Rename a chunk of a BF2 .lvl file

5. Merge Strings
	* Merge strings of a BF1 core.lvl file
	* Merge strings of a BF2 core.lvl file

5. List Strings
	* List strings (french, english, all) of a BF1 core.lvl file
	* List strings (french, english, all) of a BF2 core.lvl file
         */

        public string Operation_BFX_Template_Test()
        {
            string retVal = "PASS";
            return retVal;
        }

        #region Replace tests

        public string Replace_BF1_mcfg_Test()
        {
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.out.lvl";
            string replace_me = "TestFiles\\Replacement\\shell_movies.mcfg"; // should be smaller
            // replace item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -r {2} -mod_tools C:\\BFBuilder",
               target_lvl, output_lvl_name, replace_me
               ),
               true).Trim();

            // Compare file size for orig lvl and out lvl; 'out' file should be smaller
            long sz1 = new FileInfo(target_lvl).Length;
            long sz2 = new FileInfo(output_lvl_name).Length;
            if (sz2 >= sz1)
                retVal = String.Format("Fail! output file should be smaller");
            

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Replace_BF1_lua_Test()
        {
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.out.lvl";
            string replace_me = "TestFiles\\Replacement\\globals.lua"; // should be bigger
            // replace item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -r {2} -mod_tools C:\\BFBuilder",
               target_lvl, output_lvl_name, replace_me
               ),
               true).Trim();

            // Compare file size for orig lvl and out lvl; 'out' file should be smaller
            long sz1 = new FileInfo(target_lvl).Length;
            long sz2 = new FileInfo(output_lvl_name).Length;
            if (sz1 >= sz2)
                retVal = String.Format("Fail! output file should be bigger");


            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Replace_BF1_texture_Test()
        {
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.out.lvl";
            string replace_me = "TestFiles\\Replacement\\bf2_radiobutton_on.tga"; // should be bigger
            // replace item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -r {2} -mod_tools_folder C:\\BFBuilder",
               target_lvl, output_lvl_name, replace_me
               ),
               true).Trim();

            // Compare file size for orig lvl and out lvl; 'out' file should be smaller
            long sz1 = new FileInfo(target_lvl).Length;
            long sz2 = new FileInfo(output_lvl_name).Length;
            if (sz1 >= sz2)
                retVal = String.Format("Fail! output file should be bigger");


            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Replace_BF2_mcfg_Test()
        {
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.out.lvl";
            string replace_me = "TestFiles\\Replacement\\shell_movies.mcfg"; // should be smaller
            // replace item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -r {2}",
               target_lvl, output_lvl_name, replace_me
               ),
               true).Trim();

            // Compare file size for orig lvl and out lvl; 'out' file should be smaller
            long sz1 = new FileInfo(target_lvl).Length;
            long sz2 = new FileInfo(output_lvl_name).Length;
            if (sz2 >= sz1)
                retVal = String.Format("Fail! output file should be smaller");


            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Replace_BF2_lua_Test()
        {
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.out.lvl";
            string replace_me = "TestFiles\\Replacement\\globals.lua"; // should be bigger
            // replace item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -r {2}",
               target_lvl, output_lvl_name, replace_me
               ),
               true).Trim();

            // Compare file size for orig lvl and out lvl; 'out' file should be smaller
            long sz1 = new FileInfo(target_lvl).Length;
            long sz2 = new FileInfo(output_lvl_name).Length;
            if (sz1 >= sz2)
                retVal = String.Format("Fail! output file should be bigger");


            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Replace_BF2_texture_Test()
        {
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.out.lvl";
            string replace_me = "TestFiles\\Replacement\\bf2_radiobutton_on.tga"; // should be bigger
            // replace item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -r {2} ",
               target_lvl, output_lvl_name, replace_me
               ),
               true).Trim();

            // Compare file size for orig lvl and out lvl; 'out' file should be smaller
            long sz1 = new FileInfo(target_lvl).Length;
            long sz2 = new FileInfo(output_lvl_name).Length;
            if (sz1 >= sz2)
                retVal = String.Format("Fail! output file should be bigger");


            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        #endregion

        #region Add munged files tests

        public string Add_BF1_munged_script_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.rename.lvl";
            string add_me = "TestFiles\\munged\\BF1\\addme.script";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("addme.scr_") < 0)
            {
                retVal = String.Format("Error! file 'addme' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF1_munged_image_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.rename.lvl";
            string add_me = "TestFiles\\munged\\BF1\\bf2_radiobutton_off.texture";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("bf2_radiobutton_off.tex_") < 0)
            {
                retVal = String.Format("Error! file 'bf2_radiobutton_off' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF1_munged_config_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.rename.lvl";
            string add_me = "TestFiles\\munged\\BF1\\shell_movies.config";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("shell_movies.mcfg") < 0)
            {
                retVal = String.Format("Error! file 'shell_movies.mcfg' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF2_munged_script_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string add_me = "TestFiles\\munged\\BF2\\addme.script";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("addme.scr_") < 0)
            {
                retVal = String.Format("Error! file 'addme' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF2_munged_image_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string add_me = "TestFiles\\munged\\BF2\\bf2_radiobutton_off.texture";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("bf2_radiobutton_off.tex_") < 0)
            {
                retVal = String.Format("Error! file 'bf2_radiobutton_off' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF2_munged_config_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string add_me = "TestFiles\\munged\\BF2\\shell_movies.config";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("shell_movies.mcfg") < 0)
            {
                retVal = String.Format("Error! file 'shell_movies.mcfg' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        #endregion

        #region Add raw files tests

        public string Add_BF1_raw_script_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.rename.lvl";
            string add_me = "TestFiles\\raw\\addme.lua";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("addme.scr_") < 0)
            {
                retVal = String.Format("Error! file 'addme' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF1_raw_image_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.rename.lvl";
            string add_me = "TestFiles\\raw\\bf2_radiobutton_off.tga";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("bf2_radiobutton_off.tex_") < 0)
            {
                retVal = String.Format("Error! file 'bf2_radiobutton_off' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF1_raw_mcfg_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.rename.lvl";
            string add_me = "TestFiles\\raw\\shell_movies.mcfg";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("shell_movies.mcfg") < 0)
            {
                retVal = String.Format("Error! file 'shell_movies.mcfg' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF1_raw_prp_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.rename.lvl";
            string add_me = "TestFiles\\raw\\yav2.prp";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("yav2.prp_") < 0)
            {
                retVal = String.Format("Error! file 'yav2.prp' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF1_raw_bnd_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.rename.lvl";
            string add_me = "TestFiles\\raw\\fel1_DESIGN.BND";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("fel1_DESIGN.BND_") < 0)
            {
                retVal = String.Format("Error! file 'fel1_DESIGN.BND' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF1_raw_fx_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.rename.lvl";
            string add_me = "TestFiles\\raw\\yav2.fx";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("yav2.fx__") < 0)
            {
                retVal = String.Format("Error! file 'yav2.fx' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF2_raw_script_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string add_me = "TestFiles\\raw\\addme.lua";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("addme.scr_") < 0)
            {
                retVal = String.Format("Error! file 'addme' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF2_raw_image_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string add_me = "TestFiles\\raw\\bf2_radiobutton_off.tga";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("bf2_radiobutton_off.tex_") < 0)
            {
                retVal = String.Format("Error! file 'bf2_radiobutton_off' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF2_raw_mcfg_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string add_me = "TestFiles\\raw\\shell_movies.mcfg";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("shell_movies.mcfg") < 0)
            {
                retVal = String.Format("Error! file 'shell_movies.mcfg' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF2_raw_pth_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string add_me = "TestFiles\\raw\\yav2_Design001.PTH";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("yav2_Design001.PTH_") < 0)
            {
                retVal = String.Format("Error! file 'yav2_Design001.PTH' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF2_raw_prp_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string add_me = "TestFiles\\raw\\yav2.prp";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("yav2.prp_") < 0)
            {
                retVal = String.Format("Error! file 'yav2.prp' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF2_raw_bnd_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string add_me = "TestFiles\\raw\\fel1_DESIGN.BND";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("fel1_DESIGN.BND_") < 0)
            {
                retVal = String.Format("Error! file 'fel1_DESIGN.BND' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Add_BF2_raw_fx_Test()
        {
            // -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string add_me = "TestFiles\\raw\\yav2.fx";
            // add a munged item
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -a {2}",
               target_lvl, output_lvl_name, add_me
               ),
               true).Trim();

            // list the contents of the new lvl; check that it has out new item added
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();
            if (contents.IndexOf("yav2.fx__") < 0)
            {
                retVal = String.Format("Error! file 'yav2.fx' was not added!");
            }

            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        #endregion


        #region renaming tests
        public string Rename_BF1_item_Test()
        {
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string output_lvl_name = "TestFiles\\BF1_lvl\\BF1_test.rename.lvl";
            string targetItem = "globals";
            string renameTo   = "foo.bar";
            //  -rename  <old_name> <new_name>  Rename a UCFB chunk (names must be same size).

            // Rename an item; save to another lvl
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -rename {2} {3}",
               target_lvl, output_lvl_name, targetItem, renameTo
               ),
               true).Trim();
            // Read the contents of the new file
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();

            //Ensure it has our new name and not the old name
            if (contents.IndexOf(renameTo) < 0)
            {
                retVal = String.Format("Error! item '{0}' was not found!\n", renameTo);
            }
            if (contents.IndexOf(targetItem) > 0)
            {
                retVal = String.Format("Error! item '{0}' was found!\n", targetItem);
            }
            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }

        public string Rename_BF2_item_Test()
        {
            string retVal = "PASS";
            string target_lvl = "TestFiles\\BF2_lvl\\BF2_test.lvl";
            string output_lvl_name = "TestFiles\\BF2_lvl\\BF2_test.rename.lvl";
            string targetItem = "globals";
            string renameTo = "foo.bar";
            //  -rename  <old_name> <new_name>  Rename a UCFB chunk (names must be same size).

            // Rename an item; save to another lvl
            string t1 = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -o {1} -rename {2} {3}",
               target_lvl, output_lvl_name, targetItem, renameTo
               ),
               true).Trim();
            // Read the contents of the new file
            string contents = Program.RunCommand(
               "LVLTool.exe", String.Format("-file {0} -l ",
               output_lvl_name
               ),
               true).Trim();

            //Ensure it has our new name and not the old name
            if (contents.IndexOf(renameTo) < 0)
            {
                retVal = String.Format("Error! item '{0}' was not found!\n", renameTo);
            }
            if (contents.IndexOf(targetItem) > 0)
            {
                retVal = String.Format("Error! item '{0}' was found!\n", targetItem);
            }
            // delete the new file
            File.Delete(output_lvl_name);

            return retVal;
        }
        #endregion

        #region List Contents tests
        public string List_BF1_Content_Test()
        {
            string retVal = "PASS";
            string targetLvl = "TestFiles\\BF1_lvl\\BF1_test.lvl";
            string expected = File.ReadAllText("TestFiles\\BF1_lvl\\BF1_test.lvl.contents.txt").Trim();
            string result = Program.RunCommand(
                "LVLTool.exe", String.Format("-file {0} -l",targetLvl),true).Trim();

            if (result.CompareTo(expected) != 0)
            {
                retVal = String.Format(
                    "Fail! output does not match 'BF1_test.lvl.contents.txt'\n=====Expected=====\n{0}\n====Actual========\n{1}\n==================",
                    expected,
                    result
                    );
            }
            return retVal;
        }

        public string List_BF2_Content_Test()
        {
            string retVal = "PASS";
            string targetLvl = "TestFiles\\BF2_lvl\\bf2_test.lvl";
            string expected = File.ReadAllText("TestFiles\\BF2_lvl\\bf2_test.contents.txt").Trim();
            string result = Program.RunCommand(
                "LVLTool.exe", String.Format("-file {0} -l", targetLvl), true).Trim();

            if (result.CompareTo(expected) != 0)
            {
                retVal = String.Format(
                    "Fail! output does not match 'bf2_test.contents.txt'\n=====Expected=====\n{0}\n====Actual========\n{1}\n==================",
                    expected,
                    result
                    );
            }
            return retVal;
        }
        #endregion

        
        #region Merge Strings Test

        public string MergeStrings_BF1_Test()
        {
            string retVal = "PASS";
            string core_merge_folder = "Temp\\core_merge_test\\";
            //LVLTool.exe -file base.core.lvl -o core.lvl -merge_strings top_folder_with_cores
            Directory.CreateDirectory(core_merge_folder);

            // use existing BF1 core file as base
            File.Copy("TestFiles\\BF1_lvl\\core.lvl", core_merge_folder + "base.core.lvl");
            // copy the english Strings to the test folder to add them
            string mod_folder = "Temp\\core_merge_test\\000\\";
            Directory.CreateDirectory(mod_folder);
            File.Copy("TestFiles\\raw\\english.txt", mod_folder + "english.txt");

            // run the merge core operation
            string result = Program.RunCommand(
                "LVLTool.exe",
                " -file Temp\\core_merge_test\\base.core.lvl -o Temp\\core_merge_test\\core.lvl -merge_strings Temp\\core_merge_test\\",
                true).Trim();
            //weapons.rep.weap.inf_flamethrower="Flamespitter"
            string string_content = Program.RunCommand(
                "LVLTool.exe",
                "-file Temp\\core_merge_test\\core.lvl -list_strings english",
                true).Trim();
            string look_for = "Flamespitter";
            if (string_content.IndexOf(look_for) < 0)
            {
                retVal = "Error! strings not merged into core.lvl";
            }

            return retVal;
        }

        public string MergeStrings_BF2_Test()
        {
            string retVal = "PASS";
            string core_merge_folder = "Temp\\core_merge_test\\";
            //LVLTool.exe -file base.core.lvl -o core.lvl -merge_strings top_folder_with_cores
            Directory.CreateDirectory(core_merge_folder);
            
            // use existing BF1 core file as base
            File.Copy("TestFiles\\BF2_lvl\\core.lvl", core_merge_folder + "base.core.lvl");
            // copy the english Strings to the test folder to add them
            string mod_folder = "Temp\\core_merge_test\\000\\";
            Directory.CreateDirectory(mod_folder);
            File.Copy("TestFiles\\raw\\english.txt", mod_folder + "english.txt");

            // run the merge core operation
            string result = Program.RunCommand(
                "LVLTool.exe", 
                " -file Temp\\core_merge_test\\base.core.lvl -o Temp\\core_merge_test\\core.lvl -merge_strings Temp\\core_merge_test\\",
                true).Trim();
            //weapons.rep.weap.inf_flamethrower="Flamespitter"
            string string_content = Program.RunCommand(
                "LVLTool.exe",
                "-file Temp\\core_merge_test\\core.lvl -list_strings english",
                true).Trim();
            string look_for = "Flamespitter";
            if (string_content.IndexOf(look_for) < 0)
            {
                retVal = "Error! strings not merged into core.lvl";
            }

            return retVal;
        }
        #endregion

        #region BF1 List strings tests
        public string List_BF1_English_Strings_Test()
        {
            string retVal = "PASS";
            string targetLvl = "TestFiles\\BF1_lvl\\core.lvl";
            string targetString = "- DESTROY THE TECHNO UNION SHIPS TO PREVENT THE SEPARATISTS FROM ESCAPING.";
            // french = "- DÉTRUISEZ LES VAISSEAUX DE LA TECHNO UNION POUR EMPÊCHER LES SÉPARATISTES DE S'ÉCHAPPER."
            string result = Program.RunCommand(
                "LVLTool.exe", String.Format("-file {0} -list_strings english", targetLvl), true).Trim();

            if (result.IndexOf(targetString) < 0)
            {
                retVal = String.Format(
                    "Fail! Could not find string '{0}' in output",
                    targetString
                    );
            }
            return retVal;
        }

        public string List_BF1_French_Strings_Test()
        {
            string retVal = "PASS";
            string targetLvl = "TestFiles\\BF1_lvl\\core.lvl";
            string targetString = "APPUYEZ SUR X POUR CONTINUER";
            string result = Program.RunCommand(
                "LVLTool.exe", String.Format("-file {0} -list_strings french", targetLvl), true).Trim();

            if (result.IndexOf(targetString, StringComparison.InvariantCulture) < 0)
            {
                retVal = String.Format(
                    "Fail! Could not find string '{0}' in output",
                    targetString
                    );
            }
            return retVal;
        }

        public string List_BF1_All_Strings_Test()
        {
            string retVal = "PASS";
            string targetLvl = "TestFiles\\BF1_lvl\\core.lvl";
            string targetString  = "- DESTROY THE TECHNO UNION SHIPS TO PREVENT THE SEPARATISTS FROM ESCAPING.";
            string targetString2 = "APPUYEZ SUR X POUR CONTINUER";

            string result = Program.RunCommand(
                "LVLTool.exe", String.Format("-file {0} -list_strings all", targetLvl), true).Trim();

            if (result.IndexOf(targetString) < 0)
            {
                retVal = String.Format(
                    "Fail! Could not find english string '{0}' in output",
                    targetString
                    );
            }
            if (result.IndexOf(targetString2) < 0)
            {
                retVal = String.Format(
                    "Fail! Could not find french string '{0}' in output",
                    targetString2
                    );
            }
            return retVal;
        }
        #endregion


        #region BF2 List strings tests
        public string List_BF2_English_Strings_Test()
        {
            string retVal = "PASS";
            string targetLvl = "TestFiles\\BF2_lvl\\core.lvl";
            string targetString = "Don't get cocky, kid!"; // french = "Ne sois pas arrogant, petit !"
            string result = Program.RunCommand(
                "LVLTool.exe", String.Format("-file {0} -list_strings english", targetLvl), true).Trim();

            if (result.IndexOf(targetString) < 0)
            {
                retVal = String.Format(
                    "Fail! Could not find string '{0}' in output",
                    targetString
                    );
            }
            return retVal;
        }

        public string List_BF2_French_Strings_Test()
        {
            string retVal = "PASS";
            string targetLvl = "TestFiles\\BF2_lvl\\core.lvl";
            string targetString = "Ne sois pas arrogant, petit !";
            string result = Program.RunCommand(
                "LVLTool.exe", String.Format("-file {0} -list_strings french", targetLvl), true).Trim();

            if (result.IndexOf(targetString) < 0)
            {
                retVal = String.Format(
                    "Fail! Could not find string '{0}' in output",
                    targetString
                    );
            }
            return retVal;
        }

        public string List_BF2_All_Strings_Test()
        {
            string retVal = "PASS";
            string targetLvl = "TestFiles\\BF2_lvl\\core.lvl";
            string targetString = "Don't get cocky, kid!";
            string targetString2 = "Ne sois pas arrogant, petit !";
            
            string result = Program.RunCommand(
                "LVLTool.exe", String.Format("-file {0} -list_strings all", targetLvl), true).Trim();

            if (result.IndexOf(targetString) < 0)
            {
                retVal = String.Format(
                    "Fail! Could not find english string '{0}' in output",
                    targetString
                    );
            }
            if (result.IndexOf(targetString2) < 0)
            {
                retVal = String.Format(
                    "Fail! Could not find french string '{0}' in output",
                    targetString2
                    );
            }
            return retVal;
        }
        #endregion

    }
}
