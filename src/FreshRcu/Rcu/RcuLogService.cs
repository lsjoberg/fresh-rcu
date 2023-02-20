using FreshRcu.HeatPumpLogs;

namespace FreshRcu.Rcu;

public class RcuLogService
{
    private readonly RcuClient _rcuClient;

    public RcuLogService(RcuClient rcuClient)
    {
        _rcuClient = rcuClient;
    }

    public async Task<HeatPumpLog> FetchRcuLog()
    {
        var csvData = await _rcuClient.GetLogCsv();
        var parser = new RcuParser(csvData);
        return parser.Run();
    }
}