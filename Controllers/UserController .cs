using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BookingServices.Providers;
using BookingServices.Models;
using BookingServices.Dao;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using HooksLearning.Common;

namespace BookingServices.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : BaseAccountController
    {
        public UserController(BookingServicesContext bookingServicesContext) : base(bookingServicesContext) { }

        private readonly IConfiguration _config;

        [HttpGet("{id}")]
        [Authorize]
        public object Get(string id)
        {
            DaoUser daoUser = new DaoUser(bookingServicesContext);
            var data = daoUser.getById(id);
            return data;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]Users login)
        {
            IActionResult response = Unauthorized();
            login.Password = Encryptor.MD5Hash(login.Password);
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString, user = user });
            }
            else
            {
                response = Ok(new { error = "Unauthorized" });
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Logout([FromBody]Users login)
        {
            DaoUser daoUser = new DaoUser(bookingServicesContext);
            login.Password = Encryptor.MD5Hash(login.Password);
            var userId = getUserIdByToken();
            var data = daoUser.Logout(login.Password, userId);
            return data;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RefreshToken([FromBody]Users login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString, user = user });
            }

            return response;
        }

        [AllowAnonymous]
        [HttpGet("{token}")]
        public string GetTokenClaims(string token)
        {
            var tokenString = new JwtSecurityToken(jwtEncodedString: token);
            string claims = tokenString.Claims.First(c => c.Type == "Id").Value;
            return claims;
        }

        private string GenerateJSONWebToken(Users userInfo)
        {
            var Key = "ThisismySecretKey";
            var Issuer = "Test.com";
            DateTime issuedAt = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
              {
                    new Claim("UserName", "Nh?t"),
                    new Claim("Id",userInfo.Id),
                });
            var token = (JwtSecurityToken) tokenHandler.CreateJwtSecurityToken(issuer: Issuer, audience: Issuer,
               subject: claimsIdentity, notBefore: issuedAt, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);               

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Users AuthenticateUser(Users login)
        {
            DaoUser daoUser = new DaoUser(bookingServicesContext);
            Users user = daoUser.SignIn(login.Password);
            return user;
        }
    }
}
