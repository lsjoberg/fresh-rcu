// ReSharper disable UnusedMember.Global
#pragma warning disable CS8618
namespace FreshRcu.Rcu;

public class RcuOptions
{
    public string Host { get; set; }
    public string BaseAddress => $"http://{Host}";

    public string Username { get; set; }
    public string Password { get; set; }

    public string BasicAuth => $"{Username}:{Password}".ToBase64();
}