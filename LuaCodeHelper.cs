using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
            string sourceFileName = FindSourceFile(chunk.Name);
            string code = LookupPCcode(sourceFileName);
            int sz = LuacCodeSize(sourceFileName);
            byte[] body = chunk.GetBodyData();
            if ( (body.Length -1) == sz )
            {
                retVal += "\n-- ********* LUAC Code Size MATCH!!! ***********";
                byte[] b = File.ReadAllBytes(".\\tmp.luac");
                
                int i = 0;
                for (i = 0; i < body.Length-1; i++)
                    if (b[i] != body[i])
                        break;
                if (i == body.Length -1)
                    retVal += "\n-- ********* Binary Equal !!! ***********";
            }
            retVal = retVal + string.Format("\n-- {0}\n-- PC luac code size = {1}; PC code:\n{2}", sourceFileName, sz, code);
            return retVal;
        }

        public static string LookupPCcode(string fileName)
        {
            string retVal = "";
            string sourceFile = FindSourceFile(fileName);
            if (!String.IsNullOrEmpty(sourceFile))
            {
                retVal = File.ReadAllText(sourceFile);
            }
            return retVal;
        }

        public static string FindSourceFile(string fileName)
        {
            string sourceFile = null;
            if (fileName != null)
            {
                if (sAllLuaFiles == null)
                {
                    while (!Directory.Exists(Program.ModToolsDir))
                    {
                        if (String.IsNullOrEmpty(Program.ModToolsDir))
                            return null;
                        if (!Program.ModToolsDir.EndsWith("\\")) Program.ModToolsDir += "\\";
                    }
                    sAllLuaFiles = new List<string>();
                    
                    sAllLuaFiles.AddRange(Directory.GetFiles(Program.ModToolsDir  + "assets\\", "*.lua", SearchOption.AllDirectories));
                    sAllLuaFiles.AddRange(Directory.GetFiles(Program.ModToolsDir + "data\\Common\\", "*.lua", SearchOption.AllDirectories));

                    if (Directory.Exists(Program.ModToolsDir + "TEMPLATE\\Common\\scripts\\"))
                    {
                        sAllLuaFiles.AddRange(Directory.GetFiles(Program.ModToolsDir + "TEMPLATE\\Common\\scripts\\", "*.lua", SearchOption.AllDirectories));
                    }
                    if (Directory.Exists(Program.ModToolsDir + "space_template\\"))
                    {
                        sAllLuaFiles.AddRange(Directory.GetFiles(Program.ModToolsDir + "space_template\\", "*.lua", SearchOption.AllDirectories));
                    }
                }

                string searchFor = fileName.EndsWith(".lua") ? fileName : fileName + ".lua";
                foreach (string file in sAllLuaFiles)
                {
                    if (file.EndsWith(searchFor, StringComparison.InvariantCultureIgnoreCase))
                    {
                        sourceFile = file;
                        break;
                    }
                }
            }
            return sourceFile;
        }

        public static int LuacCodeSize(string luaSourceFile)
        {
            int retVal = -1;
            string result = Program.RunCommand(Program.Luac, " -s -o tmp.luac " + luaSourceFile, true);
            if (result.Length < 10)
            {
                FileInfo info = new FileInfo(".\\tmp.luac");
                retVal = (int)info.Length;
            }
            return retVal;
        }

        internal static string GetLuacListing(Chunk c)
        {
            byte[] bodyData = c.GetBodyData();
            string fileName = ".\\decompile.luac";
            File.WriteAllBytes(fileName, bodyData);
            string output = Program.RunCommand(Program.Luac, " -l " + fileName, true);
            string retVal = string.Format("\n-- {0}\n-- luac -l listing \n{1}", c.Name, output);
            return retVal;
        }

        internal static string Decompile(Chunk c)
        {
            string targetFile = ".\\tmp.lua";
            if( File.Exists(targetFile))
                File.Delete(targetFile);

            byte[] bodyData = c.GetBodyData();
            string fileName = ".\\tmp.luac";
            File.WriteAllBytes(fileName, bodyData);
            string prog = "SWBF2CodeHelper.exe";
            string args = " " + fileName;
            string output = Program.RunCommand(prog, args, true);
            if (output.IndexOf("Error", StringComparison.InvariantCultureIgnoreCase) > -1)
                return output;
            if (!File.Exists(targetFile))
                return "Error! Could not find output!";
            string content = File.ReadAllText(targetFile);
            return content;
        }
    }
}
