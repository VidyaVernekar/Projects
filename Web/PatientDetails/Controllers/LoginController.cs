using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PatientDetails.Models;

namespace PatientDetails.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly PatientDetailsContext _context;
        public IConfiguration _configuration;

        public LoginController(PatientDetailsContext context, IConfiguration config)
        {
            _configuration = config;
            _context = context;
        }
        private async Task<Login> LoginExists(string username, string password)
        {
            return await _context.Logins.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Login _userData)
        {
            if (_userData != null && _userData.UserName != null && _userData.Password != null)
            {
                var user = await LoginExists(_userData.UserName, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("Role", user.RoleId.ToString()),
                        new Claim("UserName", user.UserName)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
