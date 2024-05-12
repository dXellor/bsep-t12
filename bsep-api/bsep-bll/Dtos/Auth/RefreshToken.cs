namespace bsep_bll.Dtos.Auth;

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
}