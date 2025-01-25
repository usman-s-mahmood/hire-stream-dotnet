using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace HireStreamDotNetProject.Utils {
    public class TokenService
{
    private readonly IDataProtector _protector;

    public TokenService(IDataProtectionProvider provider)
    {
        // Create a unique purpose for the token
        _protector = provider.CreateProtector("MyCustomTokenPurpose");
    }

    public string GenerateToken(string jsonPayload)
    {
        // Encrypt the payload
        return _protector.Protect(jsonPayload);
    }

    public string DecryptToken(string token)
    {
        try
        {
            // Decrypt the payload
            return _protector.Unprotect(token);
        }
        catch
        {
            throw new InvalidOperationException("Invalid or expired token.");
        }
    }

}

}