using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace FreshRcu.Rcu;

public class RcuClient
{
    private readonly HttpClient _rcuHttpClient;

    public RcuClient(IOptions<RcuOptions> rcuOptions)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri($"http://{rcuOptions.Value.Host}");
        httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("basic", rcuOptions.Value.BasicAuth);
        _rcuHttpClient = httpClient;
    }

    public async Task<string> GetLogCsv()
    {
        var response = await _rcuHttpClient.GetAsync("/cgi-bin/log.csv?parlog");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}