using System.Text;
namespace SyntaxAnalysis;
class ProductionInLR1 : ProductionInLR0
{
    public List<string> searchTokens;


    public ProductionInLR1(Production production, List<string> searchTokens) : base(production)
    {
        this.searchTokens = searchTokens;
    }

    public ProductionInLR1(ProductionInLR1 production) : base(production)
    {
        searchTokens = new List<string>();
        searchTokens.AddRange(production.searchTokens);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("{0} , ", base.ToString());
        sb.Append(string.Join('/', searchTokens));
        return sb.ToString();
    }

    public override bool Equals(object obj)
    {
        if (this == obj) return true;
        if (obj == null) return false;
        if (obj is not ProductionInLR1) return false;

        ProductionInLR1 production = obj as ProductionInLR1;
        return
            base.Equals(obj) &&
            searchTokens.Except(production.searchTokens).Count() == 0 &&
            production.searchTokens.Except(searchTokens).Count() == 0;
    }

    public override int GetHashCode() { return base.GetHashCode(); }

    // 构造LALR时检测是否同心
    public bool hasSameCoreWith(object obj) { return base.Equals(obj); }

}