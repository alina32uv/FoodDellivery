using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUsersRepo userRepo;
        public LoginController(IConfiguration config, IUsersRepo userRepo)
        {
            _config = config;
            this.userRepo = userRepo;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await AuthenticateAsync(userLogin);

            if(user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("Utilizatorul nu a fost gasit");
        }

        private string Generate(UsersModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.FName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Surname, user.LName),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private async Task<UsersModel> AuthenticateAsync(UserLogin userLogin)
        {
            var currentUser = await userRepo.GetUser(userLogin.FName, userLogin.Password);
            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        
        }
    }
}
