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
            string retVal = fileName;

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
            List<string> mungableFiles = new List<string>() {
                ".tga",  ".lua",
                ".mcfg", ".fx",  ".prp", ".bnd", ".snd", ".mus",
                ".combo",".sanm",".hud", ".cfg", ".pth", ".sky",
                ".lgt",  ".pvs", ".tsr",
            };
            retVal = mungableFiles.IndexOf(suffix) > -1;
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

                case ".mcfg": case ".fx":  case ".prp":   case ".bnd":
                case ".snd":  case ".mus": case ".combo": case ".sanm":
                case ".hud":  case ".cfg": case ".pth":   case ".sky":
                case ".lgt":  case ".pvs": case ".tsr":
                    retVal = ConfigMunge(file);
                    break;
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
                string prog = Path.GetFullPath( Program.ModToolsDir + "\\ToolsFL\\bin\\pc_TextureMunge.exe");
                if( !File.Exists(prog))
                    prog = Path.GetFullPath( Program.ModToolsDir + "\\ToolsFL\\bin\\TextureMunge.exe");
                if (!File.Exists(prog))
                    throw new Exception(
                        "Could not find texture munge program; ensure modtools directory is set correctly");
                if( platform == Platform.None)
                    platform = PlatformPrompt.PromptForPlatform();

                if (platform != Platform.None)
                {
                    FileInfo tmpFileInfo = new FileInfo(tmpFile);
                    string args = String.Format(
                        "-sourcedir {0} -platform {1} -inputfile {2} -outputdir {3}",
                        Program.TmpDir, platform.ToString(), tmpFileInfo.Name, Program.TmpDir);

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
                string prog = Path.GetFullPath( Program.ModToolsDir + "\\ToolsFl\\bin\\ScriptMunge.exe");
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
        /// <summary>
        /// Munges the Given File (ConfigMunge).
        /// </summary>
        /// <remarks>Check command terminal for error messages when munging the file.</remarks>
        /// <param name="file">Path of the file to munge</param>
        /// <returns>The path to the munged file, null if there was an error munging the file.</returns>
        private static string ConfigMunge(string file)
        {
            string retVal = null;
            if (File.Exists(file))
            {
                int index = file.LastIndexOf('\\');
                string tmpFile = Program.TmpDir + file.Substring(index + 1);
                File.Copy(file, tmpFile, true);
                string prog = Path.GetFullPath(Program.ModToolsDir + "\\ToolsFl\\bin\\ConfigMunge.exe");
                string args = GetConfigMungeArgs(tmpFile);
                string outputExt = ".config";
                int extIndex = args.IndexOf("-ext ");
                if (extIndex > -1)
                {
                    // this functionality relies on the '-ext' arg being the last arg.
                    outputExt = "." + args.Substring(extIndex+5).Trim();
                }
                if(Program.Verbose)
                    Console.WriteLine("info: ConfigMunge; outputExt= " + outputExt);

                string programOutput = Program.RunCommand(prog, args, true);
                FileInfo fi = new FileInfo(file);
                string inputExt = fi.Extension;
                if (Program.Verbose)
                    Console.WriteLine("info: ConfigMunge; inputExt= " + inputExt);
                string outputFile = tmpFile.Replace(inputExt, outputExt);
                if (Program.Verbose)
                    Console.WriteLine("info: ConfigMunge; outputFile= " + outputFile);
                if (File.Exists(outputFile))
                {
                    retVal = outputFile;
                }
                Console.WriteLine(programOutput);
            }
            return retVal;
        }

        /// <summary>
        /// Gets the arguments for the given file type; based on the BF1 and BF2 usage of 'ConfigMunge.exe' in
        /// the batch files.
        /// </summary>
        public static string GetConfigMungeArgs(string fileName)
        {
            List<String> bf1_hash_strings_files = new List<string>(){ ".mcfg", ".snd", ".mus", ".prp", ".bnd", ".tsr" };
            List<String> bf2_hash_strings_files = new List<string>() { ".mcfg",".snd", ".mus", ".prp", ".bnd" };

            Dictionary<string, string> bf1_chunkids = new Dictionary<string,string> {
                            {".sky", "sky"}, {".prp", "prp"}, {".fx","fx"},
                            {".bnd", "bnd"},
            };
            Dictionary<string, string> bf2_chunkids = new Dictionary<string, string> {
                            {".cfg", "load"}, {".pth", "path"},  {".sky", "sky"},
                            {".fx","fx"},     {".prp", "prp"},   {".bnd", "bnd"},
                            {".lgt","lght"},  {".pvs", "PORT"}
            };

            Dictionary<string, string> bf1_exts = new Dictionary<string, string> {
                            {".fx", "envfx"}, {".prp", "prop"}, {".bnd", "boundary"}
            };
            Dictionary<string, string> bf2_exts = new Dictionary<string, string> {
                            {".cfg", "config"},   {".pth", "path"}, {".fx", "envfx"}, {".prp", "prop"},
                            {".bnd", "boundary"}, {".lgt", "light"},{".pvs", "povs"}
            };
            FileInfo fi = new FileInfo(fileName);
            string endsWith = fi.Extension;
            string retVal = string.Format("-inputfile *{0} -continue -platform pc -sourcedir {1} -outputdir {1} ",
                endsWith, Program.TmpDir);

            if (IsCurrentConfig_BF2())
            {
                if (bf2_hash_strings_files.IndexOf(endsWith) > -1)
                    retVal += "-hashstrings ";
                if (bf2_chunkids.ContainsKey(endsWith))
                    retVal += string.Format("-chunkid {0} ",bf2_chunkids[endsWith]);
                if (bf2_exts.ContainsKey(endsWith))
                    retVal += string.Format("-ext {0} ", bf2_exts[endsWith]); // the '-ext' arg needs to be last.
            }
            else
            {
                if (bf1_hash_strings_files.IndexOf(endsWith) > -1)
                    retVal += "-hashstrings ";
                if (bf1_chunkids.ContainsKey(endsWith))
                    retVal += string.Format("-chunkid {0} ", bf1_chunkids[endsWith]);
                if (bf1_exts.ContainsKey(endsWith))
                    retVal += string.Format("-ext {0} ", bf1_exts[endsWith]); // the '-ext' arg needs to be last.
            }
            return retVal;
        }

        private static bool IsCurrentConfig_BF2()
        {
            // only BF2 has the 'swbf2rm' program (whatever that is).
            string check_for = Program.ModToolsDir + "\\ToolsFL\\bin\\swbf2rm.exe";
            bool retVal = File.Exists(check_for);
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
