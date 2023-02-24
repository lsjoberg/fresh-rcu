namespace FreshRcu.HeatPumpLogs;

public class HeatPumpLog
{
    // Power consumption per hour
    public DateTime LogStart => Rows.Min(row => row.Timestamp);
    public DateTime LogEnd => Rows.Max(row => row.Timestamp);

    public List<HeatPumpPeriod> GetInverterOnPeriods()
    {
        var periods = new List<HeatPumpPeriod>();
        var currentPeriod = new List<HeatPumpLogRow>();
        foreach (var row in Rows.OrderBy(row => row.Timestamp))
        {
            if (row.PowerConsumption > 0)
            {
                currentPeriod.Add(row);
            }

            if (row.PowerConsumption == 0 && currentPeriod.Any())
            {
                periods.Add(new HeatPumpPeriod(currentPeriod));
                currentPeriod.Clear();
            }
        }
        
        if (currentPeriod.Any())
        {
            periods.Add(new HeatPumpPeriod(currentPeriod));
        }

        return periods;
    }
    
    public List<HeatPumpPeriod> GetDailyPeriods()
    {
        var periods = new List<HeatPumpPeriod>();

        var firstRow = Rows.First();
        var currentDay = firstRow.Timestamp.Day;
        var rowsToSummarize = new List<HeatPumpLogRow> { firstRow };
        foreach (var row in Rows.OrderBy(row => row.Timestamp))
        {
            // If new day, sum and group
            if (row.Timestamp.Day != currentDay)
            {
                periods.Add(new HeatPumpPeriod(rowsToSummarize));
                rowsToSummarize.Clear();
                currentDay = row.Timestamp.Day;
            }
            
            rowsToSummarize.Add(row);
        }

        if (rowsToSummarize.Any())
        {
            periods.Add(new HeatPumpPeriod(rowsToSummarize));
        }

        return periods;
    }
    
    public IEnumerable<HeatPumpPeriod> GetHourlyPeriods()
    {
        var periods = new List<HeatPumpPeriod>();

        var firstRow = Rows.First();
        var currentHour = firstRow.Timestamp.Hour;
        var rowsToSummarize = new List<HeatPumpLogRow> { firstRow };
        foreach (var row in Rows.OrderBy(row => row.Timestamp))
        {
            // If new hour, sum and group
            if (row.Timestamp.Hour != currentHour)
            {
                periods.Add(new HeatPumpPeriod(rowsToSummarize));
                rowsToSummarize.Clear();
                currentHour = row.Timestamp.Hour;
            }
            
            rowsToSummarize.Add(row);
        }

        if (rowsToSummarize.Any())
        {
            periods.Add(new HeatPumpPeriod(rowsToSummarize));
        }

        return periods;
    }
    
    public List<HeatPumpLogRow> Rows { get; } = new();
}