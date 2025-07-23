using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LVLTool
{
    public partial class SearchTextBox : RichTextBox
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SearchTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A status control to update.
        /// </summary>
        public Control StatusControl { get; set; }

        /// <summary>
        /// the currernt search string
        /// </summary>
        public string SearchString { get; set; }

        private void ContextMenuItem_Click(object source, System.EventArgs e)
        {
            if (source == mCutMenuItem)
                this.Cut();
            else if (source == mClearMenuItem)
                this.Text = "";
            else if (source == mCopyMenuItem)
            {
                //this.Copy();
                Clipboard.SetData(DataFormats.Text, this.SelectedText);
            }
            else if (source == mPasteMenuItem)
                this.Paste(DataFormats.GetFormat(DataFormats.Text));
            else if (source == mFindNextMenuItem)
                FindNextMatch();
            else if (source == mFindPrevMenuItem)
                FindPrevMatch();
            else if (source == mSelectAllMenuItem)
                this.SelectAll();
            else if (source == mFindMenuItem)
                if (SetSearchString()) FindNextMatch();
        }

        /// <summary>
        /// Overridden for searching on key strokes
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    Clipboard.SetData(DataFormats.Text, this.SelectedText);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F)
                {
                    if (SetSearchString())
                        FindNextMatch();
                }
                else if (e.KeyCode == Keys.F3)
                    FindPrevMatch();
                else if (e.KeyCode == Keys.L)
                    CutLine();
                else if (e.KeyCode == Keys.V)
                {
                    this.Paste(DataFormats.GetFormat(DataFormats.Text));
                    e.Handled = true;
                }
                
            }
            else if (e.Shift)
            {
                if (e.KeyCode == Keys.F3)
                    FindPrevMatch();
            }
            else if (e.KeyCode == Keys.F3)
                FindNextMatch();
            else if (e.KeyCode == Keys.F2)
                FindPrevMatch();

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Finds the next match for 'SearchString.
        /// </summary>
        /// <returns> the line number the match occured on</returns>
        public int FindPrevMatch()
        {
            int retVal = -1;
            if (SearchString != null && !SearchString.Equals(""))
            {
                Regex r = new Regex(SearchString, RegexOptions.IgnoreCase);
                MatchCollection mc = r.Matches(this.Text);
                Match m = null;
                if (mc.Count > 0)
                    m = mc[mc.Count - 1];
                else
                    goto end;

                int i = 0;
                while (mc[i].Index < this.SelectionStart - mc[i].Length)
                    m = mc[i++];
                if (m != null && m.Length != 0)
                {
                    this.SelectionStart = m.Index + m.Length;
                    retVal = this.GetLineFromCharIndex(this.SelectionStart);
                }
            }
        end:
            UpdateStatusControl(retVal);
            return retVal;
        }

        private void UpdateStatusControl(int retVal)
        {
            if (StatusControl != null)
            {
                if( retVal > -1)
                    StatusControl.Text = "Found, line: " + retVal;
                else
                    StatusControl.Text = "'" + this.SearchString + "' Not Found";
            }
        }

        /// <summary>
        /// Finds the next match of 'SearchString'
        /// </summary>
        /// <returns>The line number it occurs on</returns>
        public int FindNextMatch()
        {
            int retVal = -1;
            if (SearchString != null && !SearchString.Equals(""))
            {
                try
                {
                    Regex r;
                    r = new Regex(SearchString, RegexOptions.IgnoreCase);
                    int startAt = this.SelectionStart + 1;
                    if (startAt > this.Text.Length)
                        startAt = 0;

                    Match m = r.Match(this.Text, startAt);

                    if (m.Length == 0)
                    { // continue at the top if not found
                        m = r.Match(this.Text);
                    }
                    if (m.Length > 0)
                    {
                        this.SelectionStart = m.Index;
                        this.SelectionLength = m.Length;
                        retVal = this.GetLineFromCharIndex(this.SelectionStart);
                    }
                }
                catch { }
            }
            UpdateStatusControl(retVal);
            return retVal;
        }

        /// <summary>
        /// Gets the line the caret is on.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLine()
        {
            string retVal = "";
            int ls = GetLineStart();
            int le = GetLineEnd();
            int length = le - ls + 1;
            if (length > -1)
                retVal = this.Text.Substring(ls, length);
            return retVal;
        }

        /// <summary>
        /// Cuts the current line of text.
        /// </summary>
        private void CutLine()
        {
            int ls = GetLineStart();
            int le = GetLineEnd();
            int length = le - ls + 1;
            if (length > -1)
            {
                this.SelectionStart = ls;
                this.SelectionLength = length;
                this.Cut();
            }
        }
        /// <summary>
        /// returns the position of the start of the current line.
        /// </summary>
        /// <returns></returns>
        private int GetLineStart()
        {
            int i = 0;
            int textPosition = this.SelectionStart;
            if (textPosition >= this.Text.Length)
            {
                textPosition--;
            }
            int lineStart = 0;
            for (i = textPosition; i > 0; i--)
            {
                if (this.Text[i] == '\n')
                {
                    lineStart = i + 1;
                    break;
                }
            }
            return lineStart;
        }

        /// <summary>
        /// returns the position of the end of the current line.
        /// </summary>
        /// <returns></returns>
        private int GetLineEnd()
        {
            //			int ret = 0;
            int i = this.SelectionStart;
            if (i >= this.Text.Length)
            {
                return this.Text.Length - 1;
            }
            char current = this.Text[i];
            while (i < this.Text.Length /*&& current != ' ' && 
					current != ',' */
                                      && current != '\n')
            {
                //				ret++;
                i++;
                current = this.Text[i];
            }
            return i;
        }

        /// <summary>
        /// Prompts the user for a string to search for
        /// </summary>
        /// <returns>true if the search string was set</returns>
        public bool SetSearchString()
        {
            bool ret = false;
            if (this.SelectionLength > 0)
            {
                SearchString = this.Text.Substring(this.SelectionStart, this.SelectionLength);
            }
            string result = StringInputDlg.GetString(
                                           "Enter Search String",
                                           "Please enter text (or a regex) to search for.",
                                           SearchString);

            if (!result.Equals(""))
            {
                SearchString = result;
                ret = true;
            }
            return ret;
        }

    }
}
