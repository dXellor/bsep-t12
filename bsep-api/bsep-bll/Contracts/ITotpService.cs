namespace bsep_bll.Contracts;

public interface ITotpService
{
    string GenerateTotpSecret();
    string GenerateTotpUri(string email, string secret);
    bool ValidateTotp(string code, string secret);
}