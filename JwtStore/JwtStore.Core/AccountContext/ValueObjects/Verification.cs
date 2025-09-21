using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public string Code { get; } = Guid.NewGuid().ToString("N")[0..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive
    {
        get
        {
            return VerifiedAt != null && ExpiresAt == null;
        }
    }

    public void Verify(string code)
    {
        if (IsActive)
        {
            throw new Exception("Este item já foi ativado");
        }

        if(ExpiresAt < DateTime.UtcNow)
        {
            throw new Exception("Este código já expirou");
        }

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
        {
            throw new Exception("Este código de verificação é inválido");
        }

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }
}