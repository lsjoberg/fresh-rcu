using System.Text;

namespace FreshRcu.Rcu;

public static class RcuExtensions
{
    public static string ToBase64(this string stringToEncode)
    {
        var utf8Bytes = Encoding.UTF8.GetBytes(stringToEncode);
        return Convert.ToBase64String(utf8Bytes);
    }
}