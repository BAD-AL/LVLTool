namespace LVLTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mBrowseLVL = new System.Windows.Forms.Button();
            this.mLVLFileTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.decompileButton = new System.Windows.Forms.RadioButton();
            this.listingButton = new System.Windows.Forms.RadioButton();
            this.pcLuaCodeButton = new System.Windows.Forms.RadioButton();
            this.mSummaryRadioButton = new System.Windows.Forms.RadioButton();
            this.mAddItemButton = new System.Windows.Forms.Button();
            this.mSaveFileButton = new System.Windows.Forms.Button();
            this.mReplaceButton = new System.Windows.Forms.Button();
            this.mExtractAllButton = new System.Windows.Forms.Button();
            this.mAssetListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enterBF2ToolsDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshLvlListBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mModToolsLabel = new System.Windows.Forms.Label();
            this.mStatusLabel = new System.Windows.Forms.Label();
            this.mMainTextBox = new LVLTool.SearchTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mBrowseLVL
            // 
            this.mBrowseLVL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mBrowseLVL.Location = new System.Drawing.Point(593, 40);
            this.mBrowseLVL.Name = "mBrowseLVL";
            this.mBrowseLVL.Size = new System.Drawing.Size(25, 23);
            this.mBrowseLVL.TabIndex = 15;
            this.mBrowseLVL.Text = "...";
            this.mBrowseLVL.UseVisualStyleBackColor = true;
            // 
            // mLVLFileTextBox
            // 
            this.mLVLFileTextBox.AllowDrop = true;
            this.mLVLFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mLVLFileTextBox.Location = new System.Drawing.Point(3, 43);
            this.mLVLFileTextBox.Name = "mLVLFileTextBox";
            this.mLVLFileTextBox.Size = new System.Drawing.Size(584, 20);
            this.mLVLFileTextBox.TabIndex = 14;
            this.mLVLFileTextBox.TextChanged += new System.EventHandler(this.mLVLFileTextBox_TextChanged);
            this.mLVLFileTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.mLVLFileTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 72);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.mAddItemButton);
            this.splitContainer1.Panel1.Controls.Add(this.mSaveFileButton);
            this.splitContainer1.Panel1.Controls.Add(this.mReplaceButton);
            this.splitContainer1.Panel1.Controls.Add(this.mExtractAllButton);
            this.splitContainer1.Panel1.Controls.Add(this.mAssetListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mMainTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(610, 365);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.decompileButton);
            this.groupBox1.Controls.Add(this.listingButton);
            this.groupBox1.Controls.Add(this.pcLuaCodeButton);
            this.groupBox1.Controls.Add(this.mSummaryRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 221);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 62);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lua Code Display";
            // 
            // decompileButton
            // 
            this.decompileButton.AutoSize = true;
            this.decompileButton.Location = new System.Drawing.Point(84, 42);
            this.decompileButton.Name = "decompileButton";
            this.decompileButton.Size = new System.Drawing.Size(100, 17);
            this.decompileButton.TabIndex = 3;
            this.decompileButton.Text = "Lua (decompile)";
            this.decompileButton.UseVisualStyleBackColor = true;
            this.decompileButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // listingButton
            // 
            this.listingButton.AutoSize = true;
            this.listingButton.Location = new System.Drawing.Point(6, 42);
            this.listingButton.Name = "listingButton";
            this.listingButton.Size = new System.Drawing.Size(55, 17);
            this.listingButton.TabIndex = 2;
            this.listingButton.Text = "Listing";
            this.listingButton.UseVisualStyleBackColor = true;
            this.listingButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // pcLuaCodeButton
            // 
            this.pcLuaCodeButton.AutoSize = true;
            this.pcLuaCodeButton.Checked = true;
            this.pcLuaCodeButton.Location = new System.Drawing.Point(84, 19);
            this.pcLuaCodeButton.Name = "pcLuaCodeButton";
            this.pcLuaCodeButton.Size = new System.Drawing.Size(88, 17);
            this.pcLuaCodeButton.TabIndex = 1;
            this.pcLuaCodeButton.TabStop = true;
            this.pcLuaCodeButton.Text = "PC Lua Code";
            this.pcLuaCodeButton.UseVisualStyleBackColor = true;
            this.pcLuaCodeButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // mSummaryRadioButton
            // 
            this.mSummaryRadioButton.AutoSize = true;
            this.mSummaryRadioButton.Location = new System.Drawing.Point(6, 19);
            this.mSummaryRadioButton.Name = "mSummaryRadioButton";
            this.mSummaryRadioButton.Size = new System.Drawing.Size(68, 17);
            this.mSummaryRadioButton.TabIndex = 0;
            this.mSummaryRadioButton.Text = "Summary";
            this.mSummaryRadioButton.UseVisualStyleBackColor = true;
            this.mSummaryRadioButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // mAddItemButton
            // 
            this.mAddItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mAddItemButton.Location = new System.Drawing.Point(3, 328);
            this.mAddItemButton.Name = "mAddItemButton";
            this.mAddItemButton.Size = new System.Drawing.Size(62, 23);
            this.mAddItemButton.TabIndex = 23;
            this.mAddItemButton.Text = "Add Item";
            this.mAddItemButton.UseVisualStyleBackColor = true;
            this.mAddItemButton.Click += new System.EventHandler(this.mAddItemButton_Click);
            // 
            // mSaveFileButton
            // 
            this.mSaveFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mSaveFileButton.Location = new System.Drawing.Point(98, 328);
            this.mSaveFileButton.Name = "mSaveFileButton";
            this.mSaveFileButton.Size = new System.Drawing.Size(97, 23);
            this.mSaveFileButton.TabIndex = 22;
            this.mSaveFileButton.Text = "Save lvl file";
            this.mSaveFileButton.UseVisualStyleBackColor = true;
            this.mSaveFileButton.Click += new System.EventHandler(this.mSaveFileButton_Click);
            // 
            // mReplaceButton
            // 
            this.mReplaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mReplaceButton.Location = new System.Drawing.Point(98, 289);
            this.mReplaceButton.Name = "mReplaceButton";
            this.mReplaceButton.Size = new System.Drawing.Size(97, 23);
            this.mReplaceButton.TabIndex = 21;
            this.mReplaceButton.Text = "Replace Item";
            this.mReplaceButton.UseVisualStyleBackColor = true;
            this.mReplaceButton.Click += new System.EventHandler(this.mReplaceButton_Click);
            // 
            // mExtractAllButton
            // 
            this.mExtractAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mExtractAllButton.Location = new System.Drawing.Point(3, 289);
            this.mExtractAllButton.Name = "mExtractAllButton";
            this.mExtractAllButton.Size = new System.Drawing.Size(62, 23);
            this.mExtractAllButton.TabIndex = 20;
            this.mExtractAllButton.Text = "Extract all";
            this.mExtractAllButton.UseVisualStyleBackColor = true;
            this.mExtractAllButton.Click += new System.EventHandler(this.mExtractAllButton_Click);
            // 
            // mAssetListBox
            // 
            this.mAssetListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mAssetListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mAssetListBox.FormattingEnabled = true;
            this.mAssetListBox.ItemHeight = 16;
            this.mAssetListBox.Location = new System.Drawing.Point(3, 3);
            this.mAssetListBox.Name = "mAssetListBox";
            this.mAssetListBox.Size = new System.Drawing.Size(192, 212);
            this.mAssetListBox.TabIndex = 19;
            this.mAssetListBox.SelectedIndexChanged += new System.EventHandler(this.mAssetListBox_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(625, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enterBF2ToolsDirToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // enterBF2ToolsDirToolStripMenuItem
            // 
            this.enterBF2ToolsDirToolStripMenuItem.Name = "enterBF2ToolsDirToolStripMenuItem";
            this.enterBF2ToolsDirToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.enterBF2ToolsDirToolStripMenuItem.Text = "Enter BF2 Tools Dir";
            this.enterBF2ToolsDirToolStripMenuItem.Click += new System.EventHandler(this.enterBF2ToolsDirToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshLvlListBoxToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // refreshLvlListBoxToolStripMenuItem
            // 
            this.refreshLvlListBoxToolStripMenuItem.Name = "refreshLvlListBoxToolStripMenuItem";
            this.refreshLvlListBoxToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.refreshLvlListBoxToolStripMenuItem.Text = "Refresh lvl list box";
            this.refreshLvlListBoxToolStripMenuItem.Click += new System.EventHandler(this.refreshLvlListBoxToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutProgramToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutProgramToolStripMenuItem
            // 
            this.aboutProgramToolStripMenuItem.Name = "aboutProgramToolStripMenuItem";
            this.aboutProgramToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.aboutProgramToolStripMenuItem.Text = "About Program";
            this.aboutProgramToolStripMenuItem.Click += new System.EventHandler(this.aboutProgramToolStripMenuItem_Click);
            // 
            // mModToolsLabel
            // 
            this.mModToolsLabel.AutoSize = true;
            this.mModToolsLabel.Location = new System.Drawing.Point(183, 27);
            this.mModToolsLabel.Name = "mModToolsLabel";
            this.mModToolsLabel.Size = new System.Drawing.Size(70, 13);
            this.mModToolsLabel.TabIndex = 21;
            this.mModToolsLabel.Text = "ModToolsDir:";
            // 
            // mStatusLabel
            // 
            this.mStatusLabel.AutoSize = true;
            this.mStatusLabel.Location = new System.Drawing.Point(6, 24);
            this.mStatusLabel.Name = "mStatusLabel";
            this.mStatusLabel.Size = new System.Drawing.Size(37, 13);
            this.mStatusLabel.TabIndex = 22;
            this.mStatusLabel.Text = "Status";
            // 
            // mMainTextBox
            // 
            this.mMainTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mMainTextBox.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.mMainTextBox.Location = new System.Drawing.Point(3, 3);
            this.mMainTextBox.Name = "mMainTextBox";
            this.mMainTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.mMainTextBox.SearchString = null;
            this.mMainTextBox.Size = new System.Drawing.Size(397, 359);
            this.mMainTextBox.StatusControl = null;
            this.mMainTextBox.TabIndex = 17;
            this.mMainTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 448);
            this.Controls.Add(this.mStatusLabel);
            this.Controls.Add(this.mModToolsLabel);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mBrowseLVL);
            this.Controls.Add(this.mLVLFileTextBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "LVLTool";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mBrowseLVL;
        private System.Windows.Forms.TextBox mLVLFileTextBox;
        private SearchTextBox mMainTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button mExtractAllButton;
        private System.Windows.Forms.ListBox mAssetListBox;
        private System.Windows.Forms.Button mReplaceButton;
        private System.Windows.Forms.Button mSaveFileButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enterBF2ToolsDirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshLvlListBoxToolStripMenuItem;
        private System.Windows.Forms.Button mAddItemButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton decompileButton;
        private System.Windows.Forms.RadioButton listingButton;
        private System.Windows.Forms.RadioButton pcLuaCodeButton;
        private System.Windows.Forms.RadioButton mSummaryRadioButton;
        private System.Windows.Forms.Label mModToolsLabel;
        private System.Windows.Forms.Label mStatusLabel;
    }
}

