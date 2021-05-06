using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace LVLTool
{
    static class Program
    {
        // default values
        private static string input_lvl  = null;
        private static string output_lvl = null;
        private static string files = null;
        private static Platform platform = Platform.PC;
        private static OperationMode operation = OperationMode.ShowHelp;
        private static string merge_strings_search_dir = null;

        public static bool Verbose = false;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            LoadSettings();
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                try
                {
                    ProcessArgs(args);
                }
                catch (Exception)
                {
                    Console.Error.WriteLine("Error! Check arguments!");
                    return;
                }
                string[] files_ = GetFiles(files);
                switch (operation)
                {
                    case OperationMode.ShowHelp:
                        PrintHelp();
                        break;
                    case OperationMode.Extract:
                        if (input_lvl != null)
                        {
                            UcfbHelper helper = new UcfbHelper();
                            helper.FileName = input_lvl;
                            helper.ExtractContents();
                        }
                        else
                            Console.WriteLine("Error! Must specify input file");
                        break;
                    case OperationMode.Add:
                        if (output_lvl == null)
                            output_lvl = input_lvl;
                        if (input_lvl != null)
                        {
                            UcfbHelper helper = new UcfbHelper();
                            helper.FileName = input_lvl;
                            foreach (string f in files_)
                            {
                                string fileName = Munger.EnsureMungedFile(f, platform);
                                helper.AddItemToEnd(fileName);
                            }
                        }
                        else
                            Console.WriteLine("Error! Must specify input file");
                        break;
                    case OperationMode.Replace:
                        if (output_lvl == null)
                            output_lvl = input_lvl;
                        if (input_lvl != null)
                        {
                            ReplaceItem_WithForm(input_lvl, output_lvl, files_);
                        }
                        else
                            Console.WriteLine("Error! Must specify input file");
                        break;
                    case OperationMode.ListContents:
                        UcfbHelper listHelper = new UcfbHelper();
                        listHelper.FileName = input_lvl;
                        listHelper.InitializeRead();
                        Chunk cur = null;
                        while ((cur = listHelper.RipChunk(false)) != null)
                        {
                            Console.WriteLine("{0}.{1}", cur.Name, cur.Type);
                        }
                        break;
                    case OperationMode.MergeCore:
                        List<string> theFiles = new List<string>(Directory.GetFiles(
                            merge_strings_search_dir, "core.lvl", SearchOption.AllDirectories));

                        //english,spanigh,italian,french,german,japanese,uk_english 
                        theFiles.AddRange(Directory.GetFiles(merge_strings_search_dir, "english.txt", SearchOption.AllDirectories));
                        theFiles.AddRange(Directory.GetFiles(merge_strings_search_dir, "spanish.txt", SearchOption.AllDirectories));
                        theFiles.AddRange(Directory.GetFiles(merge_strings_search_dir, "italian.txt", SearchOption.AllDirectories));
                        theFiles.AddRange(Directory.GetFiles(merge_strings_search_dir, "french.txt", SearchOption.AllDirectories));
                        theFiles.AddRange(Directory.GetFiles(merge_strings_search_dir, "german.txt", SearchOption.AllDirectories));
                        theFiles.AddRange(Directory.GetFiles(merge_strings_search_dir, "japanese.txt", SearchOption.AllDirectories));
                        theFiles.AddRange(Directory.GetFiles(merge_strings_search_dir, "uk_english.txt", SearchOption.AllDirectories));

                        CoreMerge.MergeLoc(input_lvl, output_lvl, theFiles);
                        break;
                    case OperationMode.PS2_BF1AddonScript:
                        // This code operates as expected; but currently the PS2 crashes when reading the generated .lvl .
                        string[] neededFiles = new String[] { 
                            ".\\bin\\luac_4.exe",
                            ".\\bin\\BF1_LevelPack.exe",
                            ".\\bin\\BF1_ScriptMunge.exe"
                        };
                        foreach (string nf in neededFiles)
                        {
                            if (!File.Exists(nf))
                            {
                                MessageBox.Show("Required files for operation:\r\n"+String.Join("\r\n", neededFiles), "Error, missing required files");
                                return;
                            }
                        }
                        StringBuilder builder = new StringBuilder();
                        builder.Append("print('PROCESS_ADDONS_PS2_BF1 start', 'debug')\r\n");

                        //string debugFile = "DEBUG\\DEBUG.lvl";
                        //if (File.Exists(debugFile))
                        //{
                        //    builder.Append("  ReadDataFile('addon\\debug\\debug.lvl'); \r\n");
                        //    builder.Append("  ScriptCB_DoFile('debug'); \r\n");
                        //}
                        string current = "";

                        for (int i = 0; i < 1000; i++)
                        {
                            current = string.Format("{0:D3}\\ADDON{0:D3}.lvl", i);
                            if (File.Exists(current))
                            {
                                builder.Append(String.Format("  ReadDataFile('addon\\{0:D3}\\addon{0:D3}.lvl'); \r\n", i));
                                builder.Append(String.Format("  ScriptCB_DoFile('addon{0:D3}'); \r\n", i));
                            }
                        }
                        builder.Append("print('PROCESS_ADDONS_PS2_BF1 end', 'debug')");

                        // copy to 'PROCESS_ADDONS_PS2_BF1.LUA'
                        String ps2LuaFileName = "PROCESS_ADDONS_PS2_BF1.LUA";
                        
                        File.WriteAllText(Path.GetFullPath( ps2LuaFileName), builder.ToString());
                        // create the lvl with the 'CreateLvlForm'
                        CreateLvlForm lvlMaker = new CreateLvlForm();
                        lvlMaker.OverrideScriptMunge = Path.GetFullPath( "bin\\BF1_ScriptMunge.exe");
                        lvlMaker.OverrideLevelPack = Path.GetFullPath( "bin\\BF1_LevelPack.exe");
                        File.Copy("bin\\luac_4.exe", ".\\luac.exe", true); // ensure lua4 for BF1
                        lvlMaker.AddItem(ps2LuaFileName);
                        try
                        {
                            lvlMaker.CreateLvl(".\\", "PROCESS_ADDONS_PS2_BF1.LVL", "PS2", true);
                            RemoveLogFiles();
                        }
                        catch (Exception lvlExc)
                        {
                            Console.WriteLine("Error Creating PROCESS_ADDONS_PS2_BF1.LVL!" + lvlExc.Message);
                        }
                        finally
                        {
                            File.Delete(".\\luac.exe");
                            lvlMaker.Dispose();
                        }
                        break;
                }
            }
        }

        private static void ReplaceItem_WithForm(string inputLvlName,string outputLvlName, string[] files_)
        {
            MainForm form = new MainForm();// lame to use the form, but it works.
            int count = form.SetLvlFile(inputLvlName);
            if (count < 1)
            {
                Console.WriteLine("Error! nothing found in input file!");
                return;
            }
            if (form.ReplaceItem(files_[0], platform))
                Console.WriteLine("Replaced Item with " + files_[0]);
            else
            {
                Console.WriteLine("Error during file replacement, output not saved");
                return;
            }
            form.SaveLvl(outputLvlName);
            Console.WriteLine("Saved to: " + outputLvlName);
        }

        private static void RemoveLogFiles()
        {
            string[] logFiles = Directory.GetFiles(".", "*.log", SearchOption.TopDirectoryOnly);
            foreach (string log in logFiles)
            {
                File.Delete(log);
            }
        }

        public static Chunk FindChunk(string lvlFile, string name)
        {
            Chunk cur = null;
            UcfbHelper helper = new UcfbHelper();
            helper.FileName = lvlFile;
            helper.InitializeRead();
            while ((cur = helper.RipChunk(false)) != null)
            {
                if (cur.Name == name)
                    return cur;
            }
            return null;
        }

        private static string GetChunkName(string fileName)
        {
            int start = fileName.LastIndexOf(Path.DirectorySeparatorChar) + 1;
            int end = fileName.LastIndexOf('.');
            if (end < start)
                end = fileName.Length - 1;
            string retVal = fileName.Substring(start, end - start);
            return retVal;
        }

        private static string[] GetFiles(string files)
        {
            string[] retVal = null;
            if (files != null)
            {
                retVal = files.Split(";".ToCharArray());
            }
            return retVal;
        }

        private static void ProcessArgs(string[] args)
        {
            int i = 0;
            for (; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "-file":
                        input_lvl = args[i + 1];
                        i++;
                        break;
                    case "-o":
                        output_lvl = args[i + 1]; 
                        i++;
                        break;
                    case "-r":
                        operation = OperationMode.Replace;
                        files = args[i + 1];
                        i++;
                        break;
                    case "-a":
                        operation = OperationMode.Add;
                        files = args[i + 1];
                        i++;
                        break;
                    case "-e":
                        operation = OperationMode.Extract;
                        break;
                    case "-h":
                    case "--help":
                    case "/h":
                    case "/?":
                        operation = OperationMode.ShowHelp;
                        break;
                    case "-p":
                        string p = args[i + 1].ToUpper();
                        platform = (Platform) Enum.Parse(typeof(Platform), p);
                        i++;
                        break;
                    case "-l":
                        operation = OperationMode.ListContents;
                        break;
                    case "-verbose":
                        Verbose = true;
                        break;
                    case "-merge_strings":
                        operation = OperationMode.MergeCore;
                        merge_strings_search_dir = args[i + 1];
                        i++;
                        break;
                    case "-ps2_bf1_addon_lvl":
                        operation = OperationMode.PS2_BF1AddonScript;
                        i++;
                        break;
                }
            }
        }

        public static void PrintHelp()
        {
            string help = "LVLTool.exe  Version: "+Version +"\n"+
@"Use without arguments for the GUI.
Use at the command line with the following arguments:
 -file <lvl file>  The file to operate on
 -r <munged or mungable file(s)>  to replace munged files in the target .lvl file.
 -a <munged or mungable file(s)>  to add munged files at the end of the target .lvl file.
 -o <lvl file>  The output file name. (default is input lvl file)
 -e             Extract contents
 -p <platform> (pc|xbox|ps2) only important if specifying .tga files (pc=default)
 -l List the contents of the munged/lvl file.
 -merge_strings <search_dir> Read in the '-file' arg, add the strings found in the 'core.lvl's 
               under 'search_dir' save to file specified with the '-o' arg.
 " + //"-ps2_bf1_addon_lvl  Create a PS2 Battlefront alt addon (workaround) lvl for PS2 BF1 (PS2 BF1 only)
@"
 -verbose       Display more info as operations are occuring.
 -h --help  /? Print help message

Examples:
    (Replace shell_interface script inside shell.lvl)
LVLTool.exe -file shell.lvl -r shell_interface.lua 
     or 
LVLTool.exe -file shell.lvl -r shell_interface.script
 
    (extract all (munged) contents from shell.lvl)
LVLTool.exe -file shell.lvl -e
 
    (Add XXXg_con and XXXc_con to the end of mission.lvl)
LVLTool.exe -file mission.lvl -a XXXg_con.script;XXXc_con.script

    (Replace mode_icon_con in xbox shell.lvl )
LVLTool.exe -file shell.lvl -r mode_icon_con.tga -p xbox  

    (add the strings from core.lvl files under 'top_folder_with_cores', to 'base.core.lvl', save as 'core.lvl' )
LVLTool.exe -file base.core.lvl -o core.lvl -merge_strings top_folder_with_cores

";
            Console.WriteLine(help);
        }

        public static string Luac
        {
            get
            {
                string retVal = ModToolsDir + "ToolsFL\\bin\\Luac.exe";
                return retVal;
            }
        }

        public static bool COMMAND_LINE = false;

        public static string settignsFile = ".\\.lvlToolSettings";

        public static string ModToolsDir = "C:\\BF2_ModTools\\";

        public static string TmpDir {
            get
            {
                string retVal = ".\\Temp\\";
                if (!Directory.Exists(retVal))
                    Directory.CreateDirectory(retVal);
                return retVal;
            }
        }

        public static string Version { get { return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString(); } }

        public static void MessageUser(string message)
        {
            if (COMMAND_LINE)
                Console.WriteLine(message);
            else
                MessageBox.Show(message);
        }

        public static string RunCommand(string programName, string args, bool includeStdErr)
        {
            Console.WriteLine("Running command: " + programName + " " + args);
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = programName,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            var process = Process.Start(processStartInfo);
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            process.WaitForExit();
            if (includeStdErr)
                output = output + "\r\n" + err;
            return output;
        }

        private static Regex ModToolsRegex = new Regex("ModToolsDir:(.*)");

        public static void SaveSettings()
        {
            string contents = string.Format("ModToolsDir:{0}\n", ModToolsDir);
            File.WriteAllText(settignsFile, contents);
        }

        public static void LoadSettings()
        {
            if (File.Exists(settignsFile))
            {
                string contents = File.ReadAllText(settignsFile);
                Match m = ModToolsRegex.Match(contents);
                if (m != Match.Empty)
                {
                    ModToolsDir = m.Groups[1].ToString();
                }
            }
        }
    }

    public enum Platform
    {
        None,
        PC,
        XBOX,
        PS2
    }

    public enum OperationMode
    {
        Extract,
        Add,
        Replace,
        ListContents,
        ShowHelp,
        MergeCore,
        PS2_BF1AddonScript
    }
}
