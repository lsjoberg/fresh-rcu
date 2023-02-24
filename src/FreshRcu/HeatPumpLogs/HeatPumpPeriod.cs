namespace FreshRcu.HeatPumpLogs;

public class HeatPumpPeriod
{
    public DateTime From { get; }
    
    public DateTime To { get; }
    
    public List<string> HeatingCurves { get; }
    
    public decimal KiloWattHours { get; }

    public TimeSpan InverterOnTime { get; }
    
    public int NumberOfInverterStarts { get; }
    
    public decimal AverageOutsideTemperature { get; }

    public HeatPumpPeriod(IReadOnlyCollection<HeatPumpLogRow> rows)
    {
        From = rows.Min(row => row.Timestamp);
        To = rows.Max(row => row.Timestamp);
        KiloWattHours = Math.Round(rows.Sum(row => row.PowerConsumption / 60.0M) / 1000.0M, 2);
        HeatingCurves = rows.Select(row => $"{row.HeatingCurveDisplay}").Distinct().ToList();
        InverterOnTime = rows.Max(row => row.InverterTotalTime) - rows.Min(row => row.InverterTotalTime);
        NumberOfInverterStarts = rows.Max(row => row.NumberOfInverterStarts) - rows.Min(row => row.NumberOfInverterStarts);
        AverageOutsideTemperature = Math.Round(rows.Average(row => row.OutsideTemperature), 1);
    }
}