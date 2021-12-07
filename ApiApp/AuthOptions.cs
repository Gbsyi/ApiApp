using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiApp
{
    public class AuthOptions
    {
        public const string ISSUER = "AuthServer";
        public const string AUDIENCE = "AuthClient";
        private const string SECRET = Config.SECRET;
        public const int LIFETIME = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET));
        }
    }
}
