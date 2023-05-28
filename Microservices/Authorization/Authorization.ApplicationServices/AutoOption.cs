using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authorization.ApplicationServices
{
    public class AuthOptions
    {
        public const string ISSUER = "AutoriztionMercury"; // издатель токена
        public const string AUDIENCE = "ChatMercury"; // потребитель токена
        const string KEY = "0123456789abcdef";   // ключ для шифрации
        public const int LIFETIME = 10; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}