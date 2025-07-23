using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LVLTool_Test
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            txtResults.StatusControl = labelStatus;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtResults.Text = String.Format("Current Folder: {0}\n", 
                Directory.GetCurrentDirectory());

            List<String> tests = Program.GetTestNames();
            tests.Insert(0, "All");
            comboTests.Items.AddRange(tests.ToArray());
            comboTests.SelectedIndex = 0;
        }

        private void btnRunTests_Click(object sender, EventArgs e)
        {
            if(comboTests.Text == "All")
                txtResults.Text = Program.RunTests();
            else
                txtResults.Text = Program.RunTest(comboTests.Text);
        }
    }
}
