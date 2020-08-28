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
    public partial class PlatformPrompt : Form
    {
        public PlatformPrompt()
        {
            InitializeComponent();
        }

        public Platform Platform
        {
            get
            {
                Platform retVal = Platform.None;
                if (mPcButton != null && mPcButton.Checked)
                    retVal = Platform.PC;
                else if (mXboxButton != null && mXboxButton.Checked)
                    retVal = Platform.XBOX;
                else if (mPs2Button != null && mPs2Button.Checked)
                    retVal = Platform.PS2;
                
                return retVal;
            }
        }

        public static Platform PromptForPlatform()
        {
            Platform retVal = Platform.None;
            PlatformPrompt pp = new PlatformPrompt();
            
            if (pp.ShowDialog() == DialogResult.OK)
                retVal = pp.Platform;
            pp.Dispose();

            return retVal;
        }
    }
}
