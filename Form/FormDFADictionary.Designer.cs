
namespace SyntaxAnalysis
{
    partial class FormDFADictionary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DFADictionaryTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DFADictionaryTextBox
            // 
            this.DFADictionaryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DFADictionaryTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DFADictionaryTextBox.Location = new System.Drawing.Point(12, 11);
            this.DFADictionaryTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DFADictionaryTextBox.Multiline = true;
            this.DFADictionaryTextBox.Name = "DFADictionaryTextBox";
            this.DFADictionaryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DFADictionaryTextBox.Size = new System.Drawing.Size(457, 446);
            this.DFADictionaryTextBox.TabIndex = 0;
            this.DFADictionaryTextBox.TabStop = false;
            this.DFADictionaryTextBox.WordWrap = false;
            // 
            // FormDFADictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 468);
            this.Controls.Add(this.DFADictionaryTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "FormDFADictionary";
            this.Text = "DFA集族";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FromDFADictionary_close);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DFADictionaryTextBox;
    }
}