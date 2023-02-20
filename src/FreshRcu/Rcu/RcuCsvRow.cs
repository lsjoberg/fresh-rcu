namespace FreshRcu.Rcu;

internal class RcuCsvRow
{
    private readonly RcuCsvColumnMap _columnMap;
    private readonly string[] _columns;

    public DateTime GetTimestamp()
    {
        var timeString = $"{_columns[0]} {_columns[1]}";
        return DateTime.Parse(timeString);
    }

    private string GetString(string menuItem, int? index = null)
    {
        var columnIndex = _columnMap.GetColumnIndex(menuItem, index);
        return _columns[columnIndex];
    }
    
    public int GetInt(string menuItem, int? index = null)
        => int.Parse(GetString(menuItem, index));
        
    public decimal GetDecimal(string menuItem, int? index = null)
        => decimal.Parse(GetString(menuItem, index));
        
    public RcuCsvRow(RcuCsvColumnMap columnMap, string csvRow)
    {
        _columnMap = columnMap;
        _columns = csvRow.Split(RcuParser.Separator);
    }
}