using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly TodoContext _context;

        public TokenController(IConfiguration config, TodoContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public IActionResult Post()
        {
            //create claims details based on the user information
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["JWT:Issuer"], _configuration["JWT:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}
