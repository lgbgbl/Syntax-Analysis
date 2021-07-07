using System;
using System.Collections.Generic;

namespace SyntaxAnalysis
{

    class LRAnalyze : Analyze
    {
        public LRAnalyze(InputGrammer inputGrammer, Table table, string text) : base(inputGrammer, table, text) { }

        public override void startAnalyzing()
        {
            Stack<string> inputStack = new Stack<string>();
            Stack<string> tokenStack = new Stack<string>();
            Stack<string> statusStack = new Stack<string>();
            statusStack.Push("0");
            inputStack.Push(PublicFunc.ENDSYMBOL);
            for (int i = inputTokens.Count - 1; i >= 0; i--)
            {
                inputStack.Push(inputTokens[i]);
            }
            for (; ; )
            {
                statusProcess.Add(getStrFromStack(statusStack));
                tokenProcess.Add(getStrFromStack(tokenStack));
                inputProcess.Add(getStrFromStack(inputStack, false));

                string itemData = getItemData(statusStack.Peek(), inputStack.Peek());

                if (itemData == PublicFunc.ACCOMPLISH)
                {
                    actionProcess.Add("接受");
                    break;
                }
                else if (itemData[0] == 's')
                {
                    statusStack.Push(itemData.Substring(1));
                    tokenStack.Push(inputStack.Peek());
                    actionProcess.Add(string.Format("移入 {0}", inputStack.Peek()));
                    inputStack.Pop();
                }
                else if (itemData[0] == 'r')
                {
                    Production production = inputGrammer.userProductions[Convert.ToInt32(itemData.Substring(1))];
                    List<string> values = production.Values;
                    string key = production.Key;
                    for (int i = values.Count - 1; i >= 0; i--)
                    {
                        if (values[i] != tokenStack.Peek())
                        {
                            break;
                        }
                        tokenStack.Pop();
                        statusStack.Pop();
                    }
                    tokenStack.Push(key);
                    statusStack.Push(getItemData(statusStack.Peek(), tokenStack.Peek()));
                    actionProcess.Add(string.Format("根据 {0} 规约", production.ToString()));
                }

            }

        }
    }
}
