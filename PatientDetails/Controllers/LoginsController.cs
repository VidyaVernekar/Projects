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
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly PatientDetailsContext _context;

        public LoginsController(PatientDetailsContext context)
        {
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
                    return Ok(user);
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
