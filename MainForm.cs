using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace LVLTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            State0();
            UpdateModToolsLabel();
            mMainTextBox.StatusControl = mStatusLabel;
        }

        private void textBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void State0()
        {
            mExtractAllButton.Enabled = 
                mAddItemButton.Enabled = 
                groupBox1.Enabled =
                mSaveFileButton.Enabled = 
                refreshLvlListBoxToolStripMenuItem.Enabled = 
                mReplaceButton.Enabled = 
                false;
        }

        private void State1()
        {
            mExtractAllButton.Enabled =
               mAddItemButton.Enabled =
               groupBox1.Enabled =
               mSaveFileButton.Enabled =
               refreshLvlListBoxToolStripMenuItem.Enabled = 
               mReplaceButton.Enabled = 
                true;
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

        UcfbHelper mExtractor = null;

        private void mLVLFileTextBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(mLVLFileTextBox.Text))
            {
                mMainTextBox.Clear();
                mExtractor = new UcfbHelper();
                mExtractor.FileName = mLVLFileTextBox.Text;
                PopulateListBox();
                State1();
            }
            else
            {
                State0();
            }
        }

        private void PopulateListBox()
        {
            mStatusLabel.Text =  "Reading File...";
            mAssetListBox.Items.Clear();
            mAssetListBox.BeginUpdate();
            mExtractor.InitializeRead();
            Chunk cur = null;
            while ( (cur = mExtractor.RipChunk(false)) != null)
            {
                mAssetListBox.Items.Add(cur);
            }
            mAssetListBox.EndUpdate();
            mStatusLabel.Text =  "Done Reading.";
        }

        private void extractAllButton_Click(object sender, EventArgs e)
        {
            mStatusLabel.Text =  "Extracting...";
            mExtractor.ExtractContents();
            mStatusLabel.Text =  "Done Extracting.";
        }

        private void mAssetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSelectedItemDetails();
        }

        private void ShowSelectedItemDetails()
        {
            mMainTextBox.Clear();
            int index = mAssetListBox.SelectedIndex;
            localizationMenu.Enabled = false;
            groupBox1.Enabled = false;
            if (index > -1)
            {
                Chunk c = mAssetListBox.Items[index] as Chunk;
                if (c != null)
                {
                    mMainTextBox.Text = GetItemText(c);
                    if (c.Type == "scr_")
                    {
                        groupBox1.Enabled = true;
                        if(pcLuaCodeButton.Checked || decompileButton.Checked)
                        ColorizeText();
                    }
                    else if (c.Type.ToLower().StartsWith("loc"))
                    {
                        localizationMenu.Enabled = true;
                        //ShowAllStrings(c);
                    }
                }
            }
        }

        private void mReplaceButton_Click(object sender, EventArgs e)
        {
            int index = mAssetListBox.SelectedIndex;
            if (index > -1)
            {
                Chunk c = mAssetListBox.Items[index] as Chunk;
                string fileName = null;

                try
                {
                    fileName = Munger.GetMungedFile("Choose file to replace " + c.Name + "." + c.Type);
                    if (fileName != null)
                    {
                        mStatusLabel.Text = "Replacing item...";
                        mExtractor.ReplaceUcfbChunk(c, fileName);
                        mStatusLabel.Text = "Done Replacing.";
                        PopulateListBox();
                        mMainTextBox.Clear();
                        if (index < mAssetListBox.Items.Count)
                            mAssetListBox.SelectedIndex = index;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error! Operation unsuccessful.");
                }
            }
        }

        private void mSaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = mLVLFileTextBox.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    mExtractor.SaveData(dlg.FileName);
                    mStatusLabel.Text = "File Saved.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Saving File! " + ex.Message);
                }
            }
            dlg.Dispose();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("LVLTool.exe Version " + Program.Version +
                "\nReplace or add to .lvl files\n" +
                "https://github.com/BAD-AL/LVLTool"
                );
        }

        private void refreshLvlListBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopulateListBox();
        }

        private void mAddItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName =  Munger.GetMungedFile("Select a file to add to the end of this .lvl file");
                if (fileName != null)
                {
                    mStatusLabel.Text = "Adding item...";
                    mExtractor.AddItemToEnd(fileName);
                    mStatusLabel.Text = "Done Adding.";
                    PopulateListBox();
                    mMainTextBox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error! Operation unsuccessful.");
            }

        }

        private void enterBF2ToolsDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String dir = StringInputDlg.GetString("Enter Mod Tools Directory", "", Program.ModToolsDir);
            if (dir != null && Directory.Exists(dir))
            {
                if (!dir.EndsWith("\\"))
                    dir += "\\";
                Program.ModToolsDir = dir;
                UpdateModToolsLabel();
                Program.SaveSettings();
                LuaCodeHelper.ResetFileCache();
            }
        }

        private void UpdateModToolsLabel()
        {
            mModToolsLabel.Text = "ModToolsDir: " + Program.ModToolsDir;
        }

        private string GetItemText(Chunk c)
        {
            string retVal = c.GetSummary();
            if (c.Type == "scr_")
            {
                if (pcLuaCodeButton.Checked)
                {
                    retVal = LuaCodeHelper.GetCodeSummary(c);
                }
                else if (listingButton.Checked)
                {
                    retVal = LuaCodeHelper.GetLuacListing(c);
                }
                else if (decompileButton.Checked)
                {
                    retVal = LuaCodeHelper.Decompile(c);
                }
            }
            return retVal;
        }

        Regex mComment = new Regex("(--.*)\n");
        /// <summary>
        /// Just some simple stuff, don't go crazy
        /// </summary>
        private void ColorizeText()
        {
            //CheckKeyword("-- Decompiled with SWBF2CodeHelper", Color.Green, 0);
            CheckKeyword("function", Color.Blue, 0);
            CheckKeyword("end", Color.Blue, 0);
            CheckKeyword("if", Color.Blue, 0);
            CheckKeyword("then", Color.Blue, 0);
            CheckKeyword("else", Color.Blue, 0);
            CheckKeyword("return", Color.Blue, 0);
            CheckKeyword("for", Color.Blue, 0);
            CheckKeyword("do", Color.Blue, 0);
            CheckKeyword("local", Color.Blue, 0);
            CheckKeyword("ERROR_PROCESSING_FUNCTION = true", Color.Red, 0);

            MatchCollection mc = mComment.Matches(mMainTextBox.Text);
            int selectStart = mMainTextBox.SelectionStart;
            foreach (Match m in mc)
            {
                mMainTextBox.Select(m.Groups[1].Index, m.Groups[1].Length);
                mMainTextBox.SelectionColor = Color.Green;
                mMainTextBox.Select(selectStart, 0);
                mMainTextBox.SelectionColor = Color.Black;
            }
        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (mMainTextBox.Text.Contains(word))
            {
                int index = -1;
                int selectStart = mMainTextBox.SelectionStart;

                while ((index = mMainTextBox.Text.IndexOf(word, (index + 1))) != -1)
                {
                    if (index == 0 || Char.IsWhiteSpace(mMainTextBox.Text[index - 1]))
                    {
                        mMainTextBox.Select((index + startIndex), word.Length);
                        mMainTextBox.SelectionColor = color;
                        mMainTextBox.Select(selectStart, 0);
                        mMainTextBox.SelectionColor = Color.Black;
                    }
                }
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            ShowSelectedItemDetails();
        }

        private void mExtractAllButton_Click(object sender, EventArgs e)
        {
            if (mExtractor != null)
            {
                mStatusLabel.Text = "Extracting...";
                mExtractor.InitializeRead();
                string dir = mExtractor.ExtractContents();
                mStatusLabel.Text = "Done Extracting.";
                MessageBox.Show("Extracted to: " + dir );

            }
        }

        private void mBrowseLVL_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                mLVLFileTextBox.Text = dlg.FileName;
            }
            dlg.Dispose();
        }

        private void showAllStringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mAssetListBox.SelectedIndex > -1)
            {
                Chunk c = mAssetListBox.Items[mAssetListBox.SelectedIndex] as Chunk;
                ShowAllStrings(c);
            }
        }

        private void ShowAllStrings(Chunk c)
        {
            if (c.Type.ToLower().StartsWith("loc"))
            {
                if (c.LocHelper == null)
                    c.LocHelper = new LocHelper(c.GetAssetData());
                mMainTextBox.Text = c.LocHelper.GetAllStrings();
            }
        }

        private void getStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string stringId =  StringInputDlg.GetString("Enter string id",
                "It can be a hex number (starting with '0x') or the '.' seperated words like 'cheats.ammo_off'");
            if (!String.IsNullOrEmpty( stringId ) )
            {
                if (mAssetListBox.SelectedIndex > -1)
                {
                    Chunk c = mAssetListBox.Items[mAssetListBox.SelectedIndex] as Chunk;
                    if (c.Type.ToLower().StartsWith("loc"))
                    {
                        LocHelper lc = new LocHelper(c.GetAssetData());
                        string result = lc.GetString(stringId);
                        if (!String.IsNullOrEmpty(result))
                            mMainTextBox.Text = result;
                        else 
                            mMainTextBox.Text = String.Format("StringId '{0}', not found", stringId);
                    }
                }
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
@"Displayed extension   munged extension 
    '.mcfg'                 '.config'
    '.snd_'                 '.config'
    '.zaf_'                 '.zafbin'
    '.zaa_'                 '.zaabin'
    '.lvl_'                 '.lvl'
    '.Locl'                 '.loc'
    '.tex_'                 '.texture'
    '.scr_'                 '.script'
    '.SHDR'                 '.shader'
    '.entc'                 '.class'
    '.skel'                 '.model'
    '.ANIM'                 '.anims'
    '.bnd_'                 '.boundary'
    '.plan'                 '.congraph'
    '.fx__'                 '.envfx'
    '.lght'                 '.light'
    '.wrld'                 '.world'
");
        }

        private void setStringMenuItem_Click(object sender, EventArgs e)
        {
            if (mAssetListBox.SelectedIndex > -1)
            {
                Chunk c = mAssetListBox.Items[mAssetListBox.SelectedIndex] as Chunk;
                if (c.Type.ToLower().StartsWith("loc"))
                {
                    if (c.LocHelper == null)
                        c.LocHelper = new LocHelper(c.GetAssetData());
                    string strId = StringInputDlg.GetString("Which string to set?", "Enter text:", "level.dea1.objectives.campaign.1");
                    if (strId != null)
                    {
                        string content = c.LocHelper.GetString(strId);
                        string newContant = StringInputDlg.GetString("What Value?", "Enter text:", content);
                        if (newContant.Length > 0)
                        {
                            c.LocHelper.SetString(strId, newContant);
                            mExtractor.ReplaceUcfbChunk(c, c.LocHelper.GetUcfbData());
                            ////c.Data = c.LocHelper.GetData(); // debugging only , misuse of 'Data' 
                            //c.LocHelper.DumpToFile("loc_tmp.bin");
                        }
                    }
                }
            }
        }
        // kept for possible debugging later.
        private void dumpFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mAssetListBox.SelectedIndex > -1)
            {
                Chunk c = mAssetListBox.Items[mAssetListBox.SelectedIndex] as Chunk;
                if (c.Type.ToLower().StartsWith("loc"))
                {
                    if (c.LocHelper == null)
                        c.LocHelper = new LocHelper(c.GetAssetData());
                    c.LocHelper.SaveToUcfbFile("loc_tmp.bin");
                }
            }
        }

        private void addStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mAssetListBox.SelectedIndex > -1)
            {
                Chunk c = mAssetListBox.Items[mAssetListBox.SelectedIndex] as Chunk;
                if (c.Type.ToLower().StartsWith("loc"))
                {
                    if (c.LocHelper == null)
                        c.LocHelper = new LocHelper(c.GetAssetData());
                    string strId = StringInputDlg.GetString("New String Id?", "Enter text:", "");
                    if (strId != null)
                    {
                        string content = c.LocHelper.GetString(strId);
                        string newContant = StringInputDlg.GetString("What Value?", "Enter text:", content);
                        if (newContant.Length > 0)
                        {
                            c.LocHelper.AddString(strId, newContant);
                            mExtractor.ReplaceUcfbChunk(c, c.LocHelper.GetUcfbData());
                            ////c.Data = c.LocHelper.GetData(); // debugging only , misuse of 'Data' 
                            //c.LocHelper.DumpToFile("loc_tmp.bin");
                        }
                    }
                }
            }
        }

        private void applyStringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mAssetListBox.SelectedIndex > -1)
            {
                Chunk c = mAssetListBox.Items[mAssetListBox.SelectedIndex] as Chunk;
                if (c.Type.ToLower().StartsWith("loc"))
                {
                    if (c.LocHelper == null)
                        c.LocHelper = new LocHelper(c.GetAssetData());
                    if (MessageBox.Show("Apply text content to current loc file?","Apply?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        c.LocHelper.ApplyText(mMainTextBox.Text);
                        mExtractor.ReplaceUcfbChunk(c, c.LocHelper.GetUcfbData());
                    }
                }
            }
        }

    }
}
