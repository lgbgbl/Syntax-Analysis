using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
namespace SyntaxAnalysis
{
    class ExpandException : Exception { public ExpandException(string msg) : base(msg) { } }


    class Degree
    {
        public string degreeOut;
        public string translation;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Degree degree = obj as Degree;
            return degreeOut.Equals(degree.degreeOut) && translation.Equals(degree.translation);
        }

        public override int GetHashCode() { return degreeOut.GetHashCode() + translation.GetHashCode(); }

        public Degree(string degreeOut, string translation)
        {
            this.degreeOut = degreeOut;
            this.translation = translation;
        }
    }


    class DFANode
    {
        public string ID;
        public List<ProductionInLR0> productions;

        public DFANode(List<ProductionInLR0> productions, string ID)
        {
            this.ID = ID;
            this.productions = productions;
        }

        // 入度和出度
        public List<Degree> degrees = new List<Degree>();
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("State {0}:\r\n", ID);
            foreach (ProductionInLR0 production in productions)
                sb.AppendFormat("{0}\r\n", production.ToString());
            return sb.ToString();
        }
    }


    abstract class DFAGraphBase
    {
        protected InputGrammer inputGrammer;

        protected List<DFANode> DFANodes = new List<DFANode>();

        public List<DFANode> GetDFANodes { get { return DFANodes; } }
        public DFAGraphBase(InputGrammer inputGrammer)
        {
            this.inputGrammer = inputGrammer;
            if (!checkGrammer())
                throw new ExpandException("文法需要扩充为增广文法");
        }
        private bool checkGrammer()
        {
            return inputGrammer[inputGrammer.userProductions[0].Key].Count == 1;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DFANode node in DFANodes)
                sb.AppendFormat("{0}\r\n", node.ToString());
            return sb.ToString();
        }
        protected List<string> getNextTokens(List<ProductionInLR0> productions)
        {
            List<string> generatedList = new List<string>();
            foreach (ProductionInLR0 production in productions)
            {
                string nextToken = production.NextTokenOfPoint;
                if (nextToken != null && nextToken != PublicFunc.EPSILON && !generatedList.Contains(nextToken))
                    generatedList.Add(nextToken);
            }
            return generatedList;
        }

        protected string getID(List<ProductionInLR0> productions)
        {
            foreach (DFANode DFANode in DFANodes.Where(i => i.productions.Count == productions.Count))
            {
                // 差集个数为0，即两个List内容相同
                if (DFANode.productions.Except(productions).Count() == 0 && productions.Except(DFANode.productions).Count() == 0)
                    return DFANode.ID;
            }
            return null;
        }
        abstract protected List<ProductionInLR0> closure(List<ProductionInLR0> productions);
        protected List<ProductionInLR0> goTo<T>(List<ProductionInLR0> productions, string translation) where T : ProductionInLR0
        {
            List<ProductionInLR0> generatedList = new List<ProductionInLR0>();
            foreach (ProductionInLR0 production in productions)
            {
                string nextToken = production.NextTokenOfPoint;
                if (nextToken != null && nextToken == translation)
                {
                    ProductionInLR0 newProduction = Activator.CreateInstance(typeof(T), new object[] { production }) as T;
                    // 点符号前进
                    newProduction.pointForward();
                    generatedList.Add(newProduction);
                }
            }
            return closure(generatedList);
        }
        protected void generateDFAGraph<T>(List<DFANode> DFANodes) where T : ProductionInLR0
        {
            for (int i = 0; i < DFANodes.Count; i++)
            {
                DFANode dFANode = DFANodes[i];

                List<string> nextTokens = getNextTokens(dFANode.productions);
                foreach (string token in nextTokens)
                {
                    List<ProductionInLR0> newProductionsOfDFANode = goTo<T>(dFANode.productions, token);
                    string dstID = getID(newProductionsOfDFANode);
                    // 该项集从未出现
                    if (dstID == null)
                    {
                        dstID = Convert.ToString(DFANodes.Count);
                        DFANodes.Add(new DFANode(newProductionsOfDFANode, dstID));
                    }

                    // 更新边信息
                    dFANode.degrees.Add(new Degree(dstID, token));
                }
            }
        }

    }

}