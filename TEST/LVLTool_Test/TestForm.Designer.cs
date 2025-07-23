namespace LVLTool_Test
{
    partial class TestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.txtResults = new LVLTool.SearchTextBox();
            this.btnRunTests = new System.Windows.Forms.Button();
            this.comboTests = new System.Windows.Forms.ComboBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.BackColor = System.Drawing.Color.Black;
            this.txtResults.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtResults.Location = new System.Drawing.Point(12, 50);
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtResults.SearchString = null;
            this.txtResults.Size = new System.Drawing.Size(978, 323);
            this.txtResults.StatusControl = null;
            this.txtResults.TabIndex = 5;
            this.txtResults.Text = "";
            // 
            // btnRunTests
            // 
            this.btnRunTests.Location = new System.Drawing.Point(12, 21);
            this.btnRunTests.Name = "btnRunTests";
            this.btnRunTests.Size = new System.Drawing.Size(75, 23);
            this.btnRunTests.TabIndex = 1;
            this.btnRunTests.Text = "Run Test(s)";
            this.btnRunTests.UseVisualStyleBackColor = true;
            this.btnRunTests.Click += new System.EventHandler(this.btnRunTests_Click);
            // 
            // comboTests
            // 
            this.comboTests.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTests.FormattingEnabled = true;
            this.comboTests.Location = new System.Drawing.Point(118, 23);
            this.comboTests.Name = "comboTests";
            this.comboTests.Size = new System.Drawing.Size(362, 25);
            this.comboTests.TabIndex = 6;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 378);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(13, 13);
            this.labelStatus.TabIndex = 7;
            this.labelStatus.Text = "=";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 400);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.comboTests);
            this.Controls.Add(this.btnRunTests);
            this.Controls.Add(this.txtResults);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestForm";
            this.Text = "LVLTool Test Runner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LVLTool.SearchTextBox txtResults;
        private System.Windows.Forms.Button btnRunTests;
        private System.Windows.Forms.ComboBox comboTests;
        private System.Windows.Forms.Label labelStatus;
    }
}

