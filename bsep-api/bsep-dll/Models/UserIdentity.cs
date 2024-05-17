using bsep_dll.Models.Enums;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace bsep_dll.Models;

public class UserIdentity
{
    public virtual User? User { get; init; }
    
    public string Email { get; init; }
    public string Password { get; init; }
    public byte[] Salt { get; init; }
    public int Iterations { get; init; }
    public int OutputLength { get; init; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpirationDateTime { get; set; }
    public string? ActivationToken { get; set; }
    public DateTime? ActivationTokenExpirationDateTime { get; set; }
    public string? Otp { get; set; }
    public DateTime? OtpExpirationDateTime { get; set; }
    public AcountStatusEnum AccountStatus { get; set; }
    public DateTime? BlockedUntilDateTime { get; set; }
    public UserIdentity(string email, string password, byte[] salt, int iterations, int outputLength)
    {
        Email = email;
        Password = password;
        Salt = salt;
        Iterations = iterations;
        OutputLength = outputLength;
        AccountStatus = AcountStatusEnum.ACTIVATION_PENDING;
    }

    public static UserIdentity CreateUserIdentity(string email, string password, int saltLength, int iterations, int outputLength)
    {
        var salt = RandomNumberGenerator.GetBytes(saltLength);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA512,
            outputLength
        );

        return new UserIdentity(email, Convert.ToHexString(hash), salt, iterations, outputLength);
    }

    public bool VerifyCredentials(string email, string password)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Salt,
            Iterations,
            HashAlgorithmName.SHA512,
            OutputLength
        );
        return Email.Equals(email) && CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(Password));
    }

    public void SetRefreshToken(string refreshToken, DateTime expires)
    {
        var hashedToken = SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken));
        RefreshToken = Convert.ToHexString(hashedToken);
        RefreshTokenExpirationDateTime = expires;
    }

    public bool VerifyRefreshToken(string refreshToken)
    {
        if (RefreshToken == null)
            return false;
        var hashToCompare = SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken));
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(RefreshToken)) && DateTime.UtcNow < RefreshTokenExpirationDateTime;
    }

    public void InvalidateRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpirationDateTime = null;
    }

    public bool VerifyActivationToken(string activationToken)
    {
        if (ActivationToken == null)
            return false;
        var hashToCompare = SHA256.HashData(Encoding.UTF8.GetBytes(activationToken));
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(ActivationToken)) && DateTime.UtcNow < ActivationTokenExpirationDateTime;
    }

    public void SetActivationToken(string activationToken, DateTime expires)
    {
        ActivationToken = activationToken;
        ActivationTokenExpirationDateTime = expires;
    }

    public void SetOtp(string otp, DateTime expires)
    {
        Otp = otp;
        OtpExpirationDateTime = expires;
    }

    public void InvalidateOtp()
    {
        Otp = null;
        OtpExpirationDateTime = null;
    }

    public void ActivateAccount()
    {
        AccountStatus = AcountStatusEnum.ACTIVATED;
    }

    public void BlockAccount()
    {
        AccountStatus = AcountStatusEnum.BLOCKED;
        BlockedUntilDateTime = DateTime.UtcNow.AddMonths(3);
    }
}