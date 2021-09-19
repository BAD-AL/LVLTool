using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        }


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

        private string ExecuteUnmunge(string args, bool logCommand)
        {
            string retVal = "";
            string programName = Path.GetFullPath( "swbf-unmunge.exe");
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
            if (!File.Exists("swbf-unmunge.exe"))
            {
                MessageBox.Show("Could not find program 'swbf-unmunge.exe'; please place it in the same folder as this program.");
            }
            else
            {
                buttonGo.Enabled = false;
                string output = ExecuteUnmunge(GetArgs(), true);
                Console.WriteLine(output);
                buttonGo.Enabled = true;
            }
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
    }
}
