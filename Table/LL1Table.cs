namespace SyntaxAnalysis;
class FailToGenerateLL1Exception : Exception { public FailToGenerateLL1Exception(string msg) : base(msg) { } }

class LL1Table : Table
{
    GenerateFirst generatedFirst;

    // 检查左递归或左公因子
    public bool checkLeft()
    {
        foreach (string token in inputGrammer.nonTerminalTokens)
        {
            // S->A | B 求First(A)与First(B)的交集是否存在
            List<Production> productions = inputGrammer[token];
            for (int i = 0; i < productions.Count; i++)
            {
                for (int j = i + 1; j < productions.Count; j++)
                {
                    if (generatedFirst.getFirstFromPart(productions[i].Values).Intersect(generatedFirst.getFirstFromPart(productions[j].Values)).Count() > 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public LL1Table(InputGrammer inputGrammer) : base(inputGrammer)
    {
        generatedFirst = new GenerateFirst(inputGrammer);
        GenerateFollow generatedFollow = new GenerateFollow(inputGrammer, generatedFirst);
        if (!checkLeft())
        {
            throw new FailToGenerateLL1Exception("含有左递归或左公因子");
        }
        foreach (Production production in inputGrammer.userProductions)
        {
            List<string> firstSetOfRightPart = generatedFirst.getFirstFromPart(production.Values);
            foreach (string token in firstSetOfRightPart.Where(i => (!inputGrammer.nonTerminalTokens.Contains(i) && i != PublicFunc.EPSILON)))
            {
                Item item = new Item(production.Key, token, production.ToString());
                checkConflict(item);
                table.Add(item);
            }
            if (firstSetOfRightPart.Contains(PublicFunc.EPSILON))
            {
                foreach (string token in generatedFollow[production.Key].Where(i => !inputGrammer.nonTerminalTokens.Contains(i)))
                {
                    Item item = new Item(production.Key, token, production.ToString());
                    checkConflict(item);
                    table.Add(item);

                }
            }
        }
    }

    protected override void checkConflict(Item newItem)
    {
        foreach (Item item in table)
        {
            if (item.conflictWith(newItem))
            {
                throw new ConflictException("分析表具有二义性", false, item.data, newItem);
            }
        }
    }
}