namespace SyntaxAnalysis;

abstract class LRTable : Table
{
    protected DFAGraphBase dFAGraph;

    public LRTable(DFAGraphBase dFAGraph, InputGrammer inputGrammer) : base(inputGrammer)
    {
        this.dFAGraph = dFAGraph;
    }
    protected void generateLRTable()
    {
        foreach (DFANode node in dFAGraph.GetDFANodes)
        {
            string ID = node.ID;
            foreach (Degree degree in node.degrees)
            {
                string dstID = degree.degreeOut;

                string translation = degree.translation;
                List<ProductionInLR0> productions = node.productions;
                foreach (ProductionInLR0 production in productions)
                {
                    string token = production.NextTokenOfPoint;
                    // 接收或者规约
                    if (token == null)
                    {
                        setAcceptOrReduce(ID, production);
                    }
                    // 移入或goto
                    else if (token == translation)
                    {
                        setShiftOrGoto(ID, token, dstID);
                    }
                }
            }
            //该项集点符号都在最后面
            if (node.degrees.Count == 0)
            {
                List<ProductionInLR0> productions = node.productions;
                foreach (ProductionInLR0 production in productions)
                {
                    setAcceptOrReduce(ID, production);
                }
            }
        }
    }

    abstract protected void setAcceptOrReduce(string row, ProductionInLR0 production);

    protected void setShiftOrGoto(string row, string col, string data)
    {
        Item item = inputGrammer.nonTerminalTokens.Contains(col) ?
            new Item(row, col, data) :
            new Item(row, col, "s" + data);
        checkConflict(item);
        if (!table.Contains(item))
        {
            table.Add(item);
        }
    }


    protected override void checkConflict(Item newItem)
    {
        foreach (Item item in table)
        {
            if (item.conflictWith(newItem))
            {
                throw new ConflictException("分析表具有二义性", true, item.data, newItem);
            }
        }
    }
}