using System.Text;
using System.Collections.Generic;
namespace SyntaxAnalysis
{
    class GenerateFollow : PublicFunc
    {
        private List<Production> followSet = new List<Production>();
        public GenerateFollow(InputGrammer inputGrammer, GenerateFirst generatedFirst)
        {
            foreach (string token in inputGrammer.nonTerminalTokens)
                followSet.Add(new Production(token, new List<string>()));
            // 加入美元符号
            GetValuesByKey(inputGrammer.nonTerminalTokens[0], followSet).Add(ENDSYMBOL);
            int changeTotal;
            do
            {
                changeTotal = 0;
                foreach (Production production in inputGrammer.userProductions)
                {
                    List<string> valuesInFollow = GetValuesByKey(production.Key, followSet);
                    for (int i = 0; i < production.Values.Count; i++)
                    {

                        string token = production.Values[i];
                        if (inputGrammer.nonTerminalTokens.Contains(token))
                        {
                            List<string> valuesOfTokenInFollow = GetValuesByKey(token, followSet);
                            if (i != production.Values.Count - 1)
                            {
                                bool hasEpsilon = false;
                                if (ExtendElementWithoutEpsilon(generatedFirst.getFirstFromPart(production.Values, i + 1), valuesOfTokenInFollow, ref hasEpsilon))
                                    changeTotal++;
                                if (hasEpsilon)
                                    if (ExtendElement(valuesInFollow, valuesOfTokenInFollow))
                                        changeTotal++;
                            }
                            else if (ExtendElement(valuesInFollow, valuesOfTokenInFollow))
                                changeTotal++;

                        }
                    }

                }
            } while (changeTotal != 0);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Production production in followSet)
            {
                sb.AppendFormat("Follow({0}) =  ", production.Key);
                for (int i = 0; i < production.Values.Count; i++)
                    sb.AppendFormat("{0}  ", production.Values[i]);
                sb.Append("\r\n\r\n");
            }
            return sb.ToString();
        }

        public List<string> this[string key] { get { return GetValuesByKey(key, followSet); } }
    }
}