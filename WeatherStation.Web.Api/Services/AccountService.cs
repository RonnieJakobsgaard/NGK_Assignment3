using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WeatherStation.Web.Api.Data;
using WeatherStation.Web.Api.Helpers;
using WeatherStation.Web.Api.Models;

namespace WeatherStation.Web.Api.Services
{
    public interface IAccountService
    {
        UserWithoutPassword Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _db;

        public AccountService()
        {
            _db = new ApplicationDbContext();
        }

        public UserWithoutPassword Authenticate(string username, string password)
        {
            var user = _db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return null;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is the best SecretKey Ever !should be moved out of here!"));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var token = new JwtSecurityToken
            (
                issuer: "WeatherStationApi",
                audience: "WeatherStation",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(28),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);


            return new UserWithoutPassword
            {
                Username = user.Username,
                Token = jwtToken
            };
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }
    }
}
