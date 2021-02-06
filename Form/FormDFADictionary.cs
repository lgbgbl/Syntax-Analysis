using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyntaxAnalysis
{
    public partial class FormDFADictionary : Form
    {
        public FormDFADictionary() { InitializeComponent(); }
        public void getTextData(string text) { DFADictionaryTextBox.Text = text; }

        private void FromDFADictionary_close(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
