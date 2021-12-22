namespace SyntaxAnalysis
{
    class LLAnalyze : Analyze
    {
        public LLAnalyze(InputGrammer inputGrammer, Table table, string text) : base(inputGrammer, table, text) { }

        public override void startAnalyzing()
        {
            Stack<string> inputStack = new Stack<string>();
            Stack<string> tokenStack = new Stack<string>();
            inputStack.Push(PublicFunc.ENDSYMBOL);
            tokenStack.Push(PublicFunc.ENDSYMBOL);
            tokenStack.Push(inputGrammer.userProductions[0].Key);
            for (int i = inputTokens.Count - 1; i >= 0; i--)
            {
                inputStack.Push(inputTokens[i]);
            }
            for (; ; )
            {
                tokenProcess.Add(getStrFromStack(tokenStack));
                inputProcess.Add(getStrFromStack(inputStack, false));
                if (tokenStack.Peek().Equals(inputStack.Peek()))
                {
                    if (tokenStack.Peek().Equals(PublicFunc.ENDSYMBOL))
                    {
                        actionProcess.Add("接受");
                        break;
                    }
                    tokenStack.Pop();
                    inputStack.Pop();
                    actionProcess.Add("匹配");
                    continue;
                }
                string itemData = getItemData(tokenStack.Peek(), inputStack.Peek());

                Production production = new Production(itemData);
                List<string> values = production.Values;
                tokenStack.Pop();
                for (int i = values.Count - 1; i >= 0; i--)
                {
                    if(values[i]!=PublicFunc.EPSILON)
                    {
                        tokenStack.Push(values[i]);
                    }
                }
                actionProcess.Add(production.ToString());
            }
        }
    }
}
