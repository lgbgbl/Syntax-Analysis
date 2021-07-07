using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SyntaxAnalysis
{
    public class Production
    {
        protected string key;
        protected List<string> values;
        public string Key { get { return key; } }
        public List<string> Values { get { return values; } }

        public Production(string key, List<string> values)
        {
            this.key = key;
            this.values = values;
        }

        public Production(string productionStr)
        {
            MatchCollection mc = Regex.Matches(productionStr, @"(.*?)\s*\-+>\s*(.*)\s*$");
            // 
            if (mc.Count > 0)
            {
                Match m = mc[0];
                key = m.Groups[1].ToString();
                values = new List<string>();
                values.AddRange(m.Groups[2].ToString().Split(' '));
            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Production)) { return false; }

            Production production = obj as Production;
            if (!production.Key.Equals(Key)) { return false; }
            if (values.Count != production.Values.Count) { return false; }

            for (int i = 0; i < Values.Count; i++)
            {
                if (!values[i].Equals(production.Values[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode() { return Key.GetHashCode(); }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} --> ", key);
            for (int i = 0; i < values.Count; i++)
            {
                if (i != values.Count - 1)
                {
                    sb.AppendFormat("{0} ", values[i]);
                }
                else
                {
                    sb.Append(values[i]);
                }
            }
            return sb.ToString();
        }
    }

}