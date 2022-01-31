namespace SyntaxAnalysis;
class ConflictException : Exception
{
    public string reason;
    public ConflictException(string msg, bool isLRTable, string oldData = null, Item item = null) : base(msg)
    {

        if (oldData != null && item.data != null && item != null)
        {
            reason = string.Format("失败原因： [{0},{1}] 项", item.row, item.col);
            if (isLRTable)
            {
                if (oldData[0] == 'r' && item.data[0] == 'r')
                {
                    reason += "发生了规约-规约冲突";
                }
                else
                {
                    reason += "发生了移入-规约冲突";
                }
            }
            reason += string.Format("\r\n原表项为{0}\r\n新表项为{1}\r\n\r\n", oldData, item.data);
        }
    }
}
public class Item
{
    public string row;
    public string col;
    public string data;

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj is not Item) return false;
        Item item = obj as Item;
        return item.row == row && item.col == col && item.data == data;
    }

    // 填入相同数据不相同时
    public bool conflictWith(Item item) { return item.row.Equals(row) && item.col.Equals(col) && !item.data.Equals(data); }

    public override int GetHashCode() { return row.GetHashCode() * col.GetHashCode() + data.GetHashCode(); }

    public Item(string row, string col, string data)
    {
        this.row = row;
        this.col = col;
        this.data = data;
    }

    // 用于查表
    public Item(string row, string col)
    {
        this.col = col;
        this.row = row;
    }
}
abstract public class Table
{
    protected List<Item> table = new List<Item>();
    protected InputGrammer inputGrammer;

    public Table(InputGrammer inputGrammer)
    {
        this.inputGrammer = inputGrammer;
    }
    public List<Item> TableData { get { return table; } }
    // 检查二义性
    abstract protected void checkConflict(Item newItem);

    public string this[Item inputItem]
    {
        get
        {
            foreach (Item item in table)
            {
                if (item.row.Equals(inputItem.row) && item.col.Equals(inputItem.col))
                {
                    return item.data;
                }
            }
            return null;
        }
    }
}
