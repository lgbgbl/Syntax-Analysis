namespace SyntaxAnalysis;
class PublicFunc
{
    public static string EPSILON = "~";
    public static string ENDSYMBOL = "$";
    public static string POINTSYMBOL = ".";
    public static string ACCOMPLISH = "acc";


    protected bool ExtendElement(List<string> src, List<string> dst)
    {
        bool hasChange = false;
        foreach (string token in src)
        {
            if (!dst.Contains(token))
            {
                dst.Add(token);
                hasChange = true;
            }
        }
        return hasChange;
    }
    protected bool ExtendElementWithoutEpsilon(List<string> src, List<string> dst, ref bool hasEpsilon)
    {
        bool hasChange = false;
        foreach (string token in src)
        {
            if (token != EPSILON)
            {
                if (!dst.Contains(token))
                {
                    dst.Add(token);
                    hasChange = true;
                }
            }
            else
            {
                hasEpsilon = true;
            }
        }
        return hasChange;
    }

    protected List<string> GetValuesByKey(string key, List<Production> productions)
    {
        foreach (Production production in productions)
        {
            if (production.Key.Equals(key))
            {
                return production.Values;
            }
        }
        return null;
    }

}