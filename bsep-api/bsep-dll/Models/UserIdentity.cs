using bsep_dll.Models.Enums;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using static System.Net.WebRequestMethods;

namespace bsep_dll.Models;

public class UserIdentity
{
    public virtual User? User { get; init; }

    public string Email { get; init; }
    public string Password { get; private set; }
    public byte[] Salt { get; private set; }
    public int Iterations { get; init; }
    public int OutputLength { get; init; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpirationDateTime { get; set; }
    public bool TwoFaEnabled { get; set; }
    public bool IsAwaitingTotp { get; init; }
    public string? TotpSecret { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpirationDateTime { get; set; }
    public AcountStatusEnum AccountStatus { get; set; }

    public UserIdentity(string email, string password, byte[] salt, int iterations, int outputLength)
    {
        Email = email;
        Password = password;
        Salt = salt;
        Iterations = iterations;
        OutputLength = outputLength;
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

    public void BlockAccount()
    {
        AccountStatus = AcountStatusEnum.BLOCKED;
        RefreshToken = null;
        InvalidatePasswordResetToken();
    }

    public void SetPasswordResetToken(string passwordResetToken, DateTime expires)
    {
        PasswordResetToken = passwordResetToken;
        PasswordResetTokenExpirationDateTime = expires;
    }

    public void InvalidatePasswordResetToken()
    {
        PasswordResetToken = null;
        PasswordResetTokenExpirationDateTime = null;
    }

    public bool IsTokenValid(string token, string secretKey)
    {
        if(token == null) return false;

        var secret = secretKey;
        var encoding = new ASCIIEncoding();

        string tokenhash;
        using (var hmacsha256 = new HMACSHA256(encoding.GetBytes(secret)))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(encoding.GetBytes(token));
            tokenhash = Convert.ToBase64String(hashmessage);
        }
        if (!tokenhash.Equals(PasswordResetToken) || DateTime.UtcNow >= PasswordResetTokenExpirationDateTime) return false;

        return true;
    }

    public void SetNewPassword(string password, int saltLength)
    {
        var salt = RandomNumberGenerator.GetBytes(saltLength);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithmName.SHA512,
            OutputLength
        );

        Password = Convert.ToHexString(hash);
        Salt = salt;
    }

    public bool IsBlocked()
    {
        return AccountStatus == AcountStatusEnum.BLOCKED;
    }
}