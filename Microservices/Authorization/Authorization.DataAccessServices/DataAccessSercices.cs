using Authorization.ApplicationServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.DataAccessServices
{
    public class DataAccessSercices : IDataAcces
    {
        public Task<string?> ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = AuthOptions.GetSymmetricSecurityKey();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    // укзывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = AuthOptions.ISSUER,

                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = AuthOptions.AUDIENCE,
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,

                    // установка ключа безопасности
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var rrre = jwtToken.Claims.First(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
                //var ress = jwtToken.Claims.First(x => x.Type == "name").Value;
                var username = string.Format(rrre);

                // return user id from JWT token if validation successful
                return Task.FromResult(username);
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}