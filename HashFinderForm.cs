using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LVLTool
{
    public partial class HashFinderForm : Form
    {
        public HashFinderForm()
        {
            InitializeComponent();
        }


        public static UInt32 HashString(char[] input)
        {
            UInt32 FNV_prime = 16777619;
            UInt32 offset_basis = 2166136261;
            UInt32 hash = offset_basis;
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

        private void State1()
        {
            mTargetTextBox.Enabled = mPrefixTextBox.Enabled = mSuffixTextBox.Enabled =
                mCharacterSetTextBox.Enabled = true;
            mGoButton.Text = "Go";
        }

        private void State2()
        {
            mTargetTextBox.Enabled = mPrefixTextBox.Enabled = mSuffixTextBox.Enabled =
                mCharacterSetTextBox.Enabled = false;
            mGoButton.Text = "Stop";
        }

        private UInt32 GetTargetHash()
        {
            UInt32 retVal = 0;
            string str = mTargetTextBox.Text;
            if (str.StartsWith("0x"))
            {
                retVal = UInt32.Parse(str.Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            else
            {
                retVal = UInt32.Parse(str);
            }
            return retVal;
        }

        private void Compute(UInt32 targetHash, string charSet, string prefix, string suffix)
        {
            //UInt32 target = GetTargetHash();
            //string testStr = "";
            //// go from 1 - 15 length
            //int limit = 15;
            //char[]  buffer  = null;
            //char[][] indexes = new int[charSet.Length,limit];
            //for (int i = 0; i < limit; i++)
            //{
            //    for (int j = 0; i < charSet.Length; j++)
            //    {
            //        buffer = new char[i];
                    
            //    }
            //}
        }

        private void CheckResult(char[] test, UInt32 target)
        {
            UInt32 tst = HashString(test);
            if (target == tst)
            {
                string dude = new String(test);
                AppendResult(String.Format("0x{0:x} // \"{1}\"\n",tst, dude));
            }
        }

        public delegate void AppendDelegate(string result);

        private void AppendResult(string result)
        {
            if (mResultsTextBox.InvokeRequired)
            {
                object[] myArray = new object[1];
                myArray[0] = result;
                mResultsTextBox.BeginInvoke(new AppendDelegate(AppendResult), myArray);
            }
            else
                mResultsTextBox.AppendText(result);
        }
    }
}
