using System.Security.Claims;

namespace TransactionalAPIMaddiApp.Helpers.Token
{
    public interface ITokenHelper
    {
        string CreateToken(IEnumerable<Claim>? claims, TimeSpan expiration);
        bool ValidateToken(string token);
    }
}
