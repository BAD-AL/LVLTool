using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LVLTool
{
    public partial class CreateLvlForm : Form
    {
        public string OverrideScriptMunge = null;
        public string OverrideLevelPack = null;
        
        public CreateLvlForm()
        {
            InitializeComponent();
            mPlatformComboBox.SelectedIndex = 0;
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            //https://stackoverflow.com/questions/57124243/winforms-dark-title-bar-on-windows-10 (thank you:)
            if (DwmSetWindowAttribute(this.Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(this.Handle, 20, new[] { 1 }, 4);
            base.OnHandleCreated(e);
        }

        [System.Runtime.InteropServices.DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        private bool AlreadyMunged(string filename)
        {
            string[] alreadyMungedExt = new string[] { ".texture", ".script", ".lvl", ".config" };
            foreach (string ext in alreadyMungedExt)
            {
                if (filename.ToLower().EndsWith(ext))
                    return true;
            }
            return false;
        }

        internal void SetStyle(Form prevForm)
        {
            this.BackColor = prevForm.BackColor;
            this.ForeColor = prevForm.ForeColor;

            TextBox tb = null;
            Button b = null;
            MenuStrip ms = null;
            foreach (Control tmp in prevForm.Controls)
            {
                if (tb == null)
                    tb = tmp as TextBox;
                if (b == null)
                    b = tmp as Button;
                if (ms == null)
                    ms = tmp as MenuStrip;
                if (tb != null && b != null && ms != null)
                    break;
            }
            if (tb != null)
            {
                this.mListBox.BackColor = mOutputDirTextbox.BackColor = mOutputFilenameTextbox.BackColor =
                    tb.BackColor;
                this.mListBox.ForeColor = mOutputDirTextbox.ForeColor = mOutputFilenameTextbox.ForeColor =
                    tb.ForeColor;
            }
            if (b != null)
            {
                mClearButton.BackColor = mPlatformComboBox.BackColor = mCreateButton.BackColor = b.BackColor;
                mClearButton.ForeColor = mPlatformComboBox.ForeColor = mCreateButton.ForeColor = b.ForeColor;
            }
            if (ms != null)
            {
                this.menuStrip1.ForeColor = ms.ForeColor;
                this.menuStrip1.BackColor = ms.BackColor;
            }
        }

        private void mListBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void mListBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            StringBuilder errList = new StringBuilder();
            int lastSlash = 0;
            if (files != null  )
            {
                for (int i = 0; i < files.Length; i++)
                {
                    if (!AddItem(files[i]))
                    {
                        lastSlash = files[i].LastIndexOf("\\") + 1;
                        if (lastSlash > 0)
                        {
                            errList.Append("  ");
                            errList.Append(files[i].Substring(lastSlash));
                            errList.Append("\n");
                        }
                    }
                }
                if (errList.Length > 0)
                {
                    MessageBox.Show(String.Format("Error:\n{0}Currently only .tga, .texture, .script and .lua files are supported", 
                        errList.ToString()));
                }
            }
        }

        public bool AddItem(string file)
        {
            bool retVal = false;
            if ( file.EndsWith(".lua",     StringComparison.InvariantCultureIgnoreCase) ||
                 file.EndsWith(".tga",     StringComparison.InvariantCultureIgnoreCase) ||
                 file.EndsWith(".script",  StringComparison.InvariantCultureIgnoreCase) ||
                 file.EndsWith(".texture", StringComparison.InvariantCultureIgnoreCase) ||
                 file.EndsWith(".config",  StringComparison.InvariantCultureIgnoreCase) ||
                 file.EndsWith(".mcfg",    StringComparison.InvariantCultureIgnoreCase) ||
                 file.EndsWith(".lvl" ,    StringComparison.InvariantCultureIgnoreCase) 
                )
            {
                MungableItem dude = new MungableItem(file);
                mListBox.Items.Add(dude);
                if (mListBox.Items.Count == 1 && mOutputFilenameTextbox.Text == "")
                {
                    mOutputFilenameTextbox.Text = dude.MungedName + ".lvl";
                    mOutputDirTextbox.Text = dude.Path.Replace(dude.FileName, "");
                }
                retVal = true;
            }
            return retVal;
        }

        public void CreateLvl(string outputPath, string saveFileName, string platform, bool supressPopups )
        {
            if(! outputPath.EndsWith("\\"))
                outputPath += "\\";

            string workspace = outputPath + ".\\__LVLTOOL__workspace__\\";
            string munge_dir = outputPath + ".\\__LVLTOOL__workspace__\\MUNGED\\";
            string saveFilePath = outputPath + saveFileName;
            if (Directory.Exists(workspace) )
            {
                Directory.Delete(workspace, true);
            }
            Directory.CreateDirectory(workspace);
            Directory.CreateDirectory(munge_dir);
            string reqContent = GetReqString();
            string reqFileName = mOutputFilenameTextbox.Text.ToLower().Replace(".lvl", ".req");
            File.WriteAllText(workspace + reqFileName, reqContent);
            
            // now copy the files over to the workspace
            foreach (MungableItem item in mListBox.Items)
            {
                if( AlreadyMunged(item.Path))
                    File.Copy(item.Path, munge_dir + item.FileName);
                else 
                    File.Copy(item.Path, workspace + item.FileName);
            }
            // now munge & pack

            string configMunge  = Program.ModToolsDir + "ToolsFL\\bin\\ConfigMunge.exe";
            string textureMunge = Program.ModToolsDir + "ToolsFL\\bin\\pc_TextureMunge.exe";
            string scriptMunge  = Program.ModToolsDir + "ToolsFL\\bin\\ScriptMunge.exe";
            string lvlPack      = Program.ModToolsDir + "ToolsFL\\bin\\levelpack.exe";

            if (OverrideLevelPack != null)
                lvlPack = OverrideLevelPack;
            if (OverrideScriptMunge != null)
                scriptMunge = OverrideScriptMunge;
            
            string req_file_noext = reqFileName.Replace(".req", "");
            string configArgs = string.Format(
                                " -inputfile $*.mcfg -continue  -QUIET -platform {0} -sourcedir  {1} -outputdir {2} -hashstrings ",
                                platform, workspace, munge_dir);
            string scriptArgs = string.Format(
                                " -inputfile *.lua -continue  -QUIET -platform {0} -sourcedir  {1} -outputdir {2} ", 
                                platform, Path.GetFullPath( workspace), munge_dir);
            string texArgs = string.Format(
                                " -inputfile $*.tga -checkdate -continue -QUIET -platform {0} -sourcedir  {1} -outputdir {2}  ",
                                platform, workspace, munge_dir);
            string lvlPackArgs = string.Format(
                                "-inputfile {0}.req -writefiles {1}{0}.files -continue -QUIET -platform {2} -sourcedir  {3} -inputdir {1} -outputdir {3}  ",
                                req_file_noext, munge_dir, platform, Path.GetFullPath( workspace));
            //string programOutput = Program.RunCommand(program_exe, args, true);
            string programOutput = "";
            if( File.Exists(scriptMunge))
                programOutput += Program.RunCommand(scriptMunge, scriptArgs, true);
            if( File.Exists(configMunge))
                programOutput += Program.RunCommand(configMunge,  configArgs,  true);
            if( File.Exists(textureMunge))
                programOutput += Program.RunCommand(textureMunge, texArgs,     true);
            programOutput     += Program.RunCommand(lvlPack,      lvlPackArgs, true);
            string batchFileContents = string.Format(
                "md MUNGED \ndel /Y MUNGED\\*\n {0} {1}\n\n{2} {3}\n\n{4} {5}\n\n{6} {7}\n\nmove *.log MUNGED\n\n",
                textureMunge, String.Format("-inputfile $*.tga  -checkdate -continue -platform {0} -sourcedir . -outputdir MUNGED ", platform),
                scriptMunge,  String.Format("-inputfile *.lua   -continue -platform {0} -sourcedir  . -outputdir MUNGED  ", platform),
                configMunge,  String.Format("-inputfile $*.mcfg -continue -platform {0} -sourcedir . -outputdir MUNGED -hashstrings ",platform),
                lvlPack,      String.Format("-inputfile {0}.req -writefiles MUNGED\\{0}.files -continue -platform {1} -sourcedir  . -inputdir MUNGED\\ -outputdir . ", 
                                                req_file_noext, platform)
                );
            // copy output to user's choice directory
            string lvlOutput = workspace + req_file_noext + ".lvl";
            if (File.Exists(lvlOutput))
            {
                File.WriteAllText(workspace + "munge.bat", batchFileContents);
                File.WriteAllText(workspace + "munge.log", programOutput);
                File.Copy(lvlOutput, saveFilePath, true);
                if (!supressPopups)
                    MessageForm.ShowMessage("Saved lvl file", "Saved to: " + saveFilePath, SystemIcons.Information, false, false);
                else
                    Console.WriteLine("Saved lvl file to: " + saveFilePath);
                if (Directory.Exists(workspace)) // allow the user to rename in order to keep the workspace
                {
                    Directory.Delete(workspace, true);
                }
            }
            else
            {
                if( !supressPopups)
                    MessageForm.ShowMessage("Error creating lvl file", "Something went wrong:\n" + programOutput);
                else
                    Console.WriteLine("Error creating lvl file :\n" + programOutput);
            }
        }

        private string GetReqString()
        {
            string retVal = null;
            if (mListBox.Items.Count > 0)
            {
                List<MungableItem> configs = new List<MungableItem>();
                List<MungableItem> scripts = new List<MungableItem>();
                List<MungableItem> textures = new List<MungableItem>();
                List<MungableItem> lvls = new List<MungableItem>();
                StringBuilder builder = new StringBuilder(200);
                foreach( MungableItem item in mListBox.Items)
                {
                    if (item.Path.EndsWith(".tga", StringComparison.InvariantCultureIgnoreCase) || 
                        item.Path.EndsWith(".texture", StringComparison.InvariantCultureIgnoreCase))
                        textures.Add(item);
                    else if (item.Path.EndsWith(".lua", StringComparison.InvariantCultureIgnoreCase) ||
                             item.Path.EndsWith(".script", StringComparison.InvariantCultureIgnoreCase))
                        scripts.Add(item);
                    else if (item.Path.EndsWith(".mcfg", StringComparison.InvariantCultureIgnoreCase) || // TODO: add the other config types
                             item.Path.EndsWith(".config", StringComparison.InvariantCultureIgnoreCase))
                        configs.Add(item);
                    else if (item.Path.EndsWith(".lvl", StringComparison.InvariantCultureIgnoreCase) )
                        lvls.Add(item);
                }
                builder.Append("ucft\n{\n");
                if (configs.Count > 0)
                {
                    builder.Append("  REQN\n");
                    builder.Append("  {\n");
                    builder.Append("    \"config\"\n");
                    for (int i = 0; i < configs.Count; i++)
                    {
                        builder.Append(string.Format(
                                   "    \"{0}\"\n", configs[i].MungedName));
                    }
                    builder.Append("  }\n");
                }
                if (scripts.Count > 0)
                {
                    builder.Append("  REQN\n");
                    builder.Append("  {\n");
                    builder.Append("    \"script\"\n");
                    for(int i =0; i< scripts.Count; i++)
                    {
                        builder.Append(string.Format(
                                   "    \"{0}\"\n", scripts[i].MungedName));
                    }
                    builder.Append("  }\n");
                }
                if (textures.Count > 0)
                {
                    builder.Append("  REQN\n");
                    builder.Append("  {\n");
                    builder.Append("    \"texture\"\n");
                    for (int i = 0; i < textures.Count; i++)
                    {
                        builder.Append(string.Format(
                                   "    \"{0}\"\n", textures[i].MungedName));
                    }
                    builder.Append("  }\n");
                }
                if (lvls.Count > 0)
                {
                    builder.Append("  REQN\n");
                    builder.Append("  {\n");
                    builder.Append("    \"lvl\"\n");
                    for (int i = 0; i < lvls.Count; i++)
                    {
                        builder.Append(string.Format(
                                   "    \"{0}\"\n", lvls[i].MungedName));
                    }
                    builder.Append("  }\n");
                }
                builder.Append("}");
                retVal = builder.ToString();
            }
            return retVal;
        }

        private void mCreateButton_Click(object sender, EventArgs e)
        {
            CreateLvl(mOutputDirTextbox.Text, mOutputFilenameTextbox.Text, mPlatformComboBox.SelectedItem.ToString(), false );
        }

        private void supportedFileTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("currently supports [.config, .mcfg, .tga, .texture, .lua, .script, .lvl] files ");
        }

        private void mClearButton_Click(object sender, EventArgs e)
        {
            mListBox.Items.Clear();
            mOutputFilenameTextbox.Clear();
            mOutputDirTextbox.Clear();
        }
    }

    public class MungableItem
    {
        public string Path       { get; private set; }
        public string FileName   { get; private set; }
        public string MungedName { get; private set; }

        public MungableItem(string path)
        {
            Path = path;
            int targetIndex = path.LastIndexOf('\\') + 1;
            FileName = path.Substring(targetIndex);
            targetIndex = FileName.LastIndexOf(".");
            MungedName = FileName.Substring(0, targetIndex);
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
