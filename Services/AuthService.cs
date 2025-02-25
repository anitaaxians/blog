using BlogManagementAPI.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogManagementAPI.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> RegisterUser(LoginUser user)
        {
            var identityUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = true,
                Birthdate = user.Birthdate

            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);
            return result.Succeeded;
        }
        public async Task<string> GenerateTokenString(LoginDto user)
        {
         
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            var claims = new List<Claim>
            {  new Claim("UserId",identityUser.Id),
                new Claim(ClaimTypes.Email,identityUser.UserName),
                new Claim(ClaimTypes.Name,identityUser.FirstName),
                new Claim(ClaimTypes.Surname,identityUser.LastName),
                new Claim(ClaimTypes.Role,"Admin")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredentials);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }


        //public async Task<string> GenerateTokenString(LoginDto user)
        //{
        //    var identityUser = await _userManager.FindByEmailAsync(user.UserName);
        //    var claims = new List<Claim>
        //    {  
        //        new Claim(ClaimTypes.Email,user.UserName),
        //        new Claim("Password",user.Password)

        //    };
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

        //    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        //    var securityToken = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(60),
        //        issuer: _configuration.GetSection("Jwt:Issuer").Value,
        //        audience: _configuration.GetSection("Jwt:Audience").Value,
        //        signingCredentials: signingCredentials);

        //    string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        //    return tokenString;
        //}
        
        public async Task<bool> Login(LoginDto user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            if (identityUser == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }
    }
}
