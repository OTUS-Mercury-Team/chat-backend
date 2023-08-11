using Authorization.ApplicationServices;
using DataAccess;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.DataAccessServices
{
    public class DataAccessSercices : IDataAcces
    {
        IGetUser getUser;

        public DataAccessSercices(IGetUser getUser)
        {
            this.getUser = getUser;
        }

        public async Task<uint?> ValidateToken(string token)
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
                    ValidateLifetime = false,

                    // установка ключа безопасности
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var rrre = jwtToken.Claims.First(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
                var username = string.Format(rrre);

                // return user id from JWT token if validation successful
                return await GetIdentity(username);
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }


        private async Task<uint?> GetIdentity(string username)
        {
            var users = await getUser.GetUser();
            var person =  users.FirstOrDefault(x => x.Username == username);
            if (person != null)
            {
                return person.Id;
            }
            return null;
        }
    }
}