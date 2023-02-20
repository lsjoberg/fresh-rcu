namespace FreshRcu.HeatPumpLogs;

public class HeatPumpLogRow
{
    public DateTime Timestamp { get; set; }
    
    public decimal HotWaterTemperature { get; set; }
    
    public decimal SupplyTemperature { get; set; }
    
    public decimal CalculatedSupplyTemperature { get; set; }
    
    public decimal ReturnTemperature { get; set; }
    
    public int HeatingCurve { get; set; }
    
    public int OffsetHeatingCurve { get; set; }

    public string HeatingCurveDisplay
        => OffsetHeatingCurve < 0
            ? $"{HeatingCurve}{OffsetHeatingCurve}"
            : $"{HeatingCurve}+{OffsetHeatingCurve}";
    
    public decimal DegreeMinutes { get; set; }
    
    public decimal PowerConsumption { get; set; }
}