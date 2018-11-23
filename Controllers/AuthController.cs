using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TestApiBakery.Data;
using TestApiBakery.Models;

namespace TestApiBakery.Controllers
{
    [Produces("application/json")]
    public class AuthController : Controller
    {

        private ILogger<AuthController> _logger;
        private SignInManager<AppUser> _signInMgr;
        private UserManager<AppUser> _userMgr;
        private IPasswordHasher<AppUser> _hasher;
        private IConfiguration _config;

        public AuthController(
          SignInManager<AppUser> signInMgr,
          UserManager<AppUser> userMgr,
          IPasswordHasher<AppUser> hasher,
          ILogger<AuthController> logger,
          IConfiguration config)
        {
            _signInMgr = signInMgr;
            _logger = logger;
            _userMgr = userMgr;
            _hasher = hasher;
            _config = config;
        }

        [HttpPost("api/auth/token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialModel model)
        {
            try
            {
                var user = await _userMgr.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var claims = new List<Claim> {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                           };

                        var userRoles = await _userMgr.GetRolesAsync(user);

                        foreach (var userRole in userRoles)
                        {
                            claims.Add(new Claim("role", userRole));

                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                          issuer: _config["Jwt:Issuer"],
                          audience: _config["Jwt:Audience"],
                          claims: claims,
                          //expires: DateTime.UtcNow.AddMinutes(30),
                          signingCredentials: creds
                          );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            //expiration = token.ValidTo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception thrown while creating JWT: {ex}");
            }

            return BadRequest("Failed to generate token");
        }
    }
}