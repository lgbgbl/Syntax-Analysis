using System.Collections.Generic;
using System.Linq;
namespace SyntaxAnalysis
{
    class DFAGraphFromLR1 : DFAGraphBase
    {
        protected GenerateFirst generatedFirst;
        
        public DFAGraphFromLR1(InputGrammer inputGrammer) : base(inputGrammer)
        {
            generatedFirst = new GenerateFirst(inputGrammer);
            DFANodes.Add(new DFANode(closure(new List<ProductionInLR0> { new ProductionInLR1(inputGrammer.userProductions[0], new List<string> { PublicFunc.ENDSYMBOL }) }), DFANodes.Count.ToString()));
            generateDFAGraph<ProductionInLR1>(DFANodes);
        }

        protected override List<ProductionInLR0> closure(List<ProductionInLR0> productions)
        {
            List<ProductionInLR0> generatedList = new List<ProductionInLR0>();
            generatedList.AddRange(productions);

            for (int i = 0; i < generatedList.Count; i++)
            {
                ProductionInLR1 oldProduction = generatedList[i] as ProductionInLR1;
                string token = generatedList[i].NextTokenOfPoint;
                if (token != null && inputGrammer.nonTerminalTokens.Contains(token))
                {
                    foreach (Production production in inputGrammer[token])
                    {
                        ProductionInLR1 newProduction = new ProductionInLR1(production, generatedFirst.getFirstFromPart(oldProduction.Values, oldProduction.PointPos + 1, oldProduction.searchTokens));
                        // 检查项集内是否有同心项（或者完全相同），有则不添加
                        bool noNeedToAdd = false;
                        foreach (ProductionInLR1 production1 in generatedList)
                        {
                            if (production1.hasSameCoreWith(newProduction))
                            {
                                noNeedToAdd = true;
                                production1.searchTokens = production1.searchTokens.Union(newProduction.searchTokens).ToList();
                            }
                        }
                        if (!noNeedToAdd)
                        {
                            generatedList.Add(newProduction);
                        }
                    }
                }
            }
            return generatedList;
        }

    }

}