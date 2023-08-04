using Ardalis.ApiEndpoints;
using FoodDelivery.Dto;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDelivery.Endpoints.Logins
{
    public class LoginRequest
    {

        [FromBody] public UserLogin userLogin { get; set; } = default!;
    }
    public class Login : EndpointBaseAsync
       .WithRequest<LoginRequest>
        .WithActionResult
    {
        private IConfiguration _config;
        private readonly IUsersRepo userRepo;
        public Login(IConfiguration config, IUsersRepo userRepo)
        {
            _config = config;
            this.userRepo = userRepo;
        }
        [HttpPost("login")]
        public override async  Task<ActionResult> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var user = await AuthenticateAsync(request.userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return Ok("Utilizatorul nu a fost gasit");
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
