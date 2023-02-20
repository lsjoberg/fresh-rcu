namespace FreshRcu.HeatPumpLogs;

public class HeatPumpLog
{
    // Power consumption per hour
    public DateTime LogStart => Rows.Min(row => row.Timestamp);
    public DateTime LogEnd => Rows.Max(row => row.Timestamp);
    
    // From, To, Power (kwh)
    public List<HeatPumpConsumption> GetConsumptionPerHour()
    {
        var consumption = new List<HeatPumpConsumption>();

        var currentHour = -1;
        var rowsToSummarize = new List<HeatPumpLogRow>();
        foreach (var row in Rows.OrderBy(row => row.Timestamp))
        {
            if (currentHour == -1)
            {
                rowsToSummarize.Add(row);
                currentHour = row.Timestamp.Hour;
                continue;
            }
            // If new hour, sum and group
            if (row.Timestamp.Hour != currentHour)
            {
                consumption.Add(new HeatPumpConsumption(rowsToSummarize));
                currentHour = row.Timestamp.Hour;
                rowsToSummarize = new();
            }
            
            rowsToSummarize.Add(row);
        }

        if (rowsToSummarize.Any())
        {
            consumption.Add(new HeatPumpConsumption(rowsToSummarize));
        }

        return consumption;
    }
    
    public List<HeatPumpLogRow> Rows { get; } = new();
}