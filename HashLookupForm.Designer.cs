namespace LVLTool
{
    partial class HashLookupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HashLookupForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btn_lookup = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_addStrings = new System.Windows.Forms.Button();
            this.txt_hashMe = new System.Windows.Forms.TextBox();
            this.grp_hashMe = new System.Windows.Forms.GroupBox();
            this.lbl_hashMe = new System.Windows.Forms.Label();
            this.txt_output = new LVLTool.SearchTextBox();
            this.txt_input = new LVLTool.SearchTextBox();
            this.grp_hashMe.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter hashes to lookup here:";
            // 
            // btn_lookup
            // 
            this.btn_lookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_lookup.Location = new System.Drawing.Point(291, 24);
            this.btn_lookup.Name = "btn_lookup";
            this.btn_lookup.Size = new System.Drawing.Size(75, 50);
            this.btn_lookup.TabIndex = 2;
            this.btn_lookup.Text = "&Lookup";
            this.btn_lookup.UseVisualStyleBackColor = true;
            this.btn_lookup.Click += new System.EventHandler(this.mLookupButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Results:";
            // 
            // btn_addStrings
            // 
            this.btn_addStrings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_addStrings.Location = new System.Drawing.Point(293, 125);
            this.btn_addStrings.Name = "btn_addStrings";
            this.btn_addStrings.Size = new System.Drawing.Size(75, 86);
            this.btn_addStrings.TabIndex = 5;
            this.btn_addStrings.Text = "Add strings to internal dict";
            this.btn_addStrings.UseVisualStyleBackColor = true;
            this.btn_addStrings.Click += new System.EventHandler(this.mAddStringsButton_Click);
            // 
            // txt_hashMe
            // 
            this.txt_hashMe.Location = new System.Drawing.Point(6, 13);
            this.txt_hashMe.Name = "txt_hashMe";
            this.txt_hashMe.Size = new System.Drawing.Size(264, 20);
            this.txt_hashMe.TabIndex = 6;
            this.txt_hashMe.TextChanged += new System.EventHandler(this.mHashMeTextBox_TextChanged);
            // 
            // grp_hashMe
            // 
            this.grp_hashMe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grp_hashMe.Controls.Add(this.lbl_hashMe);
            this.grp_hashMe.Controls.Add(this.txt_hashMe);
            this.grp_hashMe.Location = new System.Drawing.Point(6, 310);
            this.grp_hashMe.Name = "grp_hashMe";
            this.grp_hashMe.Size = new System.Drawing.Size(367, 37);
            this.grp_hashMe.TabIndex = 7;
            this.grp_hashMe.TabStop = false;
            this.grp_hashMe.Text = "Hash Me";
            // 
            // lbl_hashMe
            // 
            this.lbl_hashMe.AutoSize = true;
            this.lbl_hashMe.Location = new System.Drawing.Point(276, 16);
            this.lbl_hashMe.Name = "lbl_hashMe";
            this.lbl_hashMe.Size = new System.Drawing.Size(114, 13);
            this.lbl_hashMe.TabIndex = 7;
            this.lbl_hashMe.Text = "type to see hash value";
            // 
            // txt_output
            // 
            this.txt_output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_output.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_output.Location = new System.Drawing.Point(12, 125);
            this.txt_output.Name = "txt_output";
            this.txt_output.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txt_output.SearchString = null;
            this.txt_output.Size = new System.Drawing.Size(273, 179);
            this.txt_output.StatusControl = null;
            this.txt_output.TabIndex = 3;
            this.txt_output.Text = "";
            // 
            // txt_input
            // 
            this.txt_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_input.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_input.Location = new System.Drawing.Point(12, 24);
            this.txt_input.Name = "txt_input";
            this.txt_input.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txt_input.SearchString = null;
            this.txt_input.Size = new System.Drawing.Size(273, 79);
            this.txt_input.StatusControl = null;
            this.txt_input.TabIndex = 0;
            this.txt_input.Text = "";
            this.txt_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mInputTextBox_KeyDown);
            // 
            // HashLookupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 355);
            this.Controls.Add(this.grp_hashMe);
            this.Controls.Add(this.btn_addStrings);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_output);
            this.Controls.Add(this.btn_lookup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_input);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HashLookupForm";
            this.Text = "Find Known Hash";
            this.grp_hashMe.ResumeLayout(false);
            this.grp_hashMe.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SearchTextBox txt_input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_lookup;
        private SearchTextBox txt_output;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_addStrings;
        private System.Windows.Forms.TextBox txt_hashMe;
        private System.Windows.Forms.GroupBox grp_hashMe;
        private System.Windows.Forms.Label lbl_hashMe;
    }
}