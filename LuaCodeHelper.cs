using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LVLTool
{
    public class LuaCodeHelper
    {
        private static List<string> sAllLuaFiles = null;

        public static void ResetFileCache()
        {
            sAllLuaFiles = null;
        }

        public static string GetCodeSummary(Chunk chunk)
        {
            string retVal = "";
            string sourceFileName = null;
            List<string> sourceFileNames = FindSourceFile(chunk.Name);
            if (sourceFileNames.Count == 1)
                sourceFileName = sourceFileNames[0];
            else if (sourceFileNames.Count > 1)
            {
                /*sourceFileName = sourceFileNames[0];
                foreach (string file in sourceFileNames)
                {
                    if (MessageBox.Show(String.Format("Multiple matches; use '{0}'?", file),
                        "Multiple matches", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        sourceFileName = file;
                        break;
                    }
                }*/
                sourceFileName = ListForm.PromptUserForSelection("Multiple matches found", "Choose version:",
                    sourceFileNames, sourceFileNames[0], new MainForm());
            }
            //sourceFileName = chunk.Name;
            if (!String.IsNullOrEmpty(sourceFileName))
            {
                string code = File.ReadAllText(sourceFileName); //LookupPCcode(sourceFileName);
                int sz = LuacCodeSize(sourceFileName);
                byte[] body = chunk.GetBodyData();
                if ((body.Length - 1) == sz)
                {
                    retVal += "\n-- ********* LUAC Code Size MATCH!!! ***********";
                    byte[] b = File.ReadAllBytes(Program.TmpDir + "tmp.luac");

                    int i = 0;
                    for (i = 0; i < body.Length - 1; i++)
                        if (b[i] != body[i])
                            break;
                    if (i == body.Length - 1)
                        retVal += "\n-- ********* Binary Equal !!! ***********";
                }
                retVal = retVal + string.Format("\n-- {0}\n-- PC luac code size = {1}; PC code:\n{2}", sourceFileName, sz, code);
            }
            else
            {
                retVal = "File not found: "+ chunk.Name;
            }
            return retVal;
        }

        public static string LookupPCcode(string fileName)
        {
            string retVal = "";
            List<string> sourceFileNames = FindSourceFile(fileName);
            if (sourceFileNames.Count == 1)
                retVal = sourceFileNames[0];
            else if (sourceFileNames.Count > 1)
            {
                foreach (string file in sourceFileNames)
                {
                    if (MessageBox.Show(String.Format("Multiple matches; use '{0}'?", file),
                        "Multiple matches", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        retVal = file;
                        break;
                    }
                }
            }
            return retVal;
        }

        public static List<string> FindSourceFile(string fileName)
        {
            List<string> retVal = new List<String>();
            if (fileName != null)
            {
                if (sAllLuaFiles == null)
                {
                    while (!Directory.Exists(Program.ModToolsDir))
                    {
                        if (String.IsNullOrEmpty(Program.ModToolsDir))
                            return null;
                    }
                    sAllLuaFiles = new List<string>();

                    if (Program.LuaSourceDir == null)
                    {
                        if (Directory.Exists(Program.ModToolsDir + "assets\\Shell"))
                            sAllLuaFiles.AddRange(Directory.GetFiles(Program.ModToolsDir + "assets\\Shell", "*.lua", SearchOption.AllDirectories));
                        if (Directory.Exists(Program.ModToolsDir + "data\\Shell\\"))
                            sAllLuaFiles.AddRange(Directory.GetFiles(Program.ModToolsDir + "data\\Shell\\", "*.lua", SearchOption.AllDirectories));
                        if (Directory.Exists(Program.ModToolsDir + "data\\Common\\"))
                            sAllLuaFiles.AddRange(Directory.GetFiles(Program.ModToolsDir + "data\\Common\\", "*.lua", SearchOption.AllDirectories));


                        if (Directory.Exists(Program.ModToolsDir + "TEMPLATE\\Common\\scripts\\"))
                            sAllLuaFiles.AddRange(Directory.GetFiles(Program.ModToolsDir + "TEMPLATE\\Common\\scripts\\", "*.lua", SearchOption.AllDirectories));
                        if (Directory.Exists(Program.ModToolsDir + "space_template\\"))
                            sAllLuaFiles.AddRange(Directory.GetFiles(Program.ModToolsDir + "space_template\\", "*.lua", SearchOption.AllDirectories));
                        if (sAllLuaFiles.Count == 0 && Directory.Exists(Program.ModToolsDir))
                            sAllLuaFiles.AddRange(Directory.GetFiles(Program.ModToolsDir, "*.lua", SearchOption.AllDirectories));
                    }
                    else if (Directory.Exists(Program.LuaSourceDir))
                    {
                        sAllLuaFiles.AddRange(Directory.GetFiles(Program.LuaSourceDir, "*.lua", SearchOption.AllDirectories));
                        Console.WriteLine("Found {0} lua files.", sAllLuaFiles.Count);
                    }
                    else
                    {
                        throw new FileNotFoundException ("Could not locate alternate LUA Source Dir '" + Program.LuaSourceDir +"', Please enter a valid folder");
                    }
                }

                string searchFor = fileName.EndsWith(".lua") ? fileName : fileName + ".lua";
                foreach (string file in sAllLuaFiles)
                {
                    if (file.EndsWith(searchFor, StringComparison.InvariantCultureIgnoreCase))
                    {
                        retVal.Add(file);
                    }
                }
            }
            return retVal;
        }

        public static int LuacCodeSize(string luaSourceFile)
        {
            int retVal = -1;
            string result = Program.RunCommand(Program.Luac, " -s -o "+ Program.TmpDir +"tmp.luac " + luaSourceFile, true);
            if (result.Length < 10)
            {
                FileInfo info = new FileInfo(Program.TmpDir +  "tmp.luac");
                retVal = (int)info.Length;
            }
            return retVal;
        }

        internal static string GetLuacListing(Chunk c)
        {
            byte[] bodyData = c.GetBodyData();
            string fileName = Program.TmpDir + "decompile.luac";
            //File.WriteAllBytes(fileName, bodyData);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            fs.Write(bodyData, 0, bodyData.Length-1); // For Lua 4.0, this is necessary. Lua 5 handles the extra byte and is fine with it gone
            fs.Close();
            
            string output = Program.RunCommand(Program.Luac, " -l " + fileName, true);
            string retVal = string.Format("\n-- {0}\n-- luac -l listing \n{1}", c.Name, output);
            return retVal;
        }

        internal static string Decompile(Chunk c)
        {
            string retVal = null;
            string targetFile = Program.TmpDir + "tmp.lua";
            if( File.Exists(targetFile))
                File.Delete(targetFile);

            byte[] bodyData = c.GetBodyData();
            string fileName = Program.TmpDir + "tmp.luac";
            FileStream fs = new FileStream(fileName, FileMode.Create);
            fs.Write(bodyData, 0, bodyData.Length - 1); // For Lua 4.0, this is necessary. Lua 5 handles the extra byte and is fine with it gone
            fs.Close();
            if (Program.Luac.IndexOf("BFBuilder",  StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                retVal = DecompileBF1Code(fileName);
            }
            else
            {
                string prog = "SWBF2CodeHelper.exe";
                string args = " " + fileName;
                string output = Program.RunCommand(prog, args, true);
                
                if (!File.Exists(targetFile))
                    return "Error! Could not find output!\n" + output;
                retVal = File.ReadAllText(targetFile);
            }
            return retVal;
        }

        private static string DecompileBF1Code(string fileName)
        {
            // at this point Temp\tmp.luac has been written 
            // first get the listing, then run the listing through Phantom's tool

            string prog = Program.Luac;
            string args = " -l " + fileName;
            string output = Program.RunCommand(prog, args, true);
            
            string listFile = Program.TmpDir + "luac.list";
            File.WriteAllText(listFile, output);

            prog = "LuaDC1.exe";
            args = listFile +" -o "+ Program.TmpDir + "tmp.lua";
            output = Program.RunCommand(prog, args, true);

            string retVal = File.ReadAllText(Program.TmpDir + "tmp.lua");
            return retVal;
        }
    }
}
