using ACommonAuth.Contracts.Request;
using Authorization.ApplicationServices.User.Ports;
using CommonAuth.Contracts.Request;
using CommonAuth.Contracts.Response;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.ApplicationServices.User;

public class UserService : IUserService
{
    IGetUser getUser;
    ICreateUser createUser;

    public UserService(IGetUser getUser, ICreateUser createUser)
    {
        this.getUser = getUser;
        this.createUser = createUser;
    }

    public async Task<bool> CreateUserModel(CreateUserModel model)
    {
        var identity = GetIdentity(model.UserName);
        if (identity != null)
        {
            return false;
        }
       return await  createUser.AddUser(new UserData() { Username = model.UserName, Password = model.Password  , Email = model.Email});
    }

    public async  Task<LoginDto> Login(LoginModel model)
    {

        var identity = await GetIdentity(model.UserName, model.Password);
        if (identity == null)
        {
            return new LoginDto() {LoginResult = eLoginResult.BadPassword };
        }

        var now = DateTime.UtcNow;
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.Aes128CbcHmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new LoginDto
        {
            AccessToken = encodedJwt,
            UserName = identity.Name
        };

        return response;
    }

    private async Task<ClaimsIdentity?> GetIdentity(string username, string password)
    {
        var users = await getUser.GetUser();
        var person = users.FirstOrDefault(x => x.Username == username && x.Password == password);
        if (person != null)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Username!)
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        // если пользователя не найдено
        return null;
    }

    private async Task<ClaimsIdentity?> GetIdentity(string username)
    {
        var users = await getUser.GetUser();
        var person = users.FirstOrDefault(x => x.Username == username);
        if (person == null)
        {
            return null;
        }

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Username!)
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        // если пользователя не найдено
        return claimsIdentity;
    }
}
