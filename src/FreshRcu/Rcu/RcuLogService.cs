using FreshRcu.HeatPumpLogs;

namespace FreshRcu.Rcu;

public class RcuLogService
{
    private readonly IRcuLogFetcher _rcuLogFetcher;

    public RcuLogService(IRcuLogFetcher rcuLogFetcher)
    {
        _rcuLogFetcher = rcuLogFetcher;
    }

    public async Task<HeatPumpLog> FetchRcuLog()
    {
        var csvData = await _rcuLogFetcher.GetLogCsv();
        await File.WriteAllTextAsync("logfile.csv", csvData);
        var parser = new RcuParser(csvData);
        return parser.Run();
    }
}