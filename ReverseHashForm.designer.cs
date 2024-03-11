namespace ReverseHash
{
    partial class ReverseHashForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReverseHashForm));
            this.txtHash = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.spinLen = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPossibleCharacters = new System.Windows.Forms.TextBox();
            this.bthShowCmdLine = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spinLen)).BeginInit();
            this.SuspendLayout();
            // 
            // txtHash
            // 
            this.txtHash.BackColor = System.Drawing.Color.Black;
            this.txtHash.ForeColor = System.Drawing.Color.White;
            this.txtHash.Location = new System.Drawing.Point(12, 25);
            this.txtHash.Name = "txtHash";
            this.txtHash.Size = new System.Drawing.Size(100, 20);
            this.txtHash.TabIndex = 0;
            this.txtHash.Text = "0xa7d91d67";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hash To lookup";
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.BackColor = System.Drawing.Color.Black;
            this.txtOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.ForeColor = System.Drawing.Color.White;
            this.txtOutput.Location = new System.Drawing.Point(12, 124);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(349, 183);
            this.txtOutput.TabIndex = 2;
            this.txtOutput.Text = "";
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.BackColor = System.Drawing.Color.Gray;
            this.btnGo.Location = new System.Drawing.Point(280, 22);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "&Go";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Prefix:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.BackColor = System.Drawing.Color.Black;
            this.txtPrefix.ForeColor = System.Drawing.Color.White;
            this.txtPrefix.Location = new System.Drawing.Point(12, 64);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(100, 20);
            this.txtPrefix.TabIndex = 4;
            this.txtPrefix.Text = "is";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Max Len:";
            // 
            // spinLen
            // 
            this.spinLen.BackColor = System.Drawing.Color.Black;
            this.spinLen.ForeColor = System.Drawing.Color.White;
            this.spinLen.Location = new System.Drawing.Point(121, 65);
            this.spinLen.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinLen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinLen.Name = "spinLen";
            this.spinLen.Size = new System.Drawing.Size(62, 20);
            this.spinLen.TabIndex = 7;
            this.spinLen.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Char set:";
            // 
            // txtPossibleCharacters
            // 
            this.txtPossibleCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPossibleCharacters.BackColor = System.Drawing.Color.Black;
            this.txtPossibleCharacters.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPossibleCharacters.ForeColor = System.Drawing.Color.White;
            this.txtPossibleCharacters.Location = new System.Drawing.Point(69, 105);
            this.txtPossibleCharacters.Name = "txtPossibleCharacters";
            this.txtPossibleCharacters.Size = new System.Drawing.Size(292, 20);
            this.txtPossibleCharacters.TabIndex = 8;
            this.txtPossibleCharacters.Text = "abcdefghijklmnopqrstuvwxyz.0123456789";
            // 
            // bthShowCmdLine
            // 
            this.bthShowCmdLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bthShowCmdLine.BackColor = System.Drawing.Color.Gray;
            this.bthShowCmdLine.Location = new System.Drawing.Point(233, 51);
            this.bthShowCmdLine.Name = "bthShowCmdLine";
            this.bthShowCmdLine.Size = new System.Drawing.Size(122, 48);
            this.bthShowCmdLine.TabIndex = 10;
            this.bthShowCmdLine.Text = "Show Cmd line usage";
            this.bthShowCmdLine.UseVisualStyleBackColor = false;
            this.bthShowCmdLine.Visible = false;
            this.bthShowCmdLine.Click += new System.EventHandler(this.bthShowCmdLine_Click);
            // 
            // ReverseHashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(367, 315);
            this.Controls.Add(this.bthShowCmdLine);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPossibleCharacters);
            this.Controls.Add(this.spinLen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHash);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReverseHashForm";
            this.Text = "ReverseHashForm";
            ((System.ComponentModel.ISupportInitialize)(this.spinLen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown spinLen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPossibleCharacters;
        private System.Windows.Forms.Button bthShowCmdLine;
    }
}