
namespace Infrastructure.Configurations;

public class JwtSettings
{
    public string SecretKey { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
}
