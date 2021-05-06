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
            this.createNewLVLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coreMergeFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshLvlListBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortListBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localizationMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllStringsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.applyStringsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setStringMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createMungedLocFileFromDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableStringsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStringsifNotAlreadyPresentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mModToolsLabel = new System.Windows.Forms.Label();
            this.mStatusLabel = new System.Windows.Forms.Label();
            this.mModToolsSelection = new System.Windows.Forms.ComboBox();
            this.findKnownHashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mBrowseLVL.BackColor = System.Drawing.Color.Gray;
            this.mBrowseLVL.Location = new System.Drawing.Point(642, 40);
            this.mBrowseLVL.Name = "mBrowseLVL";
            this.mBrowseLVL.Size = new System.Drawing.Size(25, 23);
            this.mBrowseLVL.TabIndex = 15;
            this.mBrowseLVL.Text = "...";
            this.mBrowseLVL.UseVisualStyleBackColor = false;
            this.mBrowseLVL.Click += new System.EventHandler(this.mBrowseLVL_Click);
            // 
            // mLVLFileTextBox
            // 
            this.mLVLFileTextBox.AllowDrop = true;
            this.mLVLFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mLVLFileTextBox.BackColor = System.Drawing.Color.Black;
            this.mLVLFileTextBox.ForeColor = System.Drawing.Color.White;
            this.mLVLFileTextBox.Location = new System.Drawing.Point(3, 43);
            this.mLVLFileTextBox.Name = "mLVLFileTextBox";
            this.mLVLFileTextBox.Size = new System.Drawing.Size(633, 20);
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
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
            this.splitContainer1.Size = new System.Drawing.Size(659, 371);
            this.splitContainer1.SplitterDistance = 219;
            this.splitContainer1.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.Gray;
            this.groupBox1.Controls.Add(this.decompileButton);
            this.groupBox1.Controls.Add(this.listingButton);
            this.groupBox1.Controls.Add(this.pcLuaCodeButton);
            this.groupBox1.Controls.Add(this.mSummaryRadioButton);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 62);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lua Code Display";
            // 
            // decompileButton
            // 
            this.decompileButton.AutoSize = true;
            this.decompileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decompileButton.Location = new System.Drawing.Point(84, 42);
            this.decompileButton.Name = "decompileButton";
            this.decompileButton.Size = new System.Drawing.Size(115, 17);
            this.decompileButton.TabIndex = 3;
            this.decompileButton.Text = "Lua (decompile)";
            this.decompileButton.UseVisualStyleBackColor = true;
            this.decompileButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // listingButton
            // 
            this.listingButton.AutoSize = true;
            this.listingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listingButton.Location = new System.Drawing.Point(6, 42);
            this.listingButton.Name = "listingButton";
            this.listingButton.Size = new System.Drawing.Size(62, 17);
            this.listingButton.TabIndex = 2;
            this.listingButton.Text = "Listing";
            this.listingButton.UseVisualStyleBackColor = true;
            this.listingButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // pcLuaCodeButton
            // 
            this.pcLuaCodeButton.AutoSize = true;
            this.pcLuaCodeButton.Checked = true;
            this.pcLuaCodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pcLuaCodeButton.Location = new System.Drawing.Point(84, 19);
            this.pcLuaCodeButton.Name = "pcLuaCodeButton";
            this.pcLuaCodeButton.Size = new System.Drawing.Size(99, 17);
            this.pcLuaCodeButton.TabIndex = 1;
            this.pcLuaCodeButton.TabStop = true;
            this.pcLuaCodeButton.Text = "PC Lua Code";
            this.pcLuaCodeButton.UseVisualStyleBackColor = true;
            this.pcLuaCodeButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // mSummaryRadioButton
            // 
            this.mSummaryRadioButton.AutoSize = true;
            this.mSummaryRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mSummaryRadioButton.Location = new System.Drawing.Point(6, 19);
            this.mSummaryRadioButton.Name = "mSummaryRadioButton";
            this.mSummaryRadioButton.Size = new System.Drawing.Size(75, 17);
            this.mSummaryRadioButton.TabIndex = 0;
            this.mSummaryRadioButton.Text = "Summary";
            this.mSummaryRadioButton.UseVisualStyleBackColor = true;
            this.mSummaryRadioButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // mAddItemButton
            // 
            this.mAddItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mAddItemButton.BackColor = System.Drawing.Color.Gray;
            this.mAddItemButton.Location = new System.Drawing.Point(3, 327);
            this.mAddItemButton.Name = "mAddItemButton";
            this.mAddItemButton.Size = new System.Drawing.Size(84, 23);
            this.mAddItemButton.TabIndex = 23;
            this.mAddItemButton.Text = "Add Item";
            this.mAddItemButton.UseVisualStyleBackColor = false;
            this.mAddItemButton.Click += new System.EventHandler(this.mAddItemButton_Click);
            // 
            // mSaveFileButton
            // 
            this.mSaveFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mSaveFileButton.BackColor = System.Drawing.Color.Gray;
            this.mSaveFileButton.Location = new System.Drawing.Point(109, 327);
            this.mSaveFileButton.Name = "mSaveFileButton";
            this.mSaveFileButton.Size = new System.Drawing.Size(102, 23);
            this.mSaveFileButton.TabIndex = 22;
            this.mSaveFileButton.Text = "Save lvl file";
            this.mSaveFileButton.UseVisualStyleBackColor = false;
            this.mSaveFileButton.Click += new System.EventHandler(this.mSaveFileButton_Click);
            // 
            // mReplaceButton
            // 
            this.mReplaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mReplaceButton.BackColor = System.Drawing.Color.Gray;
            this.mReplaceButton.Location = new System.Drawing.Point(109, 288);
            this.mReplaceButton.Name = "mReplaceButton";
            this.mReplaceButton.Size = new System.Drawing.Size(102, 23);
            this.mReplaceButton.TabIndex = 21;
            this.mReplaceButton.Text = "Replace Item";
            this.mReplaceButton.UseVisualStyleBackColor = false;
            this.mReplaceButton.Click += new System.EventHandler(this.mReplaceButton_Click);
            // 
            // mExtractAllButton
            // 
            this.mExtractAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mExtractAllButton.BackColor = System.Drawing.Color.Gray;
            this.mExtractAllButton.Location = new System.Drawing.Point(3, 288);
            this.mExtractAllButton.Name = "mExtractAllButton";
            this.mExtractAllButton.Size = new System.Drawing.Size(84, 23);
            this.mExtractAllButton.TabIndex = 20;
            this.mExtractAllButton.Text = "Extract all";
            this.mExtractAllButton.UseVisualStyleBackColor = false;
            this.mExtractAllButton.Click += new System.EventHandler(this.mExtractAllButton_Click);
            // 
            // mAssetListBox
            // 
            this.mAssetListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mAssetListBox.BackColor = System.Drawing.Color.Black;
            this.mAssetListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mAssetListBox.ForeColor = System.Drawing.Color.White;
            this.mAssetListBox.FormattingEnabled = true;
            this.mAssetListBox.ItemHeight = 16;
            this.mAssetListBox.Location = new System.Drawing.Point(3, 3);
            this.mAssetListBox.Name = "mAssetListBox";
            this.mAssetListBox.Size = new System.Drawing.Size(208, 196);
            this.mAssetListBox.TabIndex = 19;
            this.mAssetListBox.SelectedIndexChanged += new System.EventHandler(this.mAssetListBox_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.localizationMenu,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(674, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enterBF2ToolsDirToolStripMenuItem,
            this.createNewLVLToolStripMenuItem,
            this.coreMergeFormToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // enterBF2ToolsDirToolStripMenuItem
            // 
            this.enterBF2ToolsDirToolStripMenuItem.Name = "enterBF2ToolsDirToolStripMenuItem";
            this.enterBF2ToolsDirToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.enterBF2ToolsDirToolStripMenuItem.Text = "Enter Mod Tools Dir";
            this.enterBF2ToolsDirToolStripMenuItem.Click += new System.EventHandler(this.enterBF2ToolsDirToolStripMenuItem_Click);
            // 
            // createNewLVLToolStripMenuItem
            // 
            this.createNewLVLToolStripMenuItem.Name = "createNewLVLToolStripMenuItem";
            this.createNewLVLToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.createNewLVLToolStripMenuItem.Text = "Create new LVL";
            this.createNewLVLToolStripMenuItem.Click += new System.EventHandler(this.createNewLVLToolStripMenuItem_Click);
            // 
            // coreMergeFormToolStripMenuItem
            // 
            this.coreMergeFormToolStripMenuItem.Name = "coreMergeFormToolStripMenuItem";
            this.coreMergeFormToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.coreMergeFormToolStripMenuItem.Text = "Loc Merge Form";
            this.coreMergeFormToolStripMenuItem.Click += new System.EventHandler(this.locMergeFormToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshLvlListBoxToolStripMenuItem,
            this.sortListBoxToolStripMenuItem,
            this.findToolStripMenuItem,
            this.findKnownHashToolStripMenuItem});
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
            // sortListBoxToolStripMenuItem
            // 
            this.sortListBoxToolStripMenuItem.Name = "sortListBoxToolStripMenuItem";
            this.sortListBoxToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.sortListBoxToolStripMenuItem.Text = "Sort List box";
            this.sortListBoxToolStripMenuItem.Click += new System.EventHandler(this.sortListBoxToolStripMenuItem_Click);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.findToolStripMenuItem.Text = "Find Item";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // localizationMenu
            // 
            this.localizationMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAllStringsToolStripMenuItem1,
            this.applyStringsToolStripMenuItem,
            this.getToolStripMenuItem,
            this.setStringMenuItem,
            this.saveToFileToolStripMenuItem,
            this.dumpFileToolStripMenuItem,
            this.addStringToolStripMenuItem,
            this.createMungedLocFileFromDataToolStripMenuItem,
            this.disableStringsToolStripMenuItem,
            this.addStringsifNotAlreadyPresentToolStripMenuItem});
            this.localizationMenu.Enabled = false;
            this.localizationMenu.Name = "localizationMenu";
            this.localizationMenu.Size = new System.Drawing.Size(82, 20);
            this.localizationMenu.Text = "Localization";
            // 
            // showAllStringsToolStripMenuItem1
            // 
            this.showAllStringsToolStripMenuItem1.Name = "showAllStringsToolStripMenuItem1";
            this.showAllStringsToolStripMenuItem1.Size = new System.Drawing.Size(282, 22);
            this.showAllStringsToolStripMenuItem1.Text = "Show All Strings";
            this.showAllStringsToolStripMenuItem1.Click += new System.EventHandler(this.showAllStringsToolStripMenuItem_Click);
            // 
            // applyStringsToolStripMenuItem
            // 
            this.applyStringsToolStripMenuItem.Name = "applyStringsToolStripMenuItem";
            this.applyStringsToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.applyStringsToolStripMenuItem.Text = "Apply Strings";
            this.applyStringsToolStripMenuItem.Click += new System.EventHandler(this.applyStringsToolStripMenuItem_Click);
            // 
            // getToolStripMenuItem
            // 
            this.getToolStripMenuItem.Name = "getToolStripMenuItem";
            this.getToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.getToolStripMenuItem.Text = "Get String";
            this.getToolStripMenuItem.Click += new System.EventHandler(this.getStringToolStripMenuItem_Click);
            // 
            // setStringMenuItem
            // 
            this.setStringMenuItem.Name = "setStringMenuItem";
            this.setStringMenuItem.Size = new System.Drawing.Size(282, 22);
            this.setStringMenuItem.Text = "SetString";
            this.setStringMenuItem.Click += new System.EventHandler(this.setStringMenuItem_Click);
            // 
            // saveToFileToolStripMenuItem
            // 
            this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.saveToFileToolStripMenuItem.Text = "SaveToFile";
            this.saveToFileToolStripMenuItem.Visible = false;
            // 
            // dumpFileToolStripMenuItem
            // 
            this.dumpFileToolStripMenuItem.Name = "dumpFileToolStripMenuItem";
            this.dumpFileToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.dumpFileToolStripMenuItem.Text = "Dump File";
            this.dumpFileToolStripMenuItem.Visible = false;
            this.dumpFileToolStripMenuItem.Click += new System.EventHandler(this.dumpFileToolStripMenuItem_Click);
            // 
            // addStringToolStripMenuItem
            // 
            this.addStringToolStripMenuItem.Name = "addStringToolStripMenuItem";
            this.addStringToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.addStringToolStripMenuItem.Text = "Add String";
            this.addStringToolStripMenuItem.Visible = false;
            this.addStringToolStripMenuItem.Click += new System.EventHandler(this.addStringToolStripMenuItem_Click);
            // 
            // createMungedLocFileFromDataToolStripMenuItem
            // 
            this.createMungedLocFileFromDataToolStripMenuItem.Name = "createMungedLocFileFromDataToolStripMenuItem";
            this.createMungedLocFileFromDataToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.createMungedLocFileFromDataToolStripMenuItem.Text = "Create (Munged) Loc File from text box";
            this.createMungedLocFileFromDataToolStripMenuItem.Click += new System.EventHandler(this.createMungedLocFileFromDataToolStripMenuItem_Click);
            // 
            // disableStringsToolStripMenuItem
            // 
            this.disableStringsToolStripMenuItem.Name = "disableStringsToolStripMenuItem";
            this.disableStringsToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.disableStringsToolStripMenuItem.Text = "Disable Strings";
            this.disableStringsToolStripMenuItem.Click += new System.EventHandler(this.disableStringsToolStripMenuItem_Click);
            // 
            // addStringsifNotAlreadyPresentToolStripMenuItem
            // 
            this.addStringsifNotAlreadyPresentToolStripMenuItem.Name = "addStringsifNotAlreadyPresentToolStripMenuItem";
            this.addStringsifNotAlreadyPresentToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.addStringsifNotAlreadyPresentToolStripMenuItem.Text = "Add Strings (if not already present)";
            this.addStringsifNotAlreadyPresentToolStripMenuItem.Click += new System.EventHandler(this.addStringsifNotAlreadyPresentToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.DimGray;
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutProgramToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutProgramToolStripMenuItem
            // 
            this.aboutProgramToolStripMenuItem.Name = "aboutProgramToolStripMenuItem";
            this.aboutProgramToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.aboutProgramToolStripMenuItem.Text = "About Program";
            this.aboutProgramToolStripMenuItem.Click += new System.EventHandler(this.aboutProgramToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.aboutToolStripMenuItem1.Text = "About List Box types";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // mModToolsLabel
            // 
            this.mModToolsLabel.AutoSize = true;
            this.mModToolsLabel.BackColor = System.Drawing.Color.Transparent;
            this.mModToolsLabel.Location = new System.Drawing.Point(183, 26);
            this.mModToolsLabel.Name = "mModToolsLabel";
            this.mModToolsLabel.Size = new System.Drawing.Size(70, 13);
            this.mModToolsLabel.TabIndex = 21;
            this.mModToolsLabel.Text = "ModToolsDir:";
            this.mModToolsLabel.DoubleClick += new System.EventHandler(this.enterBF2ToolsDirToolStripMenuItem_Click);
            // 
            // mStatusLabel
            // 
            this.mStatusLabel.AutoSize = true;
            this.mStatusLabel.ForeColor = System.Drawing.Color.Aqua;
            this.mStatusLabel.Location = new System.Drawing.Point(6, 24);
            this.mStatusLabel.Name = "mStatusLabel";
            this.mStatusLabel.Size = new System.Drawing.Size(37, 13);
            this.mStatusLabel.TabIndex = 22;
            this.mStatusLabel.Text = "Status";
            // 
            // mModToolsSelection
            // 
            this.mModToolsSelection.BackColor = System.Drawing.Color.Gray;
            this.mModToolsSelection.FormattingEnabled = true;
            this.mModToolsSelection.Items.AddRange(new object[] {
            "C:\\BF2_ModTools\\",
            "C:\\BFBuilder\\"});
            this.mModToolsSelection.Location = new System.Drawing.Point(289, 21);
            this.mModToolsSelection.Name = "mModToolsSelection";
            this.mModToolsSelection.Size = new System.Drawing.Size(208, 21);
            this.mModToolsSelection.TabIndex = 23;
            // 
            // findKnownHashToolStripMenuItem
            // 
            this.findKnownHashToolStripMenuItem.Name = "findKnownHashToolStripMenuItem";
            this.findKnownHashToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.findKnownHashToolStripMenuItem.Text = "Find Known Hash";
            this.findKnownHashToolStripMenuItem.Click += new System.EventHandler(this.findKnownHashToolStripMenuItem_Click);
            // 
            // mMainTextBox
            // 
            this.mMainTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mMainTextBox.BackColor = System.Drawing.Color.Black;
            this.mMainTextBox.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.mMainTextBox.ForeColor = System.Drawing.Color.White;
            this.mMainTextBox.Location = new System.Drawing.Point(3, 3);
            this.mMainTextBox.Name = "mMainTextBox";
            this.mMainTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.mMainTextBox.SearchString = null;
            this.mMainTextBox.Size = new System.Drawing.Size(430, 358);
            this.mMainTextBox.StatusControl = null;
            this.mMainTextBox.TabIndex = 17;
            this.mMainTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(674, 454);
            this.Controls.Add(this.mModToolsSelection);
            this.Controls.Add(this.mStatusLabel);
            this.Controls.Add(this.mModToolsLabel);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mBrowseLVL);
            this.Controls.Add(this.mLVLFileTextBox);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.White;
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
        private System.Windows.Forms.ToolStripMenuItem localizationMenu;
        private System.Windows.Forms.ToolStripMenuItem showAllStringsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem applyStringsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setStringMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dumpFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortListBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createMungedLocFileFromDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableStringsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewLVLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStringsifNotAlreadyPresentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coreMergeFormToolStripMenuItem;
        private System.Windows.Forms.ComboBox mModToolsSelection;
        private System.Windows.Forms.ToolStripMenuItem findKnownHashToolStripMenuItem;
    }
}

