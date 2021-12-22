using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SyntaxAnalysis
{

    class FailToAnalyzeException : Exception { public FailToAnalyzeException(string msg) : base(msg) { } }

    abstract class Analyze
    {
        public List<string> statusProcess = new List<string>();
        public List<string> tokenProcess = new List<string>();
        public List<string> actionProcess = new List<string>();
        public List<string> inputProcess = new List<string>();

        protected InputGrammer inputGrammer;
        protected List<string> inputTokens;
        protected Table table;
        public Analyze(InputGrammer inputGrammer, Table table, string text)
        {
            this.inputGrammer = inputGrammer;
            // terminalTokens去除Epsilon、按字符串长度从大到小排序
            inputTokens = flatText(text, inputGrammer.terminalTokens.Where(i => i != PublicFunc.EPSILON).OrderBy(i => -i.Length).ToList());
            if (inputTokens.Count == 0)
            {
                throw new FailToAnalyzeException("无法识别任何符号");
            }
            this.table = table;
        }


        // 使用LL或LR分析技术
        abstract public void startAnalyzing();
        
        // 从用户输入的句子分离成一个个的终结符号
        protected List<string> flatText(string text, List<string> terminalTokens)
        {
            List<string> generatedList = new List<string>();
            // 进行去空格、多空格转一空格等处理
            List<string> inputList = new List<string>(Regex.Replace(text.Trim(), @"\s+", " ").Split(' '));
            foreach (string token in inputList)
            {
                int pos = 0;
                for (int i = 0; i < terminalTokens.Count; i++)
                {
                    int nextPos = token.IndexOf(terminalTokens[i], pos);
                    if (nextPos != -1 && nextPos == pos)
                    {
                        generatedList.Add(terminalTokens[i]);
                        pos += terminalTokens[i].Length;
                        //未结束就再次重新查找
                        if (pos != token.Length)
                        {
                            i = -1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                //没有遍历完就结束了，即含有无法识别的终结符号
                if (pos != token.Length)
                {
                    throw new FailToAnalyzeException(string.Format("含有无法识别的终结符号{0}", token.Substring(pos)));
                }
            }
            return generatedList;
        }

        protected string getStrFromStack(Stack<string> inputStack, bool reverse = true)
        {
            StringBuilder sb = new StringBuilder();
            string[] tokens = inputStack.ToArray();
            if (reverse)
            {
                for (int i = tokens.Length - 1; i >= 0; i--)
                {
                    sb.AppendFormat("{0} ", tokens[i]);
                }
            }
            else
            {
                for (int i = 0; i < tokens.Length; i++)
                {
                    sb.AppendFormat("{0} ", tokens[i]);
                }
            }
            return sb.ToString();
        }

        protected string getItemData(string row, string col)
        {
            string itemData = table[new Item(row, col)];
            if (itemData == null)
            {
                throw new FailToAnalyzeException(string.Format("查表 [{0},{1}] 时发生错误", row, col));
            }
            return itemData;
        }


    }
}
