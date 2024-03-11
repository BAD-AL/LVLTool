using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ReverseHash
{
    public partial class ReverseHashForm : Form, IAppendText
    {
        public ReverseHashForm()
        {
            InitializeComponent();
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

        #region IAppendText Members

        public void AppendText(string s)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Invoke an anonymous method on the thread of the form.
                this.Invoke((MethodInvoker)delegate
                {
                    AppendText(s);
                });
            }
            else
            {
                try
                {
                    txtOutput.AppendText(s);
                }
                catch {
                    Console.WriteLine("ReverseHashForm.AppendText: Error Appending Text");
                }
            }
        }

        private void SetGoButtonText(string s)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Invoke an anonymous method on the thread of the form.
                this.Invoke((MethodInvoker)delegate {
                    SetGoButtonText(s);
                });
            }
            else
            {
                btnGo.Text = s;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            try
            {
                if (workerThread != null && workerThread.IsAlive)
                {
                    workerThread.Abort();
                    workerThread = null;
                }
            }
            catch
            {
                Console.WriteLine("ReverseHashForm.OnClosing: Error closing worker thread");
            }
        }

        #endregion

        Thread workerThread = null;

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (workerThread == null)
            {
                txtOutput.Clear();
                _prefix = txtPrefix.Text;
                string hashStr = txtHash.Text;

                if (hashStr.StartsWith("0x"))
                    _hashToLookUp = Convert.ToUInt32(hashStr.Substring(2), 16);
                else
                    _hashToLookUp = Convert.ToUInt32(hashStr);
                _maxLen = (int)spinLen.Value;
                _possibleChars = txtPossibleCharacters.Text;

                btnGo.Text = "Stop";
                workerThread = new Thread(new ThreadStart(DoWork));
                workerThread.Start();
            }
            else
            {
                workerThread.Abort();
                workerThread = null;
                SetGoButtonText("&Go");
                AppendText("Worker Thread killed");
            }
        }

        private string _prefix = "";
        private string _possibleChars = "";
        private UInt32 _hashToLookUp = 0;
        int _maxLen = 0;

        private void DoWork()
        {
            HashReverse hr = new HashReverse();
            hr.AppendObj = this;
            //AppendText( string.Format("Running command:\n  ReverseHash 0x{0} -p:{1} -c:{2} -l:{3}\n", 
            //    _hashToLookUp, _prefix, _possibleChars, _maxLen));
            hr.PrintMatches(_hashToLookUp, _possibleChars,
               _prefix.Length + 1, _prefix.Length + _maxLen, _prefix);
            SetGoButtonText("&Go");
        }

        private void bthShowCmdLine_Click(object sender, EventArgs e)
        {
            _prefix = txtPrefix.Text;
            string hashStr = txtHash.Text;

            if (hashStr.StartsWith("0x"))
                _hashToLookUp = Convert.ToUInt32(hashStr.Substring(2), 16);
            else
                _hashToLookUp = Convert.ToUInt32(hashStr);
            _maxLen = (int)spinLen.Value;
            _possibleChars = txtPossibleCharacters.Text;

            AppendText(string.Format("  ReverseHash.exe 0x{0} -p:{1} -c:{2} -l:{3}\n",
                    _hashToLookUp, _prefix, _possibleChars, _maxLen));
        }
    }

    public interface IAppendText
    {
        void AppendText(string s);
    }

    public class HashReverse
    {
        private IAppendText appendObj = null;

        public IAppendText AppendObj
        {
            get { return appendObj; }
            set { appendObj = value; }
        }

        void Print(string s)
        {
            if (!s.EndsWith("\n"))
                s += "\n";
            if (appendObj == null)
                Console.Write(s);
            else
                appendObj.AppendText(s);
        }

        uint HashString(char[] input)
        {
            uint FNV_prime = 16777619;
            uint offset_basis = 2166136261;
            uint hash = offset_basis;
            byte c = 0;
            foreach (char c_ in input)
            {
                c = (byte)c_;
                c |= 0x20;
                hash ^= c;
                hash *= FNV_prime;
            }
            return hash;
        }

        public void PrintMatches(
            UInt32 hash,
            string possibleCharacters,
            int targetStringSizeBegin,
            int targetStringSizeEnd,
            string prefix
        )
        {
            DateTime start = DateTime.Now;
            Print(string.Format("Searching matches for 0x{0:x}\n with prefix '{1}'\n using char set '{2}'\n startLen: {3}\n endLen: {4} (range: {5})\n",
                hash, prefix, possibleCharacters, targetStringSizeBegin, targetStringSizeEnd, (targetStringSizeEnd - targetStringSizeBegin)));
            for (int i = targetStringSizeBegin; i < targetStringSizeEnd; i++)
            {
                Print(string.Format("searching string length = {0}; guess_len:{1}", i, i - prefix.Length));
                PrintMatches(hash, possibleCharacters, i, prefix);
            }
            DateTime end = DateTime.Now;
            var timeTaken = end - start;
            Print(string.Format("Finished Searching matches for 0x{0:x}; time: {1}ms ", hash, timeTaken.TotalMilliseconds));
        }

        void PrintMatches(uint hash, string possibleCharacters, int targetStringSize, string prefix)
        {
            char[] bufferToUse = new char[targetStringSize];
            prefix.CopyTo(0, bufferToUse, 0, prefix.Length);
            GenerateStringsAndCheckHash(prefix.Length, targetStringSize, hash, possibleCharacters, bufferToUse);
        }

        void GenerateStringsAndCheckHash(int index, int targetStringSize, uint targetHash, string possibleCharacters, char[] bufferToUse)
        {
            if (index == targetStringSize)
            {
                if (HashString(bufferToUse) == targetHash)
                {
                    Print(string.Format("Match found: '{0}'", new string(bufferToUse)));
                }
            }
            else
            {
                foreach (char c in possibleCharacters)
                {
                    bufferToUse[index] = c;
                    GenerateStringsAndCheckHash(index + 1, targetStringSize, targetHash, possibleCharacters, bufferToUse);
                }
            }
        }
    }
}
