namespace LVLTool
{
    partial class UnmungeForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnmungeForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonXbox = new System.Windows.Forms.RadioButton();
            this.buttonPS2 = new System.Windows.Forms.RadioButton();
            this.buttonPC = new System.Windows.Forms.RadioButton();
            this.textFilename = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonBF2Input = new System.Windows.Forms.RadioButton();
            this.buttonBf1Input = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonBF2Output = new System.Windows.Forms.RadioButton();
            this.buttonBf1Output = new System.Windows.Forms.RadioButton();
            this.buttonExtract = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonDDS = new System.Windows.Forms.RadioButton();
            this.buttonPNG = new System.Windows.Forms.RadioButton();
            this.buttonTGA = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.check_collision = new System.Windows.Forms.CheckBox();
            this.check_lod = new System.Windows.Forms.CheckBox();
            this.contextMenuUnmungeVersion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tryToRenameFilesWithHashedNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixRotationInlyrFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listUnmungeExe = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageExtract = new System.Windows.Forms.TabPage();
            this.tabPageExplodeAssemble = new System.Windows.Forms.TabPage();
            this.buttonExplodeAssembleHelp = new System.Windows.Forms.Button();
            this.buttonAssemble = new System.Windows.Forms.Button();
            this.buttonExplode = new System.Windows.Forms.Button();
            this.treeViewExploded = new System.Windows.Forms.TreeView();
            this.contextMenuTreeViewExploded = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyPathToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStringDict = new System.Windows.Forms.TextBox();
            this.openFolderInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.contextMenuUnmungeVersion.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageExtract.SuspendLayout();
            this.tabPageExplodeAssemble.SuspendLayout();
            this.contextMenuTreeViewExploded.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonXbox);
            this.groupBox1.Controls.Add(this.buttonPS2);
            this.groupBox1.Controls.Add(this.buttonPC);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Platform";
            // 
            // buttonXbox
            // 
            this.buttonXbox.AutoSize = true;
            this.buttonXbox.Location = new System.Drawing.Point(102, 15);
            this.buttonXbox.Name = "buttonXbox";
            this.buttonXbox.Size = new System.Drawing.Size(54, 17);
            this.buttonXbox.TabIndex = 2;
            this.buttonXbox.Text = "XBOX";
            this.buttonXbox.UseVisualStyleBackColor = true;
            // 
            // buttonPS2
            // 
            this.buttonPS2.AutoSize = true;
            this.buttonPS2.Location = new System.Drawing.Point(51, 15);
            this.buttonPS2.Name = "buttonPS2";
            this.buttonPS2.Size = new System.Drawing.Size(45, 17);
            this.buttonPS2.TabIndex = 1;
            this.buttonPS2.Text = "PS2";
            this.buttonPS2.UseVisualStyleBackColor = true;
            // 
            // buttonPC
            // 
            this.buttonPC.AutoSize = true;
            this.buttonPC.Checked = true;
            this.buttonPC.Location = new System.Drawing.Point(6, 15);
            this.buttonPC.Name = "buttonPC";
            this.buttonPC.Size = new System.Drawing.Size(39, 17);
            this.buttonPC.TabIndex = 0;
            this.buttonPC.TabStop = true;
            this.buttonPC.Text = "PC";
            this.buttonPC.UseVisualStyleBackColor = true;
            // 
            // textFilename
            // 
            this.textFilename.AllowDrop = true;
            this.textFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textFilename.Location = new System.Drawing.Point(12, 31);
            this.textFilename.Name = "textFilename";
            this.textFilename.Size = new System.Drawing.Size(424, 20);
            this.textFilename.TabIndex = 1;
            this.textFilename.TextChanged += new System.EventHandler(this.textFilename_TextChanged);
            this.textFilename.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.textFilename.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonBF2Input);
            this.groupBox2.Controls.Add(this.buttonBf1Input);
            this.groupBox2.Location = new System.Drawing.Point(6, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 49);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input Version";
            // 
            // buttonBF2Input
            // 
            this.buttonBF2Input.AutoSize = true;
            this.buttonBF2Input.Checked = true;
            this.buttonBF2Input.Location = new System.Drawing.Point(51, 19);
            this.buttonBF2Input.Name = "buttonBF2Input";
            this.buttonBF2Input.Size = new System.Drawing.Size(44, 17);
            this.buttonBF2Input.TabIndex = 1;
            this.buttonBF2Input.TabStop = true;
            this.buttonBF2Input.Text = "BF2";
            this.buttonBF2Input.UseVisualStyleBackColor = true;
            // 
            // buttonBf1Input
            // 
            this.buttonBf1Input.AutoSize = true;
            this.buttonBf1Input.Location = new System.Drawing.Point(6, 19);
            this.buttonBf1Input.Name = "buttonBf1Input";
            this.buttonBf1Input.Size = new System.Drawing.Size(44, 17);
            this.buttonBf1Input.TabIndex = 0;
            this.buttonBf1Input.Text = "BF1";
            this.buttonBf1Input.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "File (drag into text box)";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(442, 28);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(26, 23);
            this.buttonBrowse.TabIndex = 5;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonBF2Output);
            this.groupBox3.Controls.Add(this.buttonBf1Output);
            this.groupBox3.Location = new System.Drawing.Point(6, 116);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(175, 49);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output Version";
            // 
            // buttonBF2Output
            // 
            this.buttonBF2Output.AutoSize = true;
            this.buttonBF2Output.Checked = true;
            this.buttonBF2Output.Location = new System.Drawing.Point(51, 19);
            this.buttonBF2Output.Name = "buttonBF2Output";
            this.buttonBF2Output.Size = new System.Drawing.Size(44, 17);
            this.buttonBF2Output.TabIndex = 1;
            this.buttonBF2Output.TabStop = true;
            this.buttonBF2Output.Text = "BF2";
            this.buttonBF2Output.UseVisualStyleBackColor = true;
            // 
            // buttonBf1Output
            // 
            this.buttonBf1Output.AutoSize = true;
            this.buttonBf1Output.Location = new System.Drawing.Point(6, 19);
            this.buttonBf1Output.Name = "buttonBf1Output";
            this.buttonBf1Output.Size = new System.Drawing.Size(44, 17);
            this.buttonBf1Output.TabIndex = 0;
            this.buttonBf1Output.Text = "BF1";
            this.buttonBf1Output.UseVisualStyleBackColor = true;
            // 
            // buttonExtract
            // 
            this.buttonExtract.BackColor = System.Drawing.Color.Transparent;
            this.buttonExtract.Enabled = false;
            this.buttonExtract.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonExtract.Location = new System.Drawing.Point(190, 15);
            this.buttonExtract.Name = "buttonExtract";
            this.buttonExtract.Size = new System.Drawing.Size(175, 23);
            this.buttonExtract.TabIndex = 6;
            this.buttonExtract.Text = "Extract";
            this.buttonExtract.UseVisualStyleBackColor = false;
            this.buttonExtract.Click += new System.EventHandler(this.buttonExtract_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonHelp.Location = new System.Drawing.Point(3, 238);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(120, 23);
            this.buttonHelp.TabIndex = 7;
            this.buttonHelp.Text = "Get Unmunge";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonDDS);
            this.groupBox4.Controls.Add(this.buttonPNG);
            this.groupBox4.Controls.Add(this.buttonTGA);
            this.groupBox4.Location = new System.Drawing.Point(190, 61);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(175, 49);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Image Format";
            // 
            // buttonDDS
            // 
            this.buttonDDS.AutoSize = true;
            this.buttonDDS.Location = new System.Drawing.Point(102, 15);
            this.buttonDDS.Name = "buttonDDS";
            this.buttonDDS.Size = new System.Drawing.Size(48, 17);
            this.buttonDDS.TabIndex = 2;
            this.buttonDDS.Text = "DDS";
            this.buttonDDS.UseVisualStyleBackColor = true;
            // 
            // buttonPNG
            // 
            this.buttonPNG.AutoSize = true;
            this.buttonPNG.Location = new System.Drawing.Point(51, 15);
            this.buttonPNG.Name = "buttonPNG";
            this.buttonPNG.Size = new System.Drawing.Size(48, 17);
            this.buttonPNG.TabIndex = 1;
            this.buttonPNG.Text = "PNG";
            this.buttonPNG.UseVisualStyleBackColor = true;
            // 
            // buttonTGA
            // 
            this.buttonTGA.AutoSize = true;
            this.buttonTGA.Checked = true;
            this.buttonTGA.Location = new System.Drawing.Point(6, 15);
            this.buttonTGA.Name = "buttonTGA";
            this.buttonTGA.Size = new System.Drawing.Size(47, 17);
            this.buttonTGA.TabIndex = 0;
            this.buttonTGA.TabStop = true;
            this.buttonTGA.Text = "TGA";
            this.buttonTGA.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.check_collision);
            this.groupBox5.Controls.Add(this.check_lod);
            this.groupBox5.Location = new System.Drawing.Point(190, 116);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(175, 49);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Model Discard";
            // 
            // check_collision
            // 
            this.check_collision.AutoSize = true;
            this.check_collision.Location = new System.Drawing.Point(65, 20);
            this.check_collision.Name = "check_collision";
            this.check_collision.Size = new System.Drawing.Size(63, 17);
            this.check_collision.TabIndex = 1;
            this.check_collision.Text = "collision";
            this.check_collision.UseVisualStyleBackColor = true;
            // 
            // check_lod
            // 
            this.check_lod.AutoSize = true;
            this.check_lod.Location = new System.Drawing.Point(6, 20);
            this.check_lod.Name = "check_lod";
            this.check_lod.Size = new System.Drawing.Size(40, 17);
            this.check_lod.TabIndex = 0;
            this.check_lod.Text = "lod";
            this.check_lod.UseVisualStyleBackColor = true;
            // 
            // contextMenuUnmungeVersion
            // 
            this.contextMenuUnmungeVersion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tryToRenameFilesWithHashedNamesToolStripMenuItem,
            this.fixRotationInlyrFilesToolStripMenuItem});
            this.contextMenuUnmungeVersion.Name = "contextMenuUnmungeVersion";
            this.contextMenuUnmungeVersion.Size = new System.Drawing.Size(282, 48);
            // 
            // tryToRenameFilesWithHashedNamesToolStripMenuItem
            // 
            this.tryToRenameFilesWithHashedNamesToolStripMenuItem.Name = "tryToRenameFilesWithHashedNamesToolStripMenuItem";
            this.tryToRenameFilesWithHashedNamesToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.tryToRenameFilesWithHashedNamesToolStripMenuItem.Text = "Try To re-name files with hashed names";
            this.tryToRenameFilesWithHashedNamesToolStripMenuItem.Click += new System.EventHandler(this.tryToRenameFilesWithHashedNamesToolStripMenuItem_Click);
            // 
            // fixRotationInlyrFilesToolStripMenuItem
            // 
            this.fixRotationInlyrFilesToolStripMenuItem.Name = "fixRotationInlyrFilesToolStripMenuItem";
            this.fixRotationInlyrFilesToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.fixRotationInlyrFilesToolStripMenuItem.Text = "Fix Rotation in .lyr files";
            this.fixRotationInlyrFilesToolStripMenuItem.Click += new System.EventHandler(this.fixRotationInlyrFilesToolStripMenuItem_Click);
            // 
            // listUnmungeExe
            // 
            this.listUnmungeExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listUnmungeExe.ContextMenuStrip = this.contextMenuUnmungeVersion;
            this.listUnmungeExe.FormattingEnabled = true;
            this.listUnmungeExe.Location = new System.Drawing.Point(12, 395);
            this.listUnmungeExe.Name = "listUnmungeExe";
            this.listUnmungeExe.Size = new System.Drawing.Size(456, 69);
            this.listUnmungeExe.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageExtract);
            this.tabControl1.Controls.Add(this.tabPageExplodeAssemble);
            this.tabControl1.Location = new System.Drawing.Point(12, 57);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(456, 293);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPageExtract
            // 
            this.tabPageExtract.Controls.Add(this.groupBox1);
            this.tabPageExtract.Controls.Add(this.groupBox2);
            this.tabPageExtract.Controls.Add(this.groupBox3);
            this.tabPageExtract.Controls.Add(this.groupBox5);
            this.tabPageExtract.Controls.Add(this.groupBox4);
            this.tabPageExtract.Controls.Add(this.buttonHelp);
            this.tabPageExtract.Controls.Add(this.buttonExtract);
            this.tabPageExtract.Location = new System.Drawing.Point(4, 22);
            this.tabPageExtract.Name = "tabPageExtract";
            this.tabPageExtract.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExtract.Size = new System.Drawing.Size(448, 267);
            this.tabPageExtract.TabIndex = 0;
            this.tabPageExtract.Text = "Extract";
            this.tabPageExtract.UseVisualStyleBackColor = true;
            // 
            // tabPageExplodeAssemble
            // 
            this.tabPageExplodeAssemble.Controls.Add(this.buttonExplodeAssembleHelp);
            this.tabPageExplodeAssemble.Controls.Add(this.buttonAssemble);
            this.tabPageExplodeAssemble.Controls.Add(this.buttonExplode);
            this.tabPageExplodeAssemble.Controls.Add(this.treeViewExploded);
            this.tabPageExplodeAssemble.Location = new System.Drawing.Point(4, 22);
            this.tabPageExplodeAssemble.Name = "tabPageExplodeAssemble";
            this.tabPageExplodeAssemble.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExplodeAssemble.Size = new System.Drawing.Size(448, 267);
            this.tabPageExplodeAssemble.TabIndex = 1;
            this.tabPageExplodeAssemble.Text = "Explode/Assemble";
            this.tabPageExplodeAssemble.UseVisualStyleBackColor = true;
            // 
            // buttonExplodeAssembleHelp
            // 
            this.buttonExplodeAssembleHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExplodeAssembleHelp.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonExplodeAssembleHelp.ForeColor = System.Drawing.Color.White;
            this.buttonExplodeAssembleHelp.Location = new System.Drawing.Point(402, 9);
            this.buttonExplodeAssembleHelp.Name = "buttonExplodeAssembleHelp";
            this.buttonExplodeAssembleHelp.Size = new System.Drawing.Size(40, 23);
            this.buttonExplodeAssembleHelp.TabIndex = 15;
            this.buttonExplodeAssembleHelp.Text = "?";
            this.buttonExplodeAssembleHelp.UseVisualStyleBackColor = false;
            this.buttonExplodeAssembleHelp.Click += new System.EventHandler(this.buttonExplodeAssembleHelp_Click);
            // 
            // buttonAssemble
            // 
            this.buttonAssemble.Location = new System.Drawing.Point(198, 9);
            this.buttonAssemble.Name = "buttonAssemble";
            this.buttonAssemble.Size = new System.Drawing.Size(175, 23);
            this.buttonAssemble.TabIndex = 14;
            this.buttonAssemble.Text = "Assemble";
            this.buttonAssemble.UseVisualStyleBackColor = true;
            this.buttonAssemble.Click += new System.EventHandler(this.buttonAssemble_Click);
            // 
            // buttonExplode
            // 
            this.buttonExplode.Location = new System.Drawing.Point(17, 9);
            this.buttonExplode.Name = "buttonExplode";
            this.buttonExplode.Size = new System.Drawing.Size(175, 23);
            this.buttonExplode.TabIndex = 13;
            this.buttonExplode.Text = "Explode";
            this.buttonExplode.UseVisualStyleBackColor = true;
            this.buttonExplode.Click += new System.EventHandler(this.buttonExplode_Click);
            // 
            // treeViewExploded
            // 
            this.treeViewExploded.AllowDrop = true;
            this.treeViewExploded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewExploded.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewExploded.ContextMenuStrip = this.contextMenuTreeViewExploded;
            this.treeViewExploded.Location = new System.Drawing.Point(6, 38);
            this.treeViewExploded.Name = "treeViewExploded";
            this.treeViewExploded.Size = new System.Drawing.Size(436, 223);
            this.treeViewExploded.TabIndex = 12;
            this.treeViewExploded.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewExploded_DragDrop);
            this.treeViewExploded.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            // 
            // contextMenuTreeViewExploded
            // 
            this.contextMenuTreeViewExploded.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPathToClipboardToolStripMenuItem,
            this.openFolderInExplorerToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuTreeViewExploded.Name = "contextMenuTreeViewExploded";
            this.contextMenuTreeViewExploded.Size = new System.Drawing.Size(199, 92);
            // 
            // copyPathToClipboardToolStripMenuItem
            // 
            this.copyPathToClipboardToolStripMenuItem.Name = "copyPathToClipboardToolStripMenuItem";
            this.copyPathToClipboardToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.copyPathToClipboardToolStripMenuItem.Text = "Copy Path to Clipboard";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 353);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "String Dictionary file (v1.2+)";
            // 
            // textBoxStringDict
            // 
            this.textBoxStringDict.AllowDrop = true;
            this.textBoxStringDict.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStringDict.Location = new System.Drawing.Point(12, 369);
            this.textBoxStringDict.Name = "textBoxStringDict";
            this.textBoxStringDict.Size = new System.Drawing.Size(456, 20);
            this.textBoxStringDict.TabIndex = 5;
            this.textBoxStringDict.TextChanged += new System.EventHandler(this.textBoxStringDict_TextChanged);
            this.textBoxStringDict.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxStringDict_DragDrop);
            this.textBoxStringDict.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            // 
            // openFolderInExplorerToolStripMenuItem
            // 
            this.openFolderInExplorerToolStripMenuItem.Name = "openFolderInExplorerToolStripMenuItem";
            this.openFolderInExplorerToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.openFolderInExplorerToolStripMenuItem.Text = "Open Folder in Explorer";
            this.openFolderInExplorerToolStripMenuItem.Click += new System.EventHandler(this.openFolderInExplorerToolStripMenuItem_Click);
            // 
            // UnmungeForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 470);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBoxStringDict);
            this.Controls.Add(this.listUnmungeExe);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textFilename);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(496, 509);
            this.Name = "UnmungeForm";
            this.Text = "Run Unmunge";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.contextMenuUnmungeVersion.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageExtract.ResumeLayout(false);
            this.tabPageExplodeAssemble.ResumeLayout(false);
            this.contextMenuTreeViewExploded.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton buttonXbox;
        private System.Windows.Forms.RadioButton buttonPS2;
        private System.Windows.Forms.RadioButton buttonPC;
        private System.Windows.Forms.TextBox textFilename;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton buttonBF2Input;
        private System.Windows.Forms.RadioButton buttonBf1Input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton buttonBF2Output;
        private System.Windows.Forms.RadioButton buttonBf1Output;
        private System.Windows.Forms.Button buttonExtract;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton buttonDDS;
        private System.Windows.Forms.RadioButton buttonPNG;
        private System.Windows.Forms.RadioButton buttonTGA;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox check_collision;
        private System.Windows.Forms.CheckBox check_lod;
        private System.Windows.Forms.ContextMenuStrip contextMenuUnmungeVersion;
        private System.Windows.Forms.ToolStripMenuItem tryToRenameFilesWithHashedNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixRotationInlyrFilesToolStripMenuItem;
        private System.Windows.Forms.ListBox listUnmungeExe;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageExtract;
        private System.Windows.Forms.TabPage tabPageExplodeAssemble;
        private System.Windows.Forms.TreeView treeViewExploded;
        private System.Windows.Forms.Button buttonAssemble;
        private System.Windows.Forms.Button buttonExplode;
        private System.Windows.Forms.ContextMenuStrip contextMenuTreeViewExploded;
        private System.Windows.Forms.ToolStripMenuItem copyPathToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxStringDict;
        private System.Windows.Forms.Button buttonExplodeAssembleHelp;
        private System.Windows.Forms.ToolStripMenuItem openFolderInExplorerToolStripMenuItem;
    }
}