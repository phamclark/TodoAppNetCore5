using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Configuration;
using TodoApp.Models.DTOs.Request;
using TodoApp.Models.DTOs.Response;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        protected UserManager<IdentityUser> _userManager;
        protected JwtConfig _jwtConfig;

        public AuthManagementController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor
            )
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Success = false,
                        Error = "Email already token by user"
                    });
                }
                var newUser = new IdentityUser()
                {
                    UserName = user.UserName,
                    Email = user.Email
                };

                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    string jwtToken = GenerateJwtToken(newUser);
                    return Ok(new UserRegistrationResponse()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Success = false,
                        Error= $"Something went wrong, {isCreated.Errors.Select(x=>x.Description).ToList()}"
                    });
                }
            }
            return BadRequest(new UserRegistrationResponse() 
            {
                Success = false
            });
        }

        protected string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHadler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHadler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHadler.WriteToken(token);
            return jwtToken;
        }
    }
}
