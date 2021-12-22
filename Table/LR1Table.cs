namespace SyntaxAnalysis
{
    class LR1Table : LRTable
    {
        public LR1Table(DFAGraphBase dFAGraph, InputGrammer inputGrammer) : base(dFAGraph, inputGrammer) 
        {
            generateLRTable();
        }
        protected override void setAcceptOrReduce(string row, ProductionInLR0 production)
        {
            if (production.Key == inputGrammer.nonTerminalTokens[0])
            {
                table.Add(new Item(row, PublicFunc.ENDSYMBOL, PublicFunc.ACCOMPLISH));
            }
            else
            {
                ProductionInLR1 newProduction = production as ProductionInLR1;
                foreach (string token in newProduction.searchTokens)
                {
                    Item item = new Item(row, token, "r" + Convert.ToString(inputGrammer.userProductions.IndexOf(newProduction)));
                    checkConflict(item);
                    if (!table.Contains(item))
                    {
                        table.Add(item);
                    }
                }
            }
        }
    }
}