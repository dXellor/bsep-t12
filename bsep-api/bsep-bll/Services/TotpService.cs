using System.Security.Cryptography;
using bsep_bll.Contracts;
using OtpNet;

namespace bsep_bll.Services;

public class TotpService: ITotpService
{
    public string GenerateTotpSecret()
    {
        return Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(64));
    }

    public string GenerateTotpUri(string email, string secret)
    {
        return new OtpUri(OtpType.Totp, secret, email, "BSEP T12", OtpHashMode.Sha256).ToString();
    }

    public bool ValidateTotp(string totp, string secret)
    {
        var totpManager = new Totp(Base32Encoding.ToBytes(secret), mode: OtpHashMode.Sha256);
        var window = new VerificationWindow(previous:1, future:1);
        
        return totpManager.VerifyTotp(totp, out var timeWindowUsed, window: window);
    }
}