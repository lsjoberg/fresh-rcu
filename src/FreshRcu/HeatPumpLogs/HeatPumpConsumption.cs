namespace FreshRcu.HeatPumpLogs;

public class HeatPumpConsumption
{
    public DateTime From { get; }
    
    public DateTime To { get; }
    
    public List<string> HeatingCurves { get; }
    
    public decimal KiloWattHours { get; }

    public HeatPumpConsumption(List<HeatPumpLogRow> rows)
    {
        From = rows.Min(row => row.Timestamp);
        To = rows.Max(row => row.Timestamp);
        KiloWattHours = rows.Sum(row => row.PowerConsumption / 60.0M) / 1000.0M;
        HeatingCurves = rows.Select(row => $"{row.HeatingCurveDisplay}").Distinct().ToList();
    }
}