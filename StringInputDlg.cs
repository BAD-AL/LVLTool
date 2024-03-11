using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;

namespace LVLTool
{
	/// <summary>
	/// Summary description for StringInputDlg.
	/// </summary>
	public class StringInputDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button mOkButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox mUserInput;
		private string result = "";
		private System.Windows.Forms.Button mCancelButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StringInputDlg(string title, string message, string initialText)
		{
			InitializeComponent();
			this.Text= title;
			this.label1.Text = message;
			this.mUserInput.Text= initialText;
			this.mUserInput.SelectAll();
		}


        protected override void OnHandleCreated(EventArgs e)
        {
            //https://stackoverflow.com/questions/57124243/winforms-dark-title-bar-on-windows-10 (thank you:)
            if (DwmSetWindowAttribute(this.Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(this.Handle, 20, new[] { 1 }, 4);
            base.OnHandleCreated(e);
        }

        [System.Runtime.InteropServices.DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

		public static string GetString(string title, string message)
		{
			StringInputDlg sid = new StringInputDlg(title, message,"");
			sid.ShowDialog();
			string ret = sid.getResult();
			sid.Dispose();
			return ret;
		}

		public static string GetString(string title, string message, string initialText)
		{
			StringInputDlg sid = new StringInputDlg(title, message, initialText);
			sid.ShowDialog();
			string ret = sid.getResult();
			sid.Dispose();
			return ret;
		}

		public string getResult()
		{
			return result;
		}

        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Prompt the user for input for a 'set' command
        /// </summary>
        /// <param name="input">The input string to operate on</param>
        /// <returns>empty string on cancel, 'SET' string when successful.</returns>
        public static string PromptForSetUserInput(string input)
        {
            string retVal = "";
            string msg = GetPromptUserMesage(input);
            string location = GetSetLocation(input);

            while (retVal == "")
            {
                StringBuilder rangeMsg = new StringBuilder(25);
                String userInputValue = "";
                int min = 0;
                int max = 0;
                //SET(0x2224B, {32TeamNES,28TeamNES PromptUser:Msg="Enter desired quarter length":int(1-15)} )
                //SET(0x2224B, {32TeamNES,28TeamNES PromptUser:Msg="Enter desired quarter length":int(0x1-0x15)} )
                //SET(0x2224B, {32TeamNES,28TeamNES PromptUser:Msg="Enter name of...":string(len=8)} ) maybe not this one.
                if (input.IndexOf("int", StringComparison.OrdinalIgnoreCase) > -1 &&
                    (GetHexRange(ref min, ref max, input, rangeMsg) || GetDecimalRange(ref min, ref max, input, rangeMsg)))
                {
                    string rangeMessage = rangeMsg.ToString();
                    NumberStyles style = rangeMessage.IndexOf('x') > 0 ? NumberStyles.HexNumber : NumberStyles.Integer;
                    string initialText = style == NumberStyles.HexNumber ? "0x" : "";
                    StringInputDlg dlg = new StringInputDlg(msg, rangeMessage, initialText);
                    int userVal = Int32.MinValue;

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        userInputValue = dlg.getResult();
                        if (userInputValue.StartsWith("0x"))
                            userInputValue = userInputValue.Substring(2);

                        try
                        {
                            userVal = Int32.Parse(userInputValue, style);
                            if (userVal < min || userVal > max)
                            {
                                userVal = Int32.MinValue;
                                throw new Exception("Invalid input.");
                            }
                        }
                        catch (Exception )
                        {
                            ShowError(string.Format(
                                "Error with '{0}'. Value '{1}' is invalid for range {2}",
                                msg, userInputValue, rangeMessage));
                        }
                        if (userVal > Int32.MinValue)
                        {
                            retVal = string.Format("SET({0}, 0x{1:x})", location, userVal);
                        }
                    }
                    else
                    {
                        retVal = " "; // trigger us to leave the loop
                    }
                    dlg.Dispose();
                }
                else
                {
                    ShowError("ERROR applying line: " + input);
                    retVal = null;
                }
            }
            return retVal;
        }

        private static string GetSetLocation(string input)
        {
            string retVal = "";
            Regex locationRegex = new Regex(@"SET\s*\(\s*(0x[0-9a-fA-F]+)\s*,");

            Match m = locationRegex.Match(input);
            if (m == Match.Empty)
            {
                ShowError(string.Format("SET function not used properly. incorrect syntax>'{0}'", input));
            }
            else
            {
                retVal = m.Groups[1].ToString().ToLower();
            }
            return retVal;
        }

        private static bool GetHexRange(ref int min, ref int max, string input, StringBuilder rangeString)
        {
            bool retVal = false;
            Regex hexRangeRegex = new Regex(@"\(\s*0x([0-9a-fA-F]+)\s*-\s*0x([0-9a-fA-F]+)\s*\)");
            Match m = hexRangeRegex.Match(input);
            if (m != Match.Empty)
            {
                min = Int32.Parse(m.Groups[1].ToString(), System.Globalization.NumberStyles.HexNumber);
                max = Int32.Parse(m.Groups[2].ToString(), System.Globalization.NumberStyles.HexNumber);
                rangeString.Append("0x");
                rangeString.Append(m.Groups[1].ToString());
                rangeString.Append("-");
                rangeString.Append("0x");
                rangeString.Append(m.Groups[2].ToString());
                retVal = true;
            }
            return retVal;
        }

        private static bool GetDecimalRange(ref int min, ref int max, string input, StringBuilder rangeString)
        {
            bool retVal = false;
            Regex decRangeRegex = new Regex(@"\(\s*([0-9]+)\s*-\s*([0-9]+)\s*\)");
            Match m = decRangeRegex.Match(input);
            if (m != Match.Empty)
            {
                min = Int32.Parse(m.Groups[1].ToString());
                max = Int32.Parse(m.Groups[2].ToString());
                rangeString.Append(m.Groups[1].ToString());
                rangeString.Append("-");
                rangeString.Append(m.Groups[2].ToString());
                retVal = true;
            }
            return retVal;
        }
        private static string GetPromptUserMesage(string input)
        {
            int msgStartLoc = input.IndexOf("Msg=\"", StringComparison.OrdinalIgnoreCase) + 5;
            string msg = "";
            if (msgStartLoc > 5)
            {
                int endIndex = input.IndexOf('"', msgStartLoc + 1);
                if (endIndex > -1)
                    msg = input.Substring(msgStartLoc, endIndex - msgStartLoc);
            }
            return msg;
        }


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringInputDlg));
            this.mOkButton = new System.Windows.Forms.Button();
            this.mCancelButton = new System.Windows.Forms.Button();
            this.mUserInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mOkButton
            // 
            this.mOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mOkButton.Location = new System.Drawing.Point(65, 64);
            this.mOkButton.Name = "mOkButton";
            this.mOkButton.Size = new System.Drawing.Size(60, 23);
            this.mOkButton.TabIndex = 1;
            this.mOkButton.Text = "OK";
            this.mOkButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // mCancelButton
            // 
            this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancelButton.Location = new System.Drawing.Point(141, 64);
            this.mCancelButton.Name = "mCancelButton";
            this.mCancelButton.Size = new System.Drawing.Size(60, 23);
            this.mCancelButton.TabIndex = 2;
            this.mCancelButton.Text = "Cancel";
            this.mCancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // mUserInput
            // 
            this.mUserInput.Location = new System.Drawing.Point(56, 40);
            this.mUserInput.Name = "mUserInput";
            this.mUserInput.Size = new System.Drawing.Size(200, 20);
            this.mUserInput.TabIndex = 0;
            this.mUserInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userInput_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(56, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 32);
            this.label1.TabIndex = 3;
            // 
            // StringInputDlg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(282, 104);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mUserInput);
            this.Controls.Add(this.mCancelButton);
            this.Controls.Add(this.mOkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StringInputDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StringInputDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private void userInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) 
		{
			if(e.KeyCode == Keys.Enter)
			{
				//result = userInput.Text;
				okButton_Click(sender, new System.EventArgs());
            }
            else if (e.KeyCode == Keys.Escape)
            {
                cancelButton_Click(sender, e);
            }
		}

		private void okButton_Click(object sender, System.EventArgs e) 
		{
			result = mUserInput.Text;
			this.DialogResult = DialogResult.OK;
		}

		private void cancelButton_Click(object sender, System.EventArgs e) 
		{
			result = "";
			this.Close();
		}
	}
}
