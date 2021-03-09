using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LVLTool
{
    public partial class MessageForm : Form
    {
        /// <summary>
        /// Raised when user doubleClicks in the textbox
        /// </summary>
        public event EventHandler TextClicked;

        public MessageForm(Icon icon)
        {
            InitializeComponent();
            this.Icon = icon;
            mTextBox.StatusControl = this.mStatusLabel;
        }

        public bool ShowCancelButton
        {
            set { this.mCancelButton.Visible = value; }
        }

        public bool MessageEditable
        {
            get { return !mTextBox.ReadOnly;  }
            set { mTextBox.ReadOnly = !value; }
        }

        /// <summary>
        /// sets the text on the Aux button.
        /// Aux button returns a dialog result of 'Abort'
        /// </summary>
        public string AuxButtonText
        {
            get { return mAuxButton.Text; }

            set
            {
                mAuxButton.Text = value;
                mAuxButton.Visible = (mAuxButton.Text.Length > 0);
            }
        }

        /// <summary>
        /// The text to show 
        /// </summary>
        public string MessageText 
        { 
            get { return mTextBox.Text; } 
            set { mTextBox.Text = value; } 
        }

        private void mOkButton_Click(object sender, EventArgs e)
        {
            if (!this.Modal)
                Close();
        }

        private void mTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (TextClicked != null)
            {
                this.TextClicked(this, new StringEventArgs(mTextBox.GetCurrentLine()));
            }
        }

        /// <summary>
        /// Get a string from the user.
        /// </summary>
        /// <param name="title">The title of the dialog</param>
        /// <param name="message">The initial message to display.</param>
        /// <returns>valid string when the user hits 'ok', null when they cancel.</returns>
        public static string GetString(string title, string message)
        {
            return ShowMessage(title, message, SystemIcons.Question, true, true);
        }

        /// <summary>
        /// Returns the MessageText of the Form
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="message">The initial message text to display</param>
        /// <param name="icon">the Icon to use</param>
        /// <param name="editable">true to allow the user to edit the MessageText</param>
        /// <returns>The resulting MessageText</returns>
        public static string ShowMessage(string title, string message, Icon icon, bool editable, bool showCancelButton)
        {
            string retVal = null;
            MessageForm mf = new MessageForm(icon);
            mf.MessageEditable = editable;
            mf.Text = title;
            mf.MessageText = message;
            mf.ShowCancelButton = showCancelButton;

            if (mf.ShowDialog() == DialogResult.OK)
            {
                retVal = mf.MessageText;
            }
            mf.Dispose();
            return retVal;
        }

        /// <summary>
        /// Shows a message
        /// </summary>
        /// <param name="title">the title bar text</param>
        /// <param name="message">the message to show</param>
        public static void ShowMessage(string title, string message)
        {
            ShowMessage(title, message, SystemIcons.Hand, false, false);
        }

        private void mTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        public void Colorize(Regex reg, Color color)
        {
            mTextBox.Visible = false;
            mTextBox.SelectionLength = 0;
            MatchCollection mc = reg.Matches(mTextBox.Text);
            foreach (Match m in mc)
            {
                mTextBox.SelectionStart = m.Index;
                mTextBox.SelectionLength = m.Length;// -1;
                mTextBox.SelectionColor = color;
            }
            mTextBox.Visible = true;
        }

        /* Regex mColorizeRegex = new Regex("^[A-Z]+,[A-Za-z \\.']+,[A-Z,a-z ']+,", RegexOptions.Multiline); */
    }

    public class StringEventArgs : EventArgs
    {
        /// <summary>
        /// The string value
        /// </summary>
        public string Value { get; set; }

        public StringEventArgs(string val)
        {
            Value = val;
        }
    }
}
