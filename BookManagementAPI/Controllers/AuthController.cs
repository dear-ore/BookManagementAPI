using BookManagementAPI.DTOs;
using BookManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterDto registerUser)
        {
            var newUser = new ApplicationUser
            {
                UserName = registerUser.Username,
                Email = registerUser.Email
            };
            
            var result = await _userManager.CreateAsync(newUser, registerUser.Password);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken(newUser);

                var response = new AuthResponseDto
                {
                    JwtToken = token,
                    Username = newUser.UserName,
                    Email = newUser.Email
                };

                return Ok(response);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto userLogin)
        { 
            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            if(user == null)
            {
                return Unauthorized("Invalid Credentials");
            }

            var result = await _signInManager.PasswordSignInAsync(user, userLogin.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);

                var response = new AuthResponseDto
                {
                    JwtToken = token,
                    Username = user.UserName,
                    Email = user.Email
                };
                
                return Ok(response);
            }

            return Unauthorized("Invalid Credentials");
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
