using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace LVLTool
{
    public partial class HashLookupForm : Form
    {
        public HashLookupForm()
        {
            InitializeComponent();
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
        private void mInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindHashes();
            }
        }

        private void mLookupButton_Click(object sender, EventArgs e)
        {
            FindHashes();
        }

        Regex mHexNumber = new Regex("0x([0-9a-fA-F]+)");
        Regex mDecNumber = new Regex("([0-9]+)");

        private void FindHashes()
        {
            uint current = 0;
            String key = "";
            MatchCollection mc = mHexNumber.Matches(txt_input.Text);
            StringBuilder sb = new StringBuilder();
            NumberStyles style = NumberStyles.AllowHexSpecifier;
            if (mc.Count < 1)
            {
                mc = mDecNumber.Matches(txt_input.Text);
                style = NumberStyles.Integer;
            }
            foreach (Match m in mc)
            {
                current = UInt32.Parse(m.Groups[1].ToString(), style);
                key = HashHelper.ReverseLookup(current);
                if (key == null)
                    key = "<No Match>";
                if( style == NumberStyles.AllowHexSpecifier)
                    sb.Append(String.Format("0x{0:x}:{1}\r\n", current, key));
                else
                    sb.Append(String.Format("{0}= 0x{0:x}:{1}\r\n", current, key));
            }
            
            txt_output.Text = sb.ToString();
        }


        internal void SetStyle(Form prevForm)
        {
            this.BackColor = prevForm.BackColor;
            this.ForeColor = prevForm.ForeColor;

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
                this.txt_hashMe.BackColor = this.txt_input.BackColor = 
                    this.txt_output.BackColor = tb.BackColor;
                this.txt_hashMe.ForeColor = this.txt_input.ForeColor = 
                    this.txt_output.ForeColor = tb.ForeColor;
            }
            if (b != null)
            {
                btn_addStrings.BackColor = btn_lookup.BackColor = b.BackColor;
                grp_hashMe.ForeColor =
                    btn_addStrings.ForeColor = btn_lookup.ForeColor = b.ForeColor;
            }
        }

        private void mAddStringsButton_Click(object sender, EventArgs e)
        {
            //public static string ShowMessage(string title, string message, Icon icon, bool editable, bool showCancelButton)
            string result = MessageForm.ShowMessage("Paste in strings to add", "", SystemIcons.Question, true, true);
            if (result != null)
            {
                result = result.Replace("\r\n", "\n");
                //public static string Replace(string input, string pattern, string replacement);
                result =  Regex.Replace(result, "[ ]+\n", "\n");
                
                List<string> stringsToAdd = new List<string>( result.Split(new char[] {'\n'}));
                int numberAdded = HashHelper.AddStringsToDictionary(stringsToAdd);
                txt_output.Text = string.Format("Added {0} previously unknown strings\n", numberAdded);
                if(numberAdded > 0)
                    HashHelper.WriteDictionary();
            }
        }

        private void mHashMeTextBox_TextChanged(object sender, EventArgs e)
        {
            uint result = HashHelper.HashString(txt_hashMe.Text);
            lbl_hashMe.Text = string.Format("0x{0:x}", result);
        }

        private void unmungeHashFinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                List<String> newStrings = CollectHashes(dlg.FileName);
                if (newStrings != null)
                {
                    MessageForm.ShowMessage("Found these", String.Join("\n", newStrings.ToArray()), SystemIcons.Information, false, false);
                }
            }
            dlg.Dispose();

        }

        private List<String> CollectHashes(string fileName)
        {
            // unmunge 1.2.2
            // swbf-unmunge.exe -platform pc -string_dict <my_dict> -gen_string_dict <new_dict> -folder <lvl_folder>
            string current_dict = "dictionary.txt";
            string gen_dict = "new_dict.txt";
            string unmunge_1_2_2 = "unmunge_versions\\swbf-unmunge-v1.2.2\\swbf-unmunge.exe";
            string unmunge_1_2_1 = "unmunge_versions\\swbf-unmunge-v1.2.1\\swbf-unmunge.exe";
            string unmungeProgram = "";
            string args = "";
            if (File.Exists(unmunge_1_2_2))
            {
                unmungeProgram = unmunge_1_2_2;
                FileInfo info = new FileInfo(fileName);
                args = String.Format(
                    "-platform pc -string_dict {0} -gen_string_dict {1} -folder {2} ",
                    current_dict, gen_dict, info.Directory );
            }
            else if (File.Exists(unmunge_1_2_1))
            {
                unmungeProgram = unmunge_1_2_1;
                args = String.Format(
                    "-platform pc -file {0} -string_dict {1} -gen_string_dict {2} ",
                    fileName, current_dict, gen_dict);
            }
            else
            {
                MessageBox.Show(String.Format("Could not find proper Unmunge program\n\n'{0}'\nor\n'{1}'\n",
                    unmunge_1_2_2, unmunge_1_2_1));
                return null;
            }
            string output =  Program.RunCommand(unmungeProgram, args, true);
            // now figure out what we don't have
            string[] found_strings = File.ReadAllText(gen_dict).Replace("\r\n","\n").Split("\n".ToCharArray());
            List<string> retVal = new List<string>(200);
            uint hashId = 0;
            foreach (string str in found_strings)
            {
                if (!str.StartsWith("0x"))
                {
                    hashId = HashHelper.HashString(str);
                    if (HashHelper.GetStringFromHash(hashId) == null)
                        retVal.Add(str);
                }
            }
            retVal.Sort();
            return retVal;
        }
    }
}
