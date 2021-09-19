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
using System.Globalization;

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
            mModToolsSelection.SelectedIndex = 0;
            mModToolsSelection.SelectedIndexChanged += new System.EventHandler(this.mModToolsSelection_SelectedIndexChanged);
        }

        /// <summary>
        /// Set the lua code display style
        /// </summary>
        /// <param name="arg">'summary', 'pc', 'list', 'decompile' are valid. </param>
        public void SetCodeDisplayStyle(string arg)
        {
            arg = arg.ToLower();
            switch (arg)
            {
                case "summary": mSummaryRadioButton.Checked = true; break;
                case "pc": pcLuaCodeButton.Checked = true; break;
                case "list": listingButton.Checked = true; break;
                case "decompile": decompileButton.Checked = true; break; 
            }
        }

        /// <summary>
        /// Returns the number of items found in the lvl file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        internal int SetLvlFile(string filename)
        {
            mLVLFileTextBox.Text = filename;
            return mAssetListBox.Items.Count;
        }

        internal bool ReplaceItem(string newItemPath, Platform p)
        {
            bool retVal = false;
            string type = "";
            if (newItemPath.EndsWith(".lua", StringComparison.OrdinalIgnoreCase) || 
                newItemPath.EndsWith(".script", StringComparison.OrdinalIgnoreCase))
                type = "scr_";
            else if (newItemPath.EndsWith(".tga", StringComparison.OrdinalIgnoreCase) || 
                newItemPath.EndsWith(".texture", StringComparison.OrdinalIgnoreCase))
                type = "tex_";
            else if (newItemPath.EndsWith(".config", StringComparison.OrdinalIgnoreCase) )
                type = "config";
            if (String.IsNullOrEmpty(type))
            {
                Console.WriteLine("Don't know how to replace file: "+ newItemPath);
                return retVal;
            }
            int lastSlash = newItemPath.LastIndexOf("\\");
            string stem = newItemPath.Substring(lastSlash + 1);
            int dotIndex = stem.LastIndexOf(".");
            string name = stem.Substring(0, dotIndex);
            Chunk current = null;
            Chunk found = null;
            
            // find item 
            for (int i = 0; i < mAssetListBox.Items.Count; i++)
            {
                current = mAssetListBox.Items[i] as Chunk;
                if (current.Name == name && current.Type == type) // select this index
                {
                    found = current;
                    mAssetListBox.SelectedIndex = i;
                    break;
                }
            }
            //string fileName, Platform platform
            string mungedFile = Munger.EnsureMungedFile(newItemPath, p);
            if (mungedFile != null)
            {
                mUcfbFileHelper.ReplaceUcfbChunk(found, mungedFile, true);
                retVal = true;
            }
            return retVal;
        }

        internal void SaveLvl(string outputFileName)
        {
            mUcfbFileHelper.SaveData(outputFileName);
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

        UcfbHelper mUcfbFileHelper = null;

        private void mLVLFileTextBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(mLVLFileTextBox.Text))
            {
                mMainTextBox.Clear();
                mUcfbFileHelper = new UcfbHelper();
                mUcfbFileHelper.FileName = mLVLFileTextBox.Text;
                PopulateListBox();
                State1();
                ShowLVLSummary();
            }
            else
            {
                State0();
            }
        }

        private void ShowLVLSummary()
        {
            string fileName = mLVLFileTextBox.Text;
            List<Chunk> allMyChunks = GetAllChunks(fileName);
            List<String> myTypes = new List<string>();
            int limit = allMyChunks.Count;
            Chunk cur = null;
            for (int i = 0; i < limit; i++)
            {
                cur = allMyChunks[i];
                if (myTypes.IndexOf(cur.Type) < 0)
                    myTypes.Add(cur.Type);
            }
            myTypes.Sort();

            StringBuilder builder = new StringBuilder(500);
            builder.Append("Summary\n");
            builder.Append(fileName);
            builder.Append(":\n");
            
            uint typeSize = 0;
            foreach (string type in myTypes)
            {
                typeSize = 0;
                for (int i = 0; i < limit; i++)
                {
                    cur = allMyChunks[i];
                    if (cur.Type == type)
                    {
                        typeSize += cur.Length;
                    }
                }
                builder.Append(String.Format("Type: {0} Total size: {1:N}kb \n", type,typeSize/1024.0));
            }
            mMainTextBox.Text = builder.ToString();
        }

        private void PopulateListBox()
        {
            mStatusLabel.Text =  "Reading File...";
            mAssetListBox.Items.Clear();
            mAssetListBox.BeginUpdate();
            mUcfbFileHelper.InitializeRead();
            Chunk cur = null;
            while ( (cur = mUcfbFileHelper.RipChunk(false)) != null)
            {
                mAssetListBox.Items.Add(cur);
            }
            mAssetListBox.EndUpdate();
            mStatusLabel.Text =  "Done Reading.";
        }

        // Goes through the lvl file extracts all the chunks; if a child is a lvl file
        // those are split up and returned.
        private List<Chunk> GetAllChunks(string filename)
        {
            List<Chunk> retVal = new List<Chunk>();
            UcfbHelper helper = new UcfbHelper();
            helper.FileName = filename;
            helper.InitializeRead();
            Chunk cur = null;
            while ((cur = helper.RipChunk(false)) != null)
            {
                if (cur.Type != "lvl_")
                    retVal.Add(cur);
                else
                {
                    string tmpFile = "tmp_child.lvl";
                    UcfbHelper.SaveFileUCFB(tmpFile,cur.Data);
                    List<Chunk> childChunks = GetAllChunks(tmpFile);
                    File.Delete(tmpFile);
                    retVal.AddRange(childChunks);
                }
            }
            return retVal;
        }

        private void extractAllButton_Click(object sender, EventArgs e)
        {
            mStatusLabel.Text =  "Extracting...";
            mUcfbFileHelper.ExtractContents();
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
                        mUcfbFileHelper.ReplaceUcfbChunk(c, fileName, true);
                        mStatusLabel.Text = "Done Replacing.";
                        //PopulateListBox();
                        //mMainTextBox.Clear();
                        //if (index < mAssetListBox.Items.Count)
                        //    mAssetListBox.SelectedIndex = index;
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
                    mUcfbFileHelper.SaveData(dlg.FileName);
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
                    mUcfbFileHelper.AddItemToEnd(fileName);
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

        private void EnterModToolsDir()
        {
            String dir = StringInputDlg.GetString("Enter Mod Tools Directory", "", Program.ModToolsDir);
            dir = SetModToolsDir(dir);
        }

        private String SetModToolsDir(String dir)
        {
            if (dir != null && Directory.Exists(dir))
            {
                if (!dir.EndsWith("\\"))
                    dir += "\\";
                Program.ModToolsDir = dir;
                UpdateModToolsLabel();
                Program.SaveSettings();
                LuaCodeHelper.ResetFileCache();
            }
            return dir;
        }

        private void enterBF2ToolsDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnterModToolsDir();
        }


        private void mModToolsSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = mModToolsSelection.SelectedItem.ToString();
            SetModToolsDir(text);
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
            Color kwColor = Color.Magenta;
            CheckKeyword("function", kwColor, 0);
            CheckKeyword("end", kwColor, 0);
            CheckKeyword("if", kwColor, 0);
            CheckKeyword("then", kwColor, 0);
            CheckKeyword("else", kwColor, 0);
            CheckKeyword("return", kwColor, 0);
            CheckKeyword("for", kwColor, 0);
            CheckKeyword("do", kwColor, 0);
            CheckKeyword("local", kwColor, 0);
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
            if (mUcfbFileHelper != null)
            {
                mStatusLabel.Text = "Extracting...";
                mUcfbFileHelper.InitializeRead();
                string dir = mUcfbFileHelper.ExtractContents();
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
                            mUcfbFileHelper.ReplaceUcfbChunk(c, c.LocHelper.GetUcfbData(), true);
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
                            mUcfbFileHelper.ReplaceUcfbChunk(c, c.LocHelper.GetUcfbData(), true);
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
                        mUcfbFileHelper.ReplaceUcfbChunk(c, c.LocHelper.GetUcfbData(), true);
                    }
                }
            }
        }

        private void sortListBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mAssetListBox.Sorted = !mAssetListBox.Sorted;
            sortListBoxToolStripMenuItem.Checked = mAssetListBox.Sorted;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string findMe = StringInputDlg.GetString("Enter item name to search for", "<item name>");
            if (findMe != null)
            {
                Chunk chk = null;
                for(int i =0; i < mAssetListBox.Items.Count; i++)
                {
                    chk = mAssetListBox.Items[i] as Chunk;
                    if (chk != null)
                    {
                        if (findMe == chk.ToString() || findMe == chk.Name)
                        {
                            mAssetListBox.SelectedIndex = i;
                            return;
                        }
                    }
                }
                for (int i = 0; i < mAssetListBox.Items.Count; i++)
                {
                    chk = mAssetListBox.Items[i] as Chunk;
                    if (chk != null)
                    {
                        if (findMe == chk.Name)
                        {
                            mAssetListBox.SelectedIndex = i;
                            return;
                        }
                    }
                }
            }
        }

        private void createMungedLocFileFromDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();
            List<byte> data = LocHelper.GetBinaryLocData(mMainTextBox.Text, errors);
            if (data.Count > 0)
            {
                LocHelper.MakeUcfb(data, (uint)data.Count);
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.RestoreDirectory = true;
                int lastSlash = mLVLFileTextBox.Text.LastIndexOf(Path.DirectorySeparatorChar);
                dlg.InitialDirectory = mLVLFileTextBox.Text.Substring(0, lastSlash);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(dlg.FileName, data.ToArray());
                }
                dlg.Dispose();
            }
        }

        private void disableStringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chunk locChunk = mAssetListBox.Items[mAssetListBox.SelectedIndex] as Chunk;
            if (locChunk.LocHelper == null)
                locChunk.LocHelper = new LocHelper(locChunk.GetAssetData());

            string userInput =
              MessageForm.ShowMessage("Paste String ids into the text box", "", SystemIcons.Question, true, true);
            if (userInput != null)
            {
                string number = StringInputDlg.GetString("Enter occurence", "Enter the occurence of the string you want to disable", "1");
                int target_occurence = 1;
                if( Int32.TryParse(number, out target_occurence))
                {
                    string[] lines = userInput.Replace("\r\n", "\n").Split("\n".ToCharArray());
                    foreach (string line in lines)
                    {
                        if( line.Length > 0)
                            locChunk.LocHelper.DisableString(line, target_occurence);
                    }
                    // now...
                    // copy the data from the chunk to the in-memory data
                    byte[] rawData = locChunk.LocHelper.GetRawData();
                    Array.Copy(rawData, 0, mUcfbFileHelper.Data, (int)locChunk.Start, rawData.Length);
                }
            }
        }

        private void createNewLVLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateLvlForm form = new CreateLvlForm();
            form.SetStyle(this);
            form.Show();
        }

        private void addStringsifNotAlreadyPresentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mAssetListBox.SelectedIndex > -1)
            {
                Chunk c = mAssetListBox.Items[mAssetListBox.SelectedIndex] as Chunk;
                if (c.Type.ToLower().StartsWith("loc"))
                {
                    if (c.LocHelper == null)
                        c.LocHelper = new LocHelper(c.GetAssetData());
                    if (MessageBox.Show("Add new strings to current loc file?", "Apply?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        c.LocHelper.AddNewStrings(mMainTextBox.Text);
                        mUcfbFileHelper.ReplaceUcfbChunk(c, c.LocHelper.GetUcfbData(), true);
                    }
                }
            }
        }

        private void locMergeFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocMergeForm lmf = new LocMergeForm();
            lmf.SetStyle(this);
            lmf.Show();
        }

        private void findKnownHashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HashLookupForm form = new HashLookupForm();
            form.SetStyle(this);
            form.StartPosition = FormStartPosition.CenterParent;
            
            form.Show(this);
        }

        private void renameItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameSelectedItem();
        }

        private void RenameSelectedItem()
        {
            if (mAssetListBox.SelectedIndex > -1)
            {
                Chunk c = mAssetListBox.SelectedItem as Chunk;
                String newName = StringInputDlg.GetString("Enter New name","", c.Name);
                if( newName != null)
                    RenameItem(c, newName);
            }
        }

        private void RenameItem(Chunk c, String newName)
        {
            if (newName != null)
            {
                if (newName.Length == c.Name.Length)
                {
                    int start = (int)BinUtils.GetLocationOfGivenBytes(c.Start, ASCIIEncoding.ASCII.GetBytes(c.Name), mUcfbFileHelper.Data, 80);
                    for (int i = 0; i < c.Name.Length; i++)
                    {
                        mUcfbFileHelper.Data[start + i] = (byte)newName[i];
                    }
                }
                else
                {
                    Console.WriteLine("It is unsupported to change the length of the item's name.");
                }
            }
            else
            {
                Console.WriteLine("Error! Cannot set item name to null");
            }
        }

        private void runUnmungeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnmungeForm form = new UnmungeForm();
            form.SetStyle(this);
            form.Show();
        }

    }
}
