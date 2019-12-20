using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CustomerMgmt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace CustomerMgmt.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        
        public LoginController(IConfiguration config)
        {
            _config = config;
           
        }

        [HttpGet]
        public IActionResult Login(string username, string pass)
        {
            UserModel login = new UserModel
            {
                UserName = username,
                Password = pass
        };
            
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr });
            }
            return response;
        }
       
        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;
            if (login.UserName == "user" && login.Password == "user")
            {
                user = new UserModel { UserName = "user",EmailAddress = "brindhamaniam@gmail.com",Password = "user" };
            }
            return user;
        }
        private string GenerateJSONWebToken(UserModel userinfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userinfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email,userinfo.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;

        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var username = claim[0].Value;
            return "Welcome To:" + username;
        }
        
    }

}



