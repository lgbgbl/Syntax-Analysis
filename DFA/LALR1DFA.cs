namespace SyntaxAnalysis;
class DFAGraphFromLALR1 : DFAGraphFromLR1
{

    public DFAGraphFromLALR1(InputGrammer inputGrammer) : base(inputGrammer)
    {
        mergeCore();
    }

    protected void mergeCore()
    {
        Dictionary<int, List<int>> relations = new Dictionary<int, List<int>>();
        HashSet<string> merged = new HashSet<string>();

        for (int i = 0; i < DFANodes.Count; i++)
        {
            for (int j = i + 1; j < DFANodes.Count; j++)
            {
                if (bothHasSameCore(DFANodes[i].productions, DFANodes[j].productions))
                {
                    // 合并搜索符
                    foreach (ProductionInLR1 productionOne in DFANodes[i].productions)
                    {
                        foreach (ProductionInLR1 productionTwo in DFANodes[j].productions)
                        {
                            // 同心 但搜索符不同
                            if (productionTwo.hasSameCoreWith(productionOne) && !productionTwo.Equals(productionOne))
                            {
                                // 搜索符取并集
                                productionOne.searchTokens = productionOne.searchTokens.Union(productionTwo.searchTokens).ToList();
                                break;
                            }
                        }
                    }
                    // 建立 被合并与合并关系
                    if (!relations.ContainsKey(i))
                    {
                        relations.Add(i, new List<int>() { j });
                    }
                    else
                    {
                        relations[i].Add(j);
                    }
                    // 添加即将删除的项集ID
                    merged.Add(DFANodes[j].ID);
                }
            }
        }
        foreach (var relation in relations)
        {
            // 更新边信息
            foreach (int value in relation.Value)
            {
                string valueStr = value.ToString();
                // 更新边信息
                foreach (DFANode node in DFANodes)
                {
                    foreach (Degree degree in node.degrees)
                    {
                        if (valueStr.Equals(degree.degreeOut))
                        {
                            degree.degreeOut = relation.Key.ToString();
                        }
                    }
                }
                // 新增被合并的项集的信息
                DFANodes[relation.Key].degrees = DFANodes[relation.Key].degrees.Union(DFANodes[value].degrees).ToList();
            }

        }
        // 移除被合并的项集
        DFANodes.RemoveAll(i => merged.Contains(i.ID));
        //更新ID，从0顺序排列
        for (int i = 0; i < DFANodes.Count; i++)
        {
            string oldID = DFANodes[i].ID;
            DFANodes[i].ID = Convert.ToString(i);
            foreach (DFANode node in DFANodes)
            {
                foreach (Degree degree in node.degrees)
                {
                    if (oldID.Equals(degree.degreeOut))
                    {
                        degree.degreeOut = DFANodes[i].ID;
                    }
                }
            }
        }
    }
    private bool bothHasSameCore(List<ProductionInLR0> productionsOne, List<ProductionInLR0> productionsTwo)
    {
        if (productionsOne.Count != productionsTwo.Count)
            return false;
        int total = 0;
        foreach (ProductionInLR1 productionOne in productionsOne)
        {
            foreach (ProductionInLR1 productionTwo in productionsTwo)
            {
                if (productionTwo.hasSameCoreWith(productionOne))
                {
                    total++;
                    break;
                }
            }
        }
        return total == productionsOne.Count;
    }
}