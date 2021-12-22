using System.Data;
using System.Reflection;

namespace SyntaxAnalysis
{
    public partial class FormSyntaxAnalysis : Form
    {

        FormFirstAndFollow formFirstAndFollow;
        FormDFADictionary formDFADictionary;
        FormInputSentence formInputSentence;
        InputGrammer inputGrammer = null;
        DFAGraphBase dFAGraph = null;
        LRTable lRTable = null;
        LL1Table ll1table = null;
        public FormSyntaxAnalysis()
        {
            InitializeComponent();
        }

        private void generateLRDFAInGrid<T>() where T : DFAGraphBase
        {
            string kind = typeof(T).Name;
            kind = kind.Substring(kind.IndexOf("From") + 4);
            try
            {
                try
                {
                    inputGrammer = new InputGrammer(InputArea.Text);
                    dFAGraph = Activator.CreateInstance(typeof(T), new object[] { inputGrammer }) as T;
                }
                catch (TargetInvocationException ee)
                {
                    throw ee.GetBaseException();
                }
                dataGridView.Columns.Clear();
                dataGridView.Rows.Clear();
                List<DFANode> dFANodes = dFAGraph.GetDFANodes;
                List<DFANode> srcs = dFANodes.Where(i => i.degrees.Count != 0).ToList();
                List<string> dsts = dFANodes.Select(i => i.ID).ToList();


                List<string> cols = new List<string> { @"起点\终点" };
                cols.AddRange(dsts);

                // 列太多导致FillWeight过多时 ，winform DataGridView会爆掉
                int weight = 65535 / (cols.Count + 1);
                for (int i = 0; i < cols.Count; i++)
                {
                    dataGridView.Columns.Add(cols[i], null);
                    // 禁用排序
                    dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView.Columns[i].FillWeight = weight;
                }

                // 行序号从小到大
                List<string> rows = srcs.Select(i => i.ID).Distinct().OrderBy(i => Convert.ToInt32(i)).ToList();
                foreach (string row in rows)
                {
                    dataGridView.Rows.Add(row);
                }

                foreach (DFANode src in srcs)
                {
                    int rowID = rows.IndexOf(src.ID);
                    foreach (var degree in src.degrees)
                    {
                        dataGridView.Rows[rowID].Cells[cols.IndexOf(degree.degreeOut)].Value = degree.translation;
                    }
                }

                if (formDFADictionary == null)
                {
                    formDFADictionary = new FormDFADictionary();
                }
                formDFADictionary.getTextData(dFAGraph.ToString());
                formDFADictionary.Show();


                labelOfTable.Text = kind + " DFA";
            }
            catch (NoValidGrammerException ee) { MessageBox.Show(ee.Message, ee.Message); }
            catch (ExpandException ee) { MessageBox.Show(ee.Message, "生成" + kind + " DFA失败"); }
        }
        private bool generateLLTableInGrid()
        {
            try
            {
                inputGrammer = new InputGrammer(InputArea.Text);
                ll1table = new LL1Table(inputGrammer);
                List<Item> tableData = ll1table.TableData;

                List<string> cols = new List<string> { "M(N,T)" };
                cols.AddRange(tableData.Select(i => i.col).Distinct().ToList());
                List<string> rows = tableData.Select(i => i.row).Distinct().ToList();

                dataGridView.Columns.Clear();
                dataGridView.Rows.Clear();
                int weight = 65535 / (cols.Count + 1);
                for (int i = 0; i < cols.Count; i++)
                {
                    dataGridView.Columns.Add(cols[i], null);
                    // 禁用自动排序
                    dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView.Columns[i].FillWeight = weight;
                }

                foreach (string row in rows)
                {
                    dataGridView.Rows.Add(row);
                }
                foreach (Item item in tableData)
                {
                    dataGridView.Rows[rows.IndexOf(item.row)].Cells[cols.IndexOf(item.col)].Value = item.data;
                }
            }
            catch (NoValidGrammerException ee)
            {
                MessageBox.Show(ee.Message, ee.Message);
                return false;
            }
            catch (FailToGenerateLL1Exception ee)
            {
                MessageBox.Show(ee.Message, "生成LL(1)分析表失败");
                return false;
            }
            catch (ConflictException ee)
            {
                MessageBox.Show(ee.Message, "生成LL(1)分析表失败");
                if (formDFADictionary == null)
                {
                    formDFADictionary = new FormDFADictionary();
                }
                formDFADictionary.getTextData(ee.reason);
                formDFADictionary.Show();
                return false;
            }
            return true;
        }

