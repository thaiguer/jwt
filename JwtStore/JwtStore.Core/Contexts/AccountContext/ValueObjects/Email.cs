using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.SharedContext.Extensions;
using JwtStore.Core.Contexts.SharedContext.ValueObjects;
using System.Text.RegularExpressions;

namespace JwtStore.Core.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    protected Email()
    {

    }

    public Email(string address)
    {
        if(string.IsNullOrEmpty(address))
        {
            throw new Exception("address cant be null or empty");
        }

        address = address.Trim().ToLower();

        if(address.Length < 5)
        {
            throw new Exception("address cant shorter than 5 char");
        }

        if (!EmailRegex().IsMatch(address))
        {
            throw new Exception("address does not match an e-mail format");
        }

        Address = address;
    }

    public string Address { get; }
    public string Hash => Address.ToBase64();
    public Verification Verification { get; private set; } = new();
    public void ResendVerification()
    {
        Verification = new Verification();
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

    #region Implicit Operators
    
    public static implicit operator string(Email email)
    {
        return email.ToString();
    }

    public static implicit operator Email(string address)
    {
        return new Email(address);
    }

    #endregion
}