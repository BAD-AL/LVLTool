using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace LVLTool
{
    public partial class UnmungeForm : Form
    {
        public UnmungeForm()
        {
            InitializeComponent();
            PopulateListUnmunge();
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

        #region
        // repair known bugs (rename files [from hash], Fix Rotation in .lyr files)

        private void RenameHashNameFiles()
        {
            string folder = null;
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select folder to look for files with hash name as filename.";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                folder = dlg.SelectedPath;
            }
            dlg.Dispose();
            if (folder != null)
            {
                DirectoryInfo dInfo = new DirectoryInfo(folder);
                string newName = "";
                string checkName = "";
                Regex hash1Reg = new Regex("0x([0-9a-fA-F]+)");
                Regex hash2Reg = new Regex("([0-9]+)");
                UInt32 hash = 0;
                int index = 0;
                FileInfo[] files = dInfo.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo dude in files)
                {
                    index = dude.Name.LastIndexOf(".");
                    if (index > -1)
                    {
                        checkName = dude.Name.Substring(0, index);
                    }
                    if (hash1Reg.IsMatch(checkName) && UInt32.TryParse(checkName.Substring(2), 
                        System.Globalization.NumberStyles.AllowHexSpecifier, 
                        System.Globalization.CultureInfo.CurrentUICulture, out hash))
                    {
                        newName = HashHelper.GetStringFromHash(hash);
                        if (newName != null)
                        {
                            string path= dude.DirectoryName +"\\" + newName + dude.Extension;
                            dude.MoveTo(path);
                        }
                    }
                    else if (hash2Reg.IsMatch(checkName) && UInt32.TryParse(checkName, out hash))
                    {
                        newName = HashHelper.GetStringFromHash(hash);
                        if (newName != null)
                        {
                            string path = dude.DirectoryName + "\\" + newName + dude.Extension;
                            Console.WriteLine("Renaming file '{0}' to   '{1}' ", dude.FullName, path);
                            dude.MoveTo(path);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fix rotation in .lyr files
        ///   ChildRotation(1.000, 0.000, 0.000, -0.750); ==>> ChildRotation(-1.000, 0.000, 0.000, 0.750);
        /// </summary>
        private void FixRotationInLyr()
        {
            string folder = null;
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select folder to look for .lyr files with flipped Rotation numbers.";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                folder = dlg.SelectedPath;
            }
            dlg.Dispose();
            if (folder != null)
            {
                DirectoryInfo dInfo = new DirectoryInfo(folder);
                FileInfo[] files = dInfo.GetFiles("*.lyr", SearchOption.AllDirectories);
                String content = null;
                string contentAfter = "";
                foreach (FileInfo dude in files)
                {
                    content =  File.ReadAllText(dude.FullName);
                    contentAfter = FixRotation(content);
                    Console.WriteLine("Writing file: "+ dude.FullName);
                    File.WriteAllText(dude.FullName, contentAfter);
                }
            }
        }

        private string FixRotation(string content)
        {
            string replacement = "";
            string working ="";
            Regex childRotationReg = new Regex("ChildRotation\\((-?[0-9.]+),\\s*(-?[0-9.]+),\\s*(-?[0-9.]+),\\s*(-?[0-9.]+)\\s*\\)");
            MatchCollection mc = childRotationReg.Matches(content);
            for (int i = mc.Count - 1; i > -1; i--)
            {
                replacement = String.Format("ChildRotation({0}, {1}, {2}, {3})",
                    FlipSign(mc[i].Groups[1].Value),
                    FlipSign(mc[i].Groups[2].Value),
                    FlipSign(mc[i].Groups[3].Value),
                    FlipSign(mc[i].Groups[4].Value));
                working = content.Substring(0, mc[i].Index) + replacement + content.Substring(mc[i].Index + mc[i].Length);
                content = working;
            }
            return content;
        }

        private static string FlipSign(string s)
        {
            double d = Double.Parse(s);
            d *= -1;
            String retVal = d.ToString("0.000");
            return retVal;
        }

        #endregion

        private void textBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox_DragDrop(object sender, DragEventArgs e)
        {
            Control tb = sender as Control;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length == 1 && tb != null)
            {
                tb.Text = files[0];
            }
        }

        private void mBrowseLVL_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textFilename.Text = dlg.FileName;
            }
            dlg.Dispose();
        }

        private string GetArgs()
        {
            string file = " -file \"" + textFilename.Text+"\" ";
            string in_version = buttonBF2Input.Checked ? "" : " -version swbf ";
            string out_version = buttonBF2Output.Checked? "": " -outversion swbf ";
            string platform = "";
            if (buttonPS2.Checked) 
                platform = " -platform ps2 ";
            else if (buttonXbox.Checked)
                platform = " -platform xbox ";
            
            string image_format = "";
            if (buttonDDS.Checked)
                image_format = " -imgfmt dds ";
            else if (buttonPNG.Checked)
                image_format = " -imgfmt png ";

            string model_discard = "";
            if (check_collision.Checked && check_lod.Checked)
                model_discard = " -modeldiscard lod_collision ";
            else if (check_collision.Checked)
                model_discard = " -modeldiscard collision ";
            else if (check_lod.Checked)
                model_discard = " -modeldiscard lod ";
            string dictionary = "";
            if (File.Exists("dictionary.txt") && CheckUnmungeDictionaryVersion())
                dictionary = " -string_dict \"" + Path.GetFullPath("dictionary.txt") +"\" ";
            
            string args = string.Concat( file , in_version , out_version , image_format , model_discard , platform, dictionary);
            return args;
        }


        private bool CheckUnmungeDictionaryVersion()
        {
            bool retVal = false;
            string output = ExecuteUnmunge("", false);
            if (output.IndexOf("-string_dict") > -1)
                retVal = true;
            return retVal;
        }
        private string unmungeLocation = "swbf-unmunge.exe";

        private string ExecuteUnmunge(string args, bool logCommand)
        {
            string retVal = "";
            string programName = Path.GetFullPath(unmungeLocation);
            if (File.Exists(programName))
            {
                if (logCommand)
                {
                    Console.WriteLine("Running : " + programName + " " + args);
                }
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = programName,
                    Arguments = args,
                    WorkingDirectory = Path.GetFullPath("."),
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };
                
                try
                {
                    var process = Process.Start(processStartInfo);
                    string output = process.StandardOutput.ReadToEnd();
                    output += process.StandardError.ReadToEnd();

                    process.WaitForExit();
                    retVal = output;
                }
                catch (Exception ex)
                {
                    retVal = "Error occured while running program:\n"+ ex.Message;
                }
                
            }
            return retVal;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            // set unmunge location from the list box
            if(listUnmungeExe.SelectedItem != null)
                unmungeLocation = Path.GetFullPath( listUnmungeExe.SelectedItem.ToString());
            if (!File.Exists(unmungeLocation))
            {
                MessageBox.Show("Could not find program 'swbf-unmunge.exe'; please place it at the same folder as this program or in a sub directory.");
            }
            else
            {
                buttonGo.Enabled = false;
                string output = ExecuteUnmunge(GetArgs(), true);
                Console.WriteLine(output);
                Console.WriteLine(SummerizeOutput(output));
                buttonGo.Enabled = true;
            }
        }

        private string SummerizeOutput(string output)
        {
            string retVal = "";
            // summerize #errors, #missingHashes
            Regex unkHashRegex = new Regex("value: ([x0-9]+)");
            //System.Text.RegularExpressions.Regex unkHashRegex = new System.Text.RegularExpressions.Regex("value: ([x0-9]+)");
            MatchCollection mc = unkHashRegex.Matches(output);
            List<String> unkHashes = new List<string>();
            foreach (Match m in mc)
            {
                if (unkHashes.IndexOf(m.Groups[1].Value) == -1)
                    unkHashes.Add(m.Groups[1].Value);
            }
            retVal = String.Format("Unknown hashes: [{0}] ({1})", 
                unkHashes.Count, 
                String.Join(",", unkHashes.ToArray())
                );
            return retVal;
        }


        internal void SetStyle(Form prevForm)
        {
            this.BackColor = prevForm.BackColor;
            this.ForeColor = prevForm.ForeColor;

            this.listUnmungeExe.BackColor = prevForm.BackColor;
            this.listUnmungeExe.ForeColor = prevForm.ForeColor;

            TextBox tb = null;
            Button b = null;
            foreach (Control tmp in prevForm.Controls)
            {
                if (tb == null)
                    tb = tmp as TextBox;
                if (b == null)
                    b = tmp as Button;
                if (tb != null && b != null)
                    break;
            }
            if (tb != null)
            {
                this.textFilename.BackColor = tb.BackColor;
                this.textFilename.ForeColor = tb.ForeColor;
            }
            if (b != null)
            {
                buttonHelp.BackColor = 
                buttonBrowse.BackColor = buttonGo.BackColor = b.BackColor;

                groupBox1.ForeColor = groupBox2.ForeColor = groupBox3.ForeColor =
                groupBox4.ForeColor = groupBox5.ForeColor =
                buttonBrowse.ForeColor = buttonGo.ForeColor = buttonHelp.ForeColor = b.ForeColor;
            }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            string link = "https://github.com/SleepKiller/swbf-unmunge/releases";
            Process.Start(link);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textFilename.Text = dlg.FileName;
            }
            dlg.Dispose();
        }

        private void PopulateListUnmunge()
        {
            List<string> options = new List<string>(Directory.GetFiles(".", "swbf-unmun*.exe", SearchOption.AllDirectories));
            listUnmungeExe.Items.Clear();
            foreach (string option in options)
            {
                listUnmungeExe.Items.Add(option);
            }
            if (listUnmungeExe.Items.Count > 0)
            {
                listUnmungeExe.SelectedIndex = 0;
                Console.WriteLine("Current version: {0}", listUnmungeExe.Items[0].ToString());
            }
        }

        private void showKnownBugsForVersionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UnmungeBugForm().Show();
        }

        private void tryToRenameFilesWithHashedNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameHashNameFiles();
        }

        private void fixRotationInlyrFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FixRotationInLyr();
        }
    }
}
