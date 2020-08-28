namespace LVLTool
{
    partial class PlatformPrompt
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mPcButton = new System.Windows.Forms.RadioButton();
            this.mXboxButton = new System.Windows.Forms.RadioButton();
            this.mPs2Button = new System.Windows.Forms.RadioButton();
            this.mOkButton = new System.Windows.Forms.Button();
            this.mCancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mPs2Button);
            this.groupBox1.Controls.Add(this.mXboxButton);
            this.groupBox1.Controls.Add(this.mPcButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 46);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Platform";
            // 
            // mPcButton
            // 
            this.mPcButton.AutoSize = true;
            this.mPcButton.Checked = true;
            this.mPcButton.Location = new System.Drawing.Point(20, 20);
            this.mPcButton.Name = "mPcButton";
            this.mPcButton.Size = new System.Drawing.Size(39, 17);
            this.mPcButton.TabIndex = 0;
            this.mPcButton.TabStop = true;
            this.mPcButton.Text = "PC";
            this.mPcButton.UseVisualStyleBackColor = true;
            // 
            // mXboxButton
            // 
            this.mXboxButton.AutoSize = true;
            this.mXboxButton.Location = new System.Drawing.Point(80, 20);
            this.mXboxButton.Name = "mXboxButton";
            this.mXboxButton.Size = new System.Drawing.Size(54, 17);
            this.mXboxButton.TabIndex = 1;
            this.mXboxButton.Text = "XBOX";
            this.mXboxButton.UseVisualStyleBackColor = true;
            // 
            // mPs2Button
            // 
            this.mPs2Button.AutoSize = true;
            this.mPs2Button.Location = new System.Drawing.Point(155, 20);
            this.mPs2Button.Name = "mPs2Button";
            this.mPs2Button.Size = new System.Drawing.Size(45, 17);
            this.mPs2Button.TabIndex = 2;
            this.mPs2Button.Text = "PS2";
            this.mPs2Button.UseVisualStyleBackColor = true;
            // 
            // mOkButton
            // 
            this.mOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mOkButton.Location = new System.Drawing.Point(108, 133);
            this.mOkButton.Name = "mOkButton";
            this.mOkButton.Size = new System.Drawing.Size(75, 23);
            this.mOkButton.TabIndex = 1;
            this.mOkButton.Text = "&OK";
            this.mOkButton.UseVisualStyleBackColor = true;
            // 
            // mCancelButton
            // 
            this.mCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancelButton.Location = new System.Drawing.Point(189, 133);
            this.mCancelButton.Name = "mCancelButton";
            this.mCancelButton.Size = new System.Drawing.Size(75, 23);
            this.mCancelButton.TabIndex = 2;
            this.mCancelButton.Text = "&Cancel";
            this.mCancelButton.UseVisualStyleBackColor = true;
            // 
            // PlatformPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 168);
            this.Controls.Add(this.mCancelButton);
            this.Controls.Add(this.mOkButton);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(283, 207);
            this.MinimumSize = new System.Drawing.Size(283, 207);
            this.Name = "PlatformPrompt";
            this.Text = "Which Platform?";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton mPs2Button;
        private System.Windows.Forms.RadioButton mXboxButton;
        private System.Windows.Forms.RadioButton mPcButton;
        private System.Windows.Forms.Button mOkButton;
        private System.Windows.Forms.Button mCancelButton;
    }
}