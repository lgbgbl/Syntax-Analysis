namespace SyntaxAnalysis
{
    partial class FormFirstAndFollow
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
            this.FirstSetArea = new System.Windows.Forms.TextBox();
            this.FollowSetArea = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FirstSetArea
            // 
            this.FirstSetArea.AllowDrop = true;
            this.FirstSetArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FirstSetArea.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FirstSetArea.Location = new System.Drawing.Point(48, 72);
            this.FirstSetArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FirstSetArea.Multiline = true;
            this.FirstSetArea.Name = "FirstSetArea";
            this.FirstSetArea.ReadOnly = true;
            this.FirstSetArea.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.FirstSetArea.Size = new System.Drawing.Size(289, 435);
            this.FirstSetArea.TabIndex = 0;
            this.FirstSetArea.TabStop = false;
            this.FirstSetArea.WordWrap = false;
            // 
            // FollowSetArea
            // 
            this.FollowSetArea.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FollowSetArea.Location = new System.Drawing.Point(363, 72);
            this.FollowSetArea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FollowSetArea.Multiline = true;
            this.FollowSetArea.Name = "FollowSetArea";
            this.FollowSetArea.ReadOnly = true;
            this.FollowSetArea.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.FollowSetArea.Size = new System.Drawing.Size(288, 435);
            this.FollowSetArea.TabIndex = 0;
            this.FollowSetArea.TabStop = false;
            this.FollowSetArea.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "First集";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Follow集";
            // 
            // FormFirstAndFollow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 537);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FollowSetArea);
            this.Controls.Add(this.FirstSetArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "FormFirstAndFollow";
            this.Text = "First集&Follow集";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FromFirstAndFollow_close);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FirstSetArea;
        private System.Windows.Forms.TextBox FollowSetArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}