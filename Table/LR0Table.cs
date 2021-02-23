using System.Collections.Generic;
using System.Linq;
using System;
namespace SyntaxAnalysis
{
    class LR0Table : LRTable
    {
        public LR0Table(DFAGraphBase dFAGraph, InputGrammer inputGrammer) : base(dFAGraph, inputGrammer)
        {
            generateLRTable();
        }
        protected override void setAcceptOrReduce(string row, ProductionInLR0 production)
        {
            bool hasStartSymbol = production.Key == inputGrammer.nonTerminalTokens[0];
            // 防止将acc覆盖
            if (hasStartSymbol)
                table.Add(new Item(row, PublicFunc.ENDSYMBOL, PublicFunc.ACCOMPLISH));
            else
                table.Add(new Item(row, PublicFunc.ENDSYMBOL, "r" + Convert.ToString(inputGrammer.userProductions.IndexOf(production))));
            foreach (string token in inputGrammer.terminalTokens.Where(i => i != PublicFunc.EPSILON))
            {
                Item item = new Item(row, token, "r" + Convert.ToString(inputGrammer.userProductions.IndexOf(production)));
                checkConflict(item);
                if (!table.Contains(item))
                    table.Add(item);
            }
        }
    }
}