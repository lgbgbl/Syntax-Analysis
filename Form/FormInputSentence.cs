using System;
using System.Windows.Forms;

namespace SyntaxAnalysis
{
    public partial class FormInputSentence : Form
    {
        private InputGrammer inputGrammer;
        private Table table;
        public FormInputSentence()
        {
            InitializeComponent();
        }

        public void getTable(Table table, InputGrammer inputGrammer)
        {
            this.table = table;
            this.inputGrammer = inputGrammer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();
            Analyze analyze = null;
            string[] headers = null;
            try
            {

                if (table is LL1Table)
                {
                    analyze = new LLAnalyze(inputGrammer, table, inputArea.Text);
                    headers = new string[4] { "步骤", "符号栈", "输入", "动作" };
                }
                else
                {
                    analyze = new LRAnalyze(inputGrammer, table, inputArea.Text);
                    headers = new string[5] { "步骤", "状态栈", "符号栈", "输入", "动作" };
                }

                analyze.startAnalyzing();
            }
            catch (FailToAnalyzeException ee)
            {
                MessageBox.Show(ee.Message, ee.Message);
                return;
            }

            for (int ii = 0; ii < headers.Length; ii++)
            {
                dataGridView.Columns.Add(headers[ii], null);
                dataGridView.Columns[ii].SortMode = DataGridViewColumnSortMode.NotSortable;
                // "输入"一列 向右对齐
                if (ii == headers.Length - 2)
                {
                    dataGridView.Columns[ii].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            if (table is LL1Table)
            {
                for (int i = 0; i < analyze.tokenProcess.Count; i++)
                {
                    dataGridView.Rows.Add(Convert.ToString(i + 1), analyze.tokenProcess[i], analyze.inputProcess[i], analyze.actionProcess[i]);
                }
            }
            else
            {
                for (int i = 0; i < analyze.tokenProcess.Count; i++)
                {
                    dataGridView.Rows.Add(Convert.ToString(i + 1), analyze.statusProcess[i], analyze.tokenProcess[i], analyze.inputProcess[i], analyze.actionProcess[i]);
                }
            }

        }
    }
}