        private bool generateLRTableInGrid<T1, T2>() where T1 : DFAGraphBase where T2 : LRTable
        {
            string kindOfDFA = typeof(T1).Name;
            string kindOfTable = typeof(T2).Name;
            kindOfTable = kindOfTable.Substring(0, kindOfTable.IndexOf("Table"));
            try
            {
                try
                {
                    inputGrammer = new InputGrammer(InputArea.Text);
                    dFAGraph = Activator.CreateInstance(typeof(T1), new object[] { inputGrammer }) as T1;
                    lRTable = Activator.CreateInstance(typeof(T2), new object[] { dFAGraph, inputGrammer }) as T2;
                }
                catch (TargetInvocationException ee)
                {
                    throw ee.GetBaseException();
                }
                List<Item> tableData = lRTable.TableData;
                List<string> cols = new List<string> { "状态" };
                cols.AddRange(tableData.Select(i => i.col).Where(i => !inputGrammer.nonTerminalTokens.Contains(i)).Distinct().ToList());
                cols.AddRange(inputGrammer.nonTerminalTokens);
                List<string> rows = tableData.Select(i => i.row).Distinct().OrderBy(i => Convert.ToInt32(i)).ToList();

                dataGridView.Columns.Clear();
                dataGridView.Rows.Clear();


                int weight = 65535 / (cols.Count + 1);
                for (int i = 0; i < cols.Count; i++)
                {
                    dataGridView.Columns.Add(cols[i], null);
                    // 禁用自动排序
                    dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView.Columns[i].FillWeight = weight;
                }
                foreach (string row in rows)
                {
                    dataGridView.Rows.Add(row);
                }
                foreach (Item item in tableData)
                {
                    dataGridView.Rows[rows.IndexOf(item.row)].Cells[cols.IndexOf(item.col)].Value = item.data;
                }
                showTableMessage(getStrFromGrammerAndDFA(inputGrammer, dFAGraph));
                labelOfTable.Text = kindOfTable + "分析表";
            }

            catch (NoValidGrammerException ee)
            {
                MessageBox.Show(ee.Message, ee.Message);
                return false;
            }
            catch (ExpandException ee)
            {
                MessageBox.Show(ee.Message, "生成" + kindOfDFA + "失败");
                return false;
            }
            catch (ConflictException ee)
            {
                MessageBox.Show(ee.Message, "生成" + kindOfTable + "分析表失败");
                showTableMessage(ee.reason + getStrFromGrammerAndDFA(inputGrammer, dFAGraph));
                return false;
            }
            return true;
        }
        private void analyzeByLR<T1, T2>() where T1 : DFAGraphBase where T2 : LRTable
        {
            if (!generateLRTableInGrid<T1, T2>())
                return;
            formDFADictionary.Hide();
            formInputSentence = new FormInputSentence();
            formInputSentence.getTable(lRTable, inputGrammer);
            formInputSentence.Show();
        }
        private string getStrFromGrammerAndDFA(InputGrammer inputGrammer, DFAGraphBase dFAGraph)
        {
            return string.Format("\r\n产生式如下(编号从0开始)\r\n\r\n{0}\r\nDFA集族如下\r\n\r\n{1}", inputGrammer.ToString(), dFAGraph.ToString());
        }

        private void showTableMessage(string text)
        {
            if (formDFADictionary == null)
            {
                formDFADictionary = new FormDFADictionary();
            }
            formDFADictionary.getTextData(text);
            formDFADictionary.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InputGrammer inputGrammer = new InputGrammer(InputArea.Text);
                GenerateFirst generatedFirst = new GenerateFirst(inputGrammer);
                if (formFirstAndFollow == null)
                {
                    formFirstAndFollow = new FormFirstAndFollow();
                }
                formFirstAndFollow.getTextData(generatedFirst);
                formFirstAndFollow.Show();
            }
            catch (NoValidGrammerException ee) { MessageBox.Show(ee.Message, ee.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                InputGrammer inputGrammer = new InputGrammer(InputArea.Text);
                GenerateFirst generatedFirst = new GenerateFirst(inputGrammer);
                GenerateFollow generatedFollow = new GenerateFollow(inputGrammer, generatedFirst);
                if (formFirstAndFollow == null)
                {
                    formFirstAndFollow = new FormFirstAndFollow();
                }
                formFirstAndFollow.getTextData(generatedFollow);
                formFirstAndFollow.Show();
            }
            catch (NoValidGrammerException ee) { MessageBox.Show(ee.Message, ee.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            generateLLTableInGrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            generateLRDFAInGrid<DFAGraphFromLR0>();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            generateLRDFAInGrid<DFAGraphFromLR1>();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            generateLRDFAInGrid<DFAGraphFromLALR1>();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            generateLRTableInGrid<DFAGraphFromLR0, LR0Table>();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            generateLRTableInGrid<DFAGraphFromLR0, SLR1Table>();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            generateLRTableInGrid<DFAGraphFromLR1, LR1Table>();

        }
        private void button10_Click(object sender, EventArgs e)
        {
            generateLRTableInGrid<DFAGraphFromLALR1, LALR1Table>();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            analyzeByLR<DFAGraphFromLR0, LR0Table>();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            analyzeByLR<DFAGraphFromLR0, SLR1Table>();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            analyzeByLR<DFAGraphFromLR1, LR1Table>();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            analyzeByLR<DFAGraphFromLALR1, LALR1Table>();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!generateLLTableInGrid())
                return;
            formInputSentence = new FormInputSentence();
            formInputSentence.getTable(ll1table, inputGrammer);
            formInputSentence.Show();
        }
    }
}
