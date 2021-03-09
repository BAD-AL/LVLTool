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
    public partial class LocMergeForm : Form
    {
        public LocMergeForm()
        {
            InitializeComponent();
        }

        private void mLVLFileTextBox_DragDrop(object sender, DragEventArgs e)
        {
            Control tb = sender as Control;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length == 1 && tb != null)
            {
                tb.Text = files[0];
            }
            mStatusLabel.Text = "";
        }

        private void mLVLFileTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void mAssetListBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            StringBuilder errList = new StringBuilder();
            int lastSlash = 0;
            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    if (!AddItem(files[i]))
                    {
                        lastSlash = files[i].LastIndexOf("\\") + 1;
                        if (lastSlash > 0)
                        {
                            errList.Append("  ");
                            errList.Append(files[i].Substring(lastSlash));
                            errList.Append("\n");
                        }
                    }
                }
                if (errList.Length > 0)
                {
                    MessageBox.Show(String.Format(
                        "Error:\n{0} ???",
                        errList.ToString()));
                }
            }
            mStatusLabel.Text = "";
        }

        public bool AddItem(string file)
        {
            mAssetListBox.Items.Add(file);
            return true;
        }

        private void mMergeButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = mLVLFileTextBox.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CoreMerge cm = new CoreMerge(mLVLFileTextBox.Text);
                foreach (object item in mAssetListBox.Items)
                {
                    cm.GatherStrings(item.ToString());
                }
                cm.Save(dlg.FileName);
                mStatusLabel.Text = "Saved to: " + dlg.FileName;
            }
            dlg.Dispose();
        }


        internal void SetStyle(Form prevForm)
        {
            this.BackColor = prevForm.BackColor;
            this.ForeColor = prevForm.ForeColor;

            TextBox tb = null;
            Button b = null;
            foreach (Control tmp in prevForm.Controls)
            {
                if (tb == null)
                    tb = tmp as TextBox;
                if (b == null)
                    b = tmp as Button;
                if (tb != null && b != null)
                    break;
            }
            if (tb != null)
            {
                this.mLVLFileTextBox.BackColor = this.mAssetListBox.BackColor =  tb.BackColor;
                this.mLVLFileTextBox.ForeColor = this.mAssetListBox.ForeColor =  tb.ForeColor;
            }
            if (b != null)
            {
                mMergeButton.BackColor = mBrowseLVL.BackColor = b.BackColor;
                mMergeButton.ForeColor = mBrowseLVL.ForeColor = b.ForeColor;
            }
        }
    }
}
