using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using StairsAndShit.Core.ApplicationService;
using StairsAndShit.Core.Entity;
using StairsAndShit.Core.Helpers;
using JwtSecurityKey = StairsAndShit.Core.Helpers.JwtSecurityKey;


namespace StairsAndShit.RestApi.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private readonly IUserService<User> _userService;

        public LoginController(IUserService<User> userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public IActionResult Login([FromBody]LoginInputModel model)
        {
            var user = _userService.GetAll(null).FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = GenerateToken(user)
            });
        }       
        
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
        
        private string GenerateToken(User user)
        {            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            if (user.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    JwtSecurityKey.Key, 
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null,
                    null, 
                    claims.ToArray(), 
                    DateTime.Now,        
                    DateTime.Now.AddMinutes(10))); 

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }  
}
