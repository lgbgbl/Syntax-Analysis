using System;
using System.Collections.Generic;
using System.Linq;
namespace SyntaxAnalysis
{
    class DFAGraphFromLR0 : DFAGraphBase
    {
        public DFAGraphFromLR0(InputGrammer inputGrammer) : base(inputGrammer)
        {
            DFANodes.Add(new DFANode(closure(new List<ProductionInLR0> { new ProductionInLR0(inputGrammer.userProductions[0]) }), Convert.ToString(DFANodes.Count)));
            generateDFAGraph<ProductionInLR0>(DFANodes);
        }

        protected override List<ProductionInLR0> closure(List<ProductionInLR0> productions)
        {
            List<ProductionInLR0> generatedList = new List<ProductionInLR0>();
            generatedList.AddRange(productions);
            for (int i = 0; i < generatedList.Count; i++)
            {
                List<string> nextTokens = getNextTokens(generatedList);
                foreach (string token in nextTokens.Where(ii => (inputGrammer.nonTerminalTokens.Contains(ii))))
                {
                    foreach (Production production in inputGrammer[token])
                    {
                        ProductionInLR0 newProduction = new ProductionInLR0(production);
                        if (!generatedList.Contains(newProduction))
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