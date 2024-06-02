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
    public bool TwoFaEnabled { get; set; }
    public bool IsAwaitingTotp { get; init; }
    public string? TotpSecret { get; set; }

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
}