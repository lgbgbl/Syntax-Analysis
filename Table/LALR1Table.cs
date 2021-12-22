namespace SyntaxAnalysis;
class LALR1Table : LR1Table
{
    public LALR1Table(DFAGraphBase dFAGraph, InputGrammer inputGrammer) : base(dFAGraph, inputGrammer) { }
}