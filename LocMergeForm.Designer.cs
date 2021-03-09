namespace LVLTool
{
    partial class LocMergeForm
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
            this.mAssetListBox = new System.Windows.Forms.ListBox();
            this.mLVLFileTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mMergeButton = new System.Windows.Forms.Button();
            this.mBrowseLVL = new System.Windows.Forms.Button();
            this.mStatusLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mAssetListBox
            // 
            this.mAssetListBox.AllowDrop = true;
            this.mAssetListBox.BackColor = System.Drawing.Color.Black;
            this.mAssetListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mAssetListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mAssetListBox.ForeColor = System.Drawing.Color.White;
            this.mAssetListBox.FormattingEnabled = true;
            this.mAssetListBox.ItemHeight = 16;
            this.mAssetListBox.Location = new System.Drawing.Point(3, 16);
            this.mAssetListBox.Name = "mAssetListBox";
            this.mAssetListBox.Size = new System.Drawing.Size(470, 244);
            this.mAssetListBox.TabIndex = 21;
            this.mAssetListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.mAssetListBox_DragDrop);
            this.mAssetListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.mLVLFileTextBox_DragEnter);
            // 
            // mLVLFileTextBox
            // 
            this.mLVLFileTextBox.AllowDrop = true;
            this.mLVLFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mLVLFileTextBox.BackColor = System.Drawing.Color.Black;
            this.mLVLFileTextBox.ForeColor = System.Drawing.Color.White;
            this.mLVLFileTextBox.Location = new System.Drawing.Point(26, 34);
            this.mLVLFileTextBox.Name = "mLVLFileTextBox";
            this.mLVLFileTextBox.Size = new System.Drawing.Size(440, 20);
            this.mLVLFileTextBox.TabIndex = 20;
            this.mLVLFileTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.mLVLFileTextBox_DragDrop);
            this.mLVLFileTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.mLVLFileTextBox_DragEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.mAssetListBox);
            this.groupBox1.Location = new System.Drawing.Point(23, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(476, 274);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drag core.lvl files into here";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Drag Base core.lvl file into here:";
            // 
            // mMergeButton
            // 
            this.mMergeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mMergeButton.Location = new System.Drawing.Point(395, 340);
            this.mMergeButton.Name = "mMergeButton";
            this.mMergeButton.Size = new System.Drawing.Size(104, 23);
            this.mMergeButton.TabIndex = 24;
            this.mMergeButton.Text = "Merge Strings";
            this.mMergeButton.UseVisualStyleBackColor = true;
            this.mMergeButton.Click += new System.EventHandler(this.mMergeButton_Click);
            // 
            // mBrowseLVL
            // 
            this.mBrowseLVL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mBrowseLVL.Location = new System.Drawing.Point(471, 34);
            this.mBrowseLVL.Name = "mBrowseLVL";
            this.mBrowseLVL.Size = new System.Drawing.Size(28, 20);
            this.mBrowseLVL.TabIndex = 25;
            this.mBrowseLVL.Text = "...";
            this.mBrowseLVL.UseVisualStyleBackColor = true;
            // 
            // mStatusLabel
            // 
            this.mStatusLabel.AutoSize = true;
            this.mStatusLabel.Location = new System.Drawing.Point(28, 364);
            this.mStatusLabel.Name = "mStatusLabel";
            this.mStatusLabel.Size = new System.Drawing.Size(0, 13);
            this.mStatusLabel.TabIndex = 26;
            // 
            // LocMergeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 386);
            this.Controls.Add(this.mStatusLabel);
            this.Controls.Add(this.mBrowseLVL);
            this.Controls.Add(this.mMergeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mLVLFileTextBox);
            this.Name = "LocMergeForm";
            this.Text = "LocMergeForm";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox mAssetListBox;
        private System.Windows.Forms.TextBox mLVLFileTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button mMergeButton;
        private System.Windows.Forms.Button mBrowseLVL;
        private System.Windows.Forms.Label mStatusLabel;
    }
}