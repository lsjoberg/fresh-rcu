namespace FreshRcu.HeatPumpLogs;

public class HeatPumpLogRow
{
    public DateTime Timestamp { get; set; }
    
    // M1
    public decimal HotWaterTemperature { get; set; }
    
    // M2
    public decimal SupplyTemperature { get; set; }
    
    public decimal CalculatedSupplyTemperature { get; set; }
    public decimal SupplyCalculatedDelta => SupplyTemperature - CalculatedSupplyTemperature;
    
    public decimal ReturnTemperature { get; set; }
    public decimal SupplyReturnDeltaT => SupplyTemperature - ReturnTemperature;
    
    public int HeatingCurve { get; set; }
    
    public int OffsetHeatingCurve { get; set; }

    public string HeatingCurveDisplay
        => OffsetHeatingCurve < 0
            ? $"{HeatingCurve}{OffsetHeatingCurve}"
            : $"{HeatingCurve}+{OffsetHeatingCurve}";
    
    public decimal DegreeMinutes { get; set; }
    public TimeSpan CalculatedRemainingTimeOn => TimeSpan.FromMinutes((double)DegreeMinutes / (double)SupplyCalculatedDelta);

    // M4
    public decimal OutsideTemperature { get; set; }
    
    public decimal OutsideAverageTemperature { get; set; }
    
    // M5
    public int NumberOfInverterStarts { get; set; }
    
    public TimeSpan InverterTotalTime { get; set; }
    
    public decimal PowerConsumption { get; set; }
    public bool InverterOn => PowerConsumption > 0;
}