using System.Text;
using System.Text.RegularExpressions;

namespace SyntaxAnalysis;
public class Production
{
    protected string key;
    protected List<string> values;
    public string Key { get { return key; } }
    public List<string> Values { get { return values; } }

    private static readonly Regex productionReg = new Regex(@"(.*?)\s*\-+>\s*(.*)\s*$");

    public Production(string key, List<string> values)
    {
        this.key = key;
        this.values = values;
    }

    public Production(string productionStr)
    {
        MatchCollection mc = productionReg.Matches(productionStr);
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
        if (this == obj) return true;
        if (obj == null) { return false; }
        if (obj is not Production) { return false; }

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
        sb.Append(string.Join(' ', values));
        return sb.ToString();
    }
}
