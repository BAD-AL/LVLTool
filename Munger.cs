using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LVLTool
{
    public static class Munger
    {
        /// <summary>
        /// Throws exception if the selected file is not UCFB or not mungable
        /// (currently tga & lua).
        /// </summary>
        /// <param name="prompt">The text to prompt the user with.</param>
        /// <returns>The munged file name.</returns>
        public static string GetMungedFile( string prompt )
        {
            string retVal = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Title = prompt;
            if (dlg.ShowDialog() == DialogResult.OK)
                retVal = dlg.FileName;
            dlg.Dispose();

            if (retVal != null)
            {
                if (IsMungableFile(retVal))
                    retVal = MungeFile(retVal, Platform.None);

                VerifyUcfbFile(retVal); // throws exception if it's not a UCFB file
            }
            return retVal;
        }

        public static string EnsureMungedFile(string fileName, Platform platform)
        {
            string retVal = null;

            if (IsMungableFile(fileName))
                retVal = MungeFile(fileName, platform);

            VerifyUcfbFile(retVal); // throws exception if it's not a UCFB file
            return retVal;
        }

        private static bool IsMungableFile(string file)
        {
            bool retVal = false;
            int dotIndex = file.LastIndexOf('.');
            string suffix = file.Substring(dotIndex).ToLower();

            switch (suffix)
            {
                case ".tga":
                case ".lua":
                    retVal = true;
                    break;
            }
            return retVal;
        }

        private static string MungeFile(string file, Platform platform)
        {
            string retVal = null;
            int dotIndex = file.LastIndexOf('.');
            string suffix = file.Substring(dotIndex).ToLower();
            if (!Directory.Exists(Program.TmpDir))
                Directory.CreateDirectory(Program.TmpDir);

            switch (suffix)
            {
                case ".tga": retVal = TextureMunge(file, platform); break;
                case ".lua": retVal = ScriptMunge(file);  break;
            }
            return retVal;
        }

        private static string TextureMunge(string file, Platform platform)
        {
            string retVal = null;
            if (File.Exists(file))
            {
                int index = file.LastIndexOf('\\');
                string tmpFile = Program.TmpDir + file.Substring(index + 1);
                File.Copy(file, tmpFile, true);
                string prog = Program.ModToolsDir + "ToolsFL\\bin\\pc_TextureMunge.exe";
                if( !File.Exists(prog))
                    prog = Program.ModToolsDir + "ToolsFL\\bin\\TextureMunge.exe";
                if (!File.Exists(prog))
                    throw new Exception(
                        "Could not find texture munge program; ensure modtools directory is set correctly");
                if( platform == Platform.None)
                    platform = PlatformPrompt.PromptForPlatform();

                if (platform != Platform.None)
                {
                    string args = String.Format(
                        "-sourcedir {0} -platform {1} -inputfile {2} -outputdir {3}",
                        Program.TmpDir, platform.ToString(), tmpFile, Program.TmpDir);

                    string programOutput = Program.RunCommand(prog, args, true);
                    string outputFile = tmpFile.Replace(".tga", ".texture");
                    if (File.Exists(outputFile))
                    {
                        retVal = outputFile;
                    }
                    Console.WriteLine(programOutput);
                }
            }
            return retVal;
        }
        /*
        scriptmunge -sourcedir . -platform xbox -inputfile addme.lua -outputdir munged\ -checkdate -continue
        copy munged\addme.script ..\_LVL_XBOX\
         */
        private static string ScriptMunge(string file)
        {
            string retVal = null;
            if (File.Exists(file))
            {
                int index = file.LastIndexOf('\\');
                string tmpFile = Program.TmpDir + file.Substring(index + 1);
                File.Copy(file, tmpFile, true);
                string prog = Program.ModToolsDir + "ToolsFl\\bin\\ScriptMunge.exe";
                string args = String.Format(
                    // for scripts, platform doesn't matter; just use pc here
                    "-sourcedir {0} -platform pc -inputfile {1} -outputdir {2}",
                    ".",//Program.TmpDir, 
                    tmpFile, Program.TmpDir);

                string programOutput =  Program.RunCommand(prog, args, true);
                string outputFile = tmpFile.Replace(".lua", ".script");
                if (File.Exists(outputFile))
                {
                    retVal = outputFile;
                }
                Console.WriteLine(programOutput);
            }
            return retVal;
        }


        public static void VerifyUcfbFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            byte[] buff = new byte[8];
            fs.Read(buff, 0, buff.Length);
            fs.Close();
            byte[] UCFB = new byte[]{(byte)'u', (byte)'c', (byte)'f', (byte)'b'};
            if (!BinUtils.BinCompare(UCFB, buff, 0))
                throw new Exception("Error! File is not UCFB! " + filename);
        }

    }
}
