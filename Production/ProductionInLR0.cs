
using System.Reflection.Emit;

using System.Collections.Generic;
using System.Text;

using System.Linq;
namespace SyntaxAnalysis
{
    class ProductionInLR0 : Production
    {
        protected int pointPos = 0;

        public void pointForward() { pointPos++; }
        public int PointPos { get { return pointPos; } }

        public override bool Equals(object obj)
        {
            ProductionInLR0 production = obj as ProductionInLR0;
            return production.PointPos == pointPos && base.Equals(production);
        }

        public override int GetHashCode() { return base.GetHashCode() + pointPos; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} --> ", key);
            for (int i = 0; i < pointPos; i++)
            { 
                if (i != pointPos - 1)
                    sb.AppendFormat("{0} ", values[i]);
                else
                    sb.Append(values[i]);
            }
            sb.AppendFormat(" {0} ", PublicFunc.POINTSYMBOL);
            for (int i = pointPos; i < values.Count; i++)
            { 
                if (i != values.Count - 1)
                    sb.AppendFormat("{0} ", values[i]);
                else
                    sb.Append(values[i]);
            }
            return sb.ToString();
        }

        public ProductionInLR0(Production production) : base(production.Key, production.Values)
        {
            // 点符号前进至最后Epsilon后面
            for (int i = 0; i < values.Count && values[i] == PublicFunc.EPSILON; i++)
                pointForward();
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
                    return values[pointPos];
                return null;
            }
        }
    }
}