namespace LVLTool
{
    partial class MessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
            this.mOkButton = new System.Windows.Forms.Button();
            this.mCancelButton = new System.Windows.Forms.Button();
            this.mAuxButton = new System.Windows.Forms.Button();
            this.mStatusLabel = new System.Windows.Forms.Label();
            this.mTextBox = new SearchTextBox();
            this.SuspendLayout();
            // 
            // mOkButton
            // 
            this.mOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mOkButton.Location = new System.Drawing.Point(636, 416);
            this.mOkButton.Name = "mOkButton";
            this.mOkButton.Size = new System.Drawing.Size(75, 23);
            this.mOkButton.TabIndex = 0;
            this.mOkButton.Text = "&OK";
            this.mOkButton.UseVisualStyleBackColor = true;
            this.mOkButton.Click += new System.EventHandler(this.mOkButton_Click);
            // 
            // mCancelButton
            // 
            this.mCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancelButton.Location = new System.Drawing.Point(545, 416);
            this.mCancelButton.Name = "mCancelButton";
            this.mCancelButton.Size = new System.Drawing.Size(75, 23);
            this.mCancelButton.TabIndex = 2;
            this.mCancelButton.Text = "&Cancel";
            this.mCancelButton.UseVisualStyleBackColor = true;
            // 
            // mAuxButton
            // 
            this.mAuxButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mAuxButton.AutoSize = true;
            this.mAuxButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.mAuxButton.Location = new System.Drawing.Point(359, 416);
            this.mAuxButton.Name = "mAuxButton";
            this.mAuxButton.Size = new System.Drawing.Size(85, 23);
            this.mAuxButton.TabIndex = 3;
            this.mAuxButton.UseVisualStyleBackColor = true;
            this.mAuxButton.Visible = false;
            // 
            // mStatusLabel
            // 
            this.mStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mStatusLabel.AutoSize = true;
            this.mStatusLabel.Location = new System.Drawing.Point(13, 417);
            this.mStatusLabel.Name = "mStatusLabel";
            this.mStatusLabel.Size = new System.Drawing.Size(10, 13);
            this.mStatusLabel.TabIndex = 4;
            this.mStatusLabel.Text = " ";
            // 
            // mTextBox
            // 
            this.mTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mTextBox.Location = new System.Drawing.Point(1, 2);
            this.mTextBox.Name = "mTextBox";
            this.mTextBox.ReadOnly = true;
            this.mTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.mTextBox.SearchString = null;
            this.mTextBox.Size = new System.Drawing.Size(724, 408);
            this.mTextBox.StatusControl = null;
            this.mTextBox.TabIndex = 1;
            this.mTextBox.Text = "";
            this.mTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.mTextBox_LinkClicked);
            this.mTextBox.DoubleClick += new System.EventHandler(this.mTextBox_DoubleClick);
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mCancelButton;
            this.ClientSize = new System.Drawing.Size(726, 442);
            this.Controls.Add(this.mStatusLabel);
            this.Controls.Add(this.mAuxButton);
            this.Controls.Add(this.mCancelButton);
            this.Controls.Add(this.mTextBox);
            this.Controls.Add(this.mOkButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageForm";
            this.Text = "Message";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mOkButton;
        private SearchTextBox mTextBox;
        private System.Windows.Forms.Button mCancelButton;
        private System.Windows.Forms.Button mAuxButton;
        private System.Windows.Forms.Label mStatusLabel;
    }
}