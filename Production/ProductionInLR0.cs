using System.Text;

namespace SyntaxAnalysis;
class ProductionInLR0 : Production
{
    protected int pointPos = 0;

    public void pointForward() { pointPos++; }
    public int PointPos { get { return pointPos; } }

    public override bool Equals(object obj)
    {
        if (this == obj) return true;
        if (obj == null) { return false; }
        if (obj is not ProductionInLR0) { return false; }
        ProductionInLR0 production = obj as ProductionInLR0;
        return base.Equals(production) && production.PointPos == pointPos;
    }

    public override int GetHashCode() { return base.GetHashCode() + pointPos; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("{0} --> ", key);
        sb.Append(string.Join(' ', values.GetRange(0, pointPos)));
        sb.AppendFormat(" {0} ", PublicFunc.POINTSYMBOL);
        sb.Append(String.Join(' ', values.GetRange(pointPos, values.Count - pointPos)));
        return sb.ToString();
    }

    public ProductionInLR0(Production production) : base(production.Key, production.Values)
    {
        // 点符号前进至最后Epsilon后面
        for (int i = 0; i < values.Count && values[i] == PublicFunc.EPSILON; i++)
        {
            pointForward();
        }
    }

    public ProductionInLR0(ProductionInLR0 production) : base(production.Key, production.Values)
    {
        this.pointPos = production.PointPos;
    }

    public string NextTokenOfPoint
    {
        get
        {
            if (pointPos != values.Count)
            {
                return values[pointPos];
            }
            return null;
        }
    }
}