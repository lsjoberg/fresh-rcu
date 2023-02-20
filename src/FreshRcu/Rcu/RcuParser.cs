using FreshRcu.HeatPumpLogs;

namespace FreshRcu.Rcu;

public class RcuParser
{
    public const char Separator = ';';

    private readonly List<RcuCsvRow> _rows;
    
    public RcuParser(string csvContent)
    {
        var rows = csvContent.Split(Environment.NewLine);
        var headerRow = rows.First();
        var menuColumnMap = new RcuCsvColumnMap(headerRow);

        _rows = rows.Skip(1)
            .Where(row => !string.IsNullOrEmpty(row))
            .Select(row => new RcuCsvRow(menuColumnMap, row))
            .ToList();
    }

    public HeatPumpLog Run()
    {
        var log = new HeatPumpLog();

        foreach (var csvRow in _rows)
        {
            var logRow = new HeatPumpLogRow();

            // Time
            logRow.Timestamp = csvRow.GetTimestamp();
            
            // Hot water
            logRow.HotWaterTemperature = csvRow.GetDecimal("M1.0");
            
            // Supply water
            logRow.SupplyTemperature = csvRow.GetDecimal("M2.0", 0);
            logRow.CalculatedSupplyTemperature = csvRow.GetDecimal("M2.0", 1);
            logRow.HeatingCurve = csvRow.GetInt("M2.1");
            logRow.OffsetHeatingCurve = csvRow.GetInt("M2.2");
            logRow.ReturnTemperature = csvRow.GetDecimal("M2.7");
            logRow.DegreeMinutes = csvRow.GetDecimal("M2.8");
            
            // Inverter
            logRow.PowerConsumption = csvRow.GetDecimal("M5.12.2");

            log.Rows.Add(logRow);
        }
        
        return log;
    }
}