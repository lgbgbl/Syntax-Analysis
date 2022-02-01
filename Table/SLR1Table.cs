namespace SyntaxAnalysis;
class SLR1Table : LRTable
{
    protected GenerateFollow generatedFollow;

    public SLR1Table(DFAGraphBase DFAGraph, InputGrammer inputGrammer) : base(DFAGraph, inputGrammer)
    {
        GenerateFirst generatedFirst = new GenerateFirst(inputGrammer);
        generatedFollow = new GenerateFollow(inputGrammer, generatedFirst);
        generateLRTable();
    }

    protected override void setAcceptOrReduce(string row, ProductionInLR0 production)
    {
        if (production.Key == inputGrammer.nonTerminalTokens[0])
        {
            table.Add(new Item(row, PublicFunc.ENDSYMBOL, PublicFunc.ACCOMPLISH));
            return;
        }

        foreach (string token in generatedFollow[production.Key])
        {
            Item item = new Item(row, token, "r" + Convert.ToString(inputGrammer.userProductions.IndexOf(production)));
            checkConflict(item);
            if (!table.Contains(item))
            {
                table.Add(item);
            }
        }
    }
}