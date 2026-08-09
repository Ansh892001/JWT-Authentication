public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
}

public class TokenService : ITokenService
{
    public TokenService(JwtSettings settings)
    {
        Console.WriteLine("TokenService Created");
    }
    // public TokenService()
    // {
    //     Console.WriteLine("TokenService Created");
    // }
}