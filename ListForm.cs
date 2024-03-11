using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LVLTool
{
    public partial class ListForm : Form
    {
        public ListForm()
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

        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        public string PromptString
        {
            get { return labSelectPrompt.Text; }
            set { labSelectPrompt.Text = value; }
        }

        public string CurrentOption
        {
            get { return listOptions.SelectedItem.ToString(); }
            set
            {
                int index = listOptions.Items.IndexOf(value);
                if (index > -1)
                {
                    listOptions.SelectedIndex = index;
                }
                else
                {
                    Console.WriteLine("Warning: ListForm.SetCurrentOption: value not found: "+ value);
                }
            }
        }

        public List<string> Options
        {
            get
            {
                List<string> retVal = new List<string>(listOptions.Items.Count);
                foreach (object item in listOptions.Items)
                {
                    retVal.Add(item.ToString());
                }
                return retVal;
            }
            set
            {
                listOptions.Items.Clear();
                listOptions.Items.AddRange(value.ToArray());
            }
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
                this.listOptions.BackColor = tb.BackColor;
                this.listOptions.ForeColor = tb.ForeColor;
            }
            if (b != null)
            {
                btnCancel.BackColor = btnOk.BackColor = b.BackColor;
                btnCancel.ForeColor = btnOk.ForeColor = b.ForeColor;
            }
        }

        private void listOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
            }
        }


        private void listOptions_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Prompts the user for a selection
        /// </summary>
        /// <param name="title">The title of the dialog</param>
        /// <param name="caption">The dialog prompt</param>
        /// <param name="options">the list of options to choose from</param>
        /// <returns>Selected option or null if the user canceled</returns>
        public static string PromptUserForSelection(string title, string caption, List<string> options, string defaultOption)
        {
            return PromptUserForSelection(title, caption,  options,defaultOption, null);
        }

        /// <summary>
        /// Prompts the user for a selection
        /// </summary>
        /// <param name="title">The title of the dialog</param>
        /// <param name="caption">The dialog prompt</param>
        /// <param name="options">the list of options to choose from</param>
        /// <param name="f">the font to use for the form</param>
        /// <returns>Selected option or null if the user canceled</returns>
        public static string PromptUserForSelection(string title, string caption, List<string> options, string defaultOption, Form f)
        {
            string retVal = null;
            ListForm form = new ListForm();
            if(title != null)
                form.Text = title;
            if( caption != null)
                form.PromptString = caption;
            form.Options = options;
            if( defaultOption != null)
                form.CurrentOption = defaultOption;
            if (f != null)
            {
                form.SetStyle(f);
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                retVal = form.CurrentOption;
            }
            return retVal;
        }


    }
}
