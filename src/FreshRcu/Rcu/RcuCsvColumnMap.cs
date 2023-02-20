namespace FreshRcu.Rcu;

internal class RcuCsvColumnMap
{
    private readonly Dictionary<string, List<int>> _columnMap;

    public int GetColumnIndex(string menuItem, int? index = null)
    {
        var columns = _columnMap[menuItem];
        return columns[index ?? 0];
    }

    public RcuCsvColumnMap(string headerRow)
    {
        var columns = headerRow.Split(RcuParser.Separator);

        var map = new Dictionary<string, List<int>>();

        // First two columns is time and date
        for (int colIndex = 2; colIndex < columns.Length; colIndex++)
        {
            var column = columns[colIndex];
            var menuItem = column.Split(':')[0].Trim();
            if (!map.ContainsKey(menuItem))
            {
                map.Add(menuItem, new List<int>());
            }

            map[menuItem].Add(colIndex);
        }

        _columnMap = map;
    }
}