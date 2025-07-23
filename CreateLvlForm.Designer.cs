namespace LVLTool
{
    partial class CreateLvlForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateLvlForm));
            this.mListBox = new System.Windows.Forms.ListBox();
            this.mCreateButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supportedFileTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mPlatformComboBox = new System.Windows.Forms.ComboBox();
            this.mOutputFilenameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mOutputDirTextbox = new System.Windows.Forms.TextBox();
            this.mClearButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mListBox
            // 
            this.mListBox.AllowDrop = true;
            this.mListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mListBox.FormattingEnabled = true;
            this.mListBox.Location = new System.Drawing.Point(3, 16);
            this.mListBox.Name = "mListBox";
            this.mListBox.Size = new System.Drawing.Size(185, 238);
            this.mListBox.TabIndex = 0;
            this.mListBox.DragOver += new System.Windows.Forms.DragEventHandler(this.mListBox_DragOver);
            this.mListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.mListBox_DragDrop);
            // 
            // mCreateButton
            // 
            this.mCreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mCreateButton.Location = new System.Drawing.Point(7, 312);
            this.mCreateButton.Name = "mCreateButton";
            this.mCreateButton.Size = new System.Drawing.Size(75, 23);
            this.mCreateButton.TabIndex = 1;
            this.mCreateButton.Text = "Create";
            this.mCreateButton.UseVisualStyleBackColor = true;
            this.mCreateButton.Click += new System.EventHandler(this.mCreateButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.mListBox);
            this.groupBox1.Location = new System.Drawing.Point(6, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 270);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drop Items into listbox to add";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(350, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supportedFileTypesToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // supportedFileTypesToolStripMenuItem
            // 
            this.supportedFileTypesToolStripMenuItem.Name = "supportedFileTypesToolStripMenuItem";
            this.supportedFileTypesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.supportedFileTypesToolStripMenuItem.Text = "Supported File types";
            this.supportedFileTypesToolStripMenuItem.Click += new System.EventHandler(this.supportedFileTypesToolStripMenuItem_Click);
            // 
            // mPlatformComboBox
            // 
            this.mPlatformComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mPlatformComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mPlatformComboBox.FormattingEnabled = true;
            this.mPlatformComboBox.Items.AddRange(new object[] {
            "PC",
            "PS2",
            "XBOX"});
            this.mPlatformComboBox.Location = new System.Drawing.Point(203, 36);
            this.mPlatformComboBox.Name = "mPlatformComboBox";
            this.mPlatformComboBox.Size = new System.Drawing.Size(57, 21);
            this.mPlatformComboBox.TabIndex = 4;
            // 
            // mOutputFilenameTextbox
            // 
            this.mOutputFilenameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mOutputFilenameTextbox.Location = new System.Drawing.Point(201, 131);
            this.mOutputFilenameTextbox.Name = "mOutputFilenameTextbox";
            this.mOutputFilenameTextbox.Size = new System.Drawing.Size(135, 20);
            this.mOutputFilenameTextbox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(199, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Filename";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Output Dir:";
            // 
            // mOutputDirTextbox
            // 
            this.mOutputDirTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mOutputDirTextbox.Location = new System.Drawing.Point(203, 90);
            this.mOutputDirTextbox.Name = "mOutputDirTextbox";
            this.mOutputDirTextbox.Size = new System.Drawing.Size(135, 20);
            this.mOutputDirTextbox.TabIndex = 7;
            this.mOutputDirTextbox.Text = ".\\";
            // 
            // mClearButton
            // 
            this.mClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mClearButton.Location = new System.Drawing.Point(104, 312);
            this.mClearButton.Name = "mClearButton";
            this.mClearButton.Size = new System.Drawing.Size(75, 23);
            this.mClearButton.TabIndex = 9;
            this.mClearButton.Text = "Clear items";
            this.mClearButton.UseVisualStyleBackColor = true;
            this.mClearButton.Click += new System.EventHandler(this.mClearButton_Click);
            // 
            // CreateLvlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 347);
            this.Controls.Add(this.mClearButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mOutputDirTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mOutputFilenameTextbox);
            this.Controls.Add(this.mPlatformComboBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mCreateButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CreateLvlForm";
            this.Text = "Create a .lvl file";
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox mListBox;
        private System.Windows.Forms.Button mCreateButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supportedFileTypesToolStripMenuItem;
        private System.Windows.Forms.ComboBox mPlatformComboBox;
        private System.Windows.Forms.TextBox mOutputFilenameTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mOutputDirTextbox;
        private System.Windows.Forms.Button mClearButton;
    }
}