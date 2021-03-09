namespace LVLTool
{
    partial class HashFinderForm
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
            this.mPrefixTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mSuffixTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mCharacterSetTextBox = new System.Windows.Forms.TextBox();
            this.mResultsTextBox = new System.Windows.Forms.RichTextBox();
            this.mGoButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.mTargetTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mPrefixTextBox
            // 
            this.mPrefixTextBox.Location = new System.Drawing.Point(12, 66);
            this.mPrefixTextBox.Name = "mPrefixTextBox";
            this.mPrefixTextBox.Size = new System.Drawing.Size(100, 20);
            this.mPrefixTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Prefix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Suffix";
            // 
            // mSuffixTextBox
            // 
            this.mSuffixTextBox.Location = new System.Drawing.Point(12, 105);
            this.mSuffixTextBox.Name = "mSuffixTextBox";
            this.mSuffixTextBox.Size = new System.Drawing.Size(100, 20);
            this.mSuffixTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Character Set";
            // 
            // mCharacterSetTextBox
            // 
            this.mCharacterSetTextBox.Location = new System.Drawing.Point(12, 25);
            this.mCharacterSetTextBox.Name = "mCharacterSetTextBox";
            this.mCharacterSetTextBox.Size = new System.Drawing.Size(100, 20);
            this.mCharacterSetTextBox.TabIndex = 4;
            // 
            // mResultsTextBox
            // 
            this.mResultsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mResultsTextBox.Location = new System.Drawing.Point(12, 142);
            this.mResultsTextBox.Name = "mResultsTextBox";
            this.mResultsTextBox.Size = new System.Drawing.Size(490, 213);
            this.mResultsTextBox.TabIndex = 6;
            this.mResultsTextBox.Text = "";
            // 
            // mGoButton
            // 
            this.mGoButton.Location = new System.Drawing.Point(313, 21);
            this.mGoButton.Name = "mGoButton";
            this.mGoButton.Size = new System.Drawing.Size(75, 23);
            this.mGoButton.TabIndex = 7;
            this.mGoButton.Text = "Go";
            this.mGoButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Target Hash";
            // 
            // mTargetTextBox
            // 
            this.mTargetTextBox.Location = new System.Drawing.Point(153, 24);
            this.mTargetTextBox.Name = "mTargetTextBox";
            this.mTargetTextBox.Size = new System.Drawing.Size(100, 20);
            this.mTargetTextBox.TabIndex = 9;
            this.mTargetTextBox.Text = "0x";
            // 
            // HashFinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 367);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mTargetTextBox);
            this.Controls.Add(this.mGoButton);
            this.Controls.Add(this.mResultsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mCharacterSetTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mSuffixTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mPrefixTextBox);
            this.Name = "HashFinderForm";
            this.Text = "HashFinderForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mPrefixTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mSuffixTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mCharacterSetTextBox;
        private System.Windows.Forms.RichTextBox mResultsTextBox;
        private System.Windows.Forms.Button mGoButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox mTargetTextBox;
    }
}