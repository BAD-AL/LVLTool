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

namespace LVLTool
{
    public partial class HashLookupForm : Form
    {
        public HashLookupForm()
        {
            InitializeComponent();
        }

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
            }
        }

        private void mHashMeTextBox_TextChanged(object sender, EventArgs e)
        {
            uint result = HashHelper.HashString(txt_hashMe.Text);
            lbl_hashMe.Text = string.Format("0x{0:x}", result);
        }
    }
}
