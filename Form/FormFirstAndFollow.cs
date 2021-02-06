using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyntaxAnalysis
{
    public partial class FormFirstAndFollow : Form
    {
        public FormFirstAndFollow()
        {
            InitializeComponent();
        }
        public void getTextData(object obj)
        {
            if (obj is GenerateFirst)
                FirstSetArea.Text = obj.ToString();
            else if (obj is GenerateFollow)
                FollowSetArea.Text = obj.ToString();
        }
        private void FromFirstAndFollow_close(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

    }
}
