namespace FreshRcu.Rcu;

public class RcuLogFileFetcher : IRcuLogFetcher
{
    public Task<string> GetLogCsv()
    {
        return File.ReadAllTextAsync("logfile.csv");
    }
}