using System.Text;

namespace JwtStore.Core.Contexts.SharedContext.Extensions;
public static class StringExtension
{
    public static string ToBase64(this string text)
    {
        return Convert.ToBase64String(Encoding.ASCII.GetBytes(text));
    }
}