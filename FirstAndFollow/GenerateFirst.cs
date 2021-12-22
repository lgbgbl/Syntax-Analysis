using System.Text;
namespace SyntaxAnalysis;

class GenerateFirst : PublicFunc
{
    private List<Production> firstSet = new List<Production>();

    private List<string> nonTerminalTokens;
    public GenerateFirst(InputGrammer inputGrammer)
    {
        nonTerminalTokens = inputGrammer.nonTerminalTokens;
        // 初始化first集合
        foreach (string token in inputGrammer.nonTerminalTokens)
        {
            firstSet.Add(new Production(token, new List<string>()));
        }
        int changeTotal;
        do
        {
            changeTotal = 0;
            foreach (Production production in inputGrammer.userProductions)
            {
                List<string> valuesInFirst = GetValuesByKey(production.Key, firstSet);
                int i;
                for (i = 0; i < production.Values.Count; i++)
                {
                    string token = production.Values[i];
                    if (!inputGrammer.nonTerminalTokens.Contains(token))
                    {
                        // 终结符号且不是Epsilon
                        if (token != EPSILON)
                        {
                            if (!valuesInFirst.Contains(token))
                            {
                                valuesInFirst.Add(token);
                                changeTotal++;
                            }
                            break;
                        }
                    }
                    else
                    {
                        bool hasEpsilon = false;
                        List<string> valuesOfTokenInFirst = GetValuesByKey(token, firstSet);
                        if (ExtendElementWithoutEpsilon(valuesOfTokenInFirst, valuesInFirst, ref hasEpsilon))
                        {
                            changeTotal++;
                        }
                        if (!hasEpsilon)
                        {
                            break;
                        }
                    }
                }
                if (i == production.Values.Count && !valuesInFirst.Contains(EPSILON))
                {
                    valuesInFirst.Add(EPSILON);
                    changeTotal++;
                }
            }

        } while (changeTotal != 0);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Production production in firstSet)
        {
            sb.AppendFormat("First({0}) =  ", production.Key);
            for (int i = 0; i < production.Values.Count; i++)
            {
                sb.AppendFormat("{0}  ", production.Values[i]);
            }
            sb.Append("\r\n\r\n");
        }
        return sb.ToString();
    }


    public List<string> getFirstFromPart(List<string> src, int pos, List<string> searchSymbols)
    {
        List<string> generatedList = new List<string>();
        generatedList.AddRange(src.Skip(pos).ToList());
        generatedList = getFirstFromPart(generatedList);
        if (generatedList.Count == 0 || generatedList.Contains(EPSILON))
        {
            // 去除Epsilon
            generatedList.Remove(EPSILON);
            generatedList.AddRange(searchSymbols.Where(i => !generatedList.Contains(i)));
        }
        return generatedList;
    }


    // 供求解Follow集合时调用
    public List<string> getFirstFromPart(List<string> src, int pos = 0)
    {
        List<string> generatedList = new List<string>();
        for (int i = pos; i < src.Count; i++)
        {
            bool hasEpsilon = false;
            // 非终结符号
            if (nonTerminalTokens.Contains(src[i]))
            {
                ExtendElementWithoutEpsilon(GetValuesByKey(src[i], firstSet), generatedList, ref hasEpsilon);
            }
            else
            {
                if (src[i] != EPSILON)
                {
                    generatedList.Add(src[i]);
                }
                else
                {
                    hasEpsilon = true;
                }
            }

            if (!hasEpsilon)
            {
                break;
            }
            else if (i == src.Count - 1)
            {
                generatedList.Add(EPSILON);
            }
        }
        return generatedList;
    }


    public List<string> this[string key] { get { return GetValuesByKey(key, firstSet); } }
}
