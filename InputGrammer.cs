
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Text;
namespace SyntaxAnalysis
{
    class NoValidGrammerException : Exception { public NoValidGrammerException(string msg) : base(msg) { } }
    public class InputGrammer
    {
        public List<Production> userProductions = new List<Production>();
        private string[] lines;
        public List<string> terminalTokens = new List<string>();
        public List<string> nonTerminalTokens = new List<string>();

        public InputGrammer(string textFromInputArea)
        {
            string resultFromUser = textFromInputArea;
            lines = textFromInputArea.Split('\n');
            dealWithLines();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < userProductions.Count; i++)
            {
                sb.AppendFormat("{0}:\t{1}\r\n", i, userProductions[i]);
            }
            return sb.ToString();
        }

        private void dealWithLines()
        {
            int total = 0;
            foreach (string line in lines)
            {
                MatchCollection mc = Regex.Matches(line, @"\s*(.*?)\s*\-+>\s*(.*)\s*$");
                // 若成功匹配
                if (mc.Count > 0)
                {
                    Match m = mc[0];
                    string leftPart = m.Groups[1].ToString();
                    string[] rightParts = m.Groups[2].ToString().Split('|');
                    for (int i = 0; i < rightParts.Length; i++)
                    {
                        // 进行去空格、多空格转一空格等处理
                        List<string> rightPart = new List<string>(Regex.Replace(rightParts[i].Trim(), @"\s+", " ").Split(' '));
                        userProductions.Add(new Production(leftPart, rightPart));
                    }

                    // 保存非终结符号
                    if (!nonTerminalTokens.Contains(leftPart))
                        nonTerminalTokens.Add(leftPart);
                }
                else
                    total++;
            }
            // 全都无法匹配
            if (total == lines.Length)
                throw new NoValidGrammerException("无法找到有效文法");

            // 保存终结符号
            foreach (Production production in userProductions)
                foreach (string value in production.Values.Where(i => (!nonTerminalTokens.Contains(i) && !terminalTokens.Contains(i))))
                    terminalTokens.Add(value);

        }

        public List<Production> this[string key]
        {
            get
            {
                List<Production> generatedList = new List<Production>();
                foreach (Production production in userProductions.Where(i => i.Key == key))
                    generatedList.Add(production);
                return generatedList;
            }
        }
    }
}