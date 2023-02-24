namespace FreshRcu.Rcu;

public interface IRcuLogFetcher
{
    Task<string> GetLogCsv();
}