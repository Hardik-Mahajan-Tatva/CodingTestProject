using CoddingAssessmentProject.Repositories.Intefaces;
using CoddingAssessmentProject.Services.Intefaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoddingAssessmentProject.Services.Implmentations
{
    public class JwtService : IJwtService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly IUsersRepository _userRepository;
        public JwtService(IConfiguration configuration, IUsersRepository userRepository)
        {
            _key = configuration["Jwt:Key"]
                ?? throw new ArgumentNullException(nameof(configuration), "JWT key is missing in configuration.");

            _issuer = configuration["Jwt:Issuer"]
                ?? throw new ArgumentNullException(nameof(configuration), "JWT issuer is missing in configuration.");

            _audience = configuration["Jwt:Audience"]
                ?? throw new ArgumentNullException(nameof(configuration), "JWT audience is missing in configuration.");

            _userRepository = userRepository;
        }
        #region  Generate Token
        public async Task<string> GenerateJwtToken(string userEmail, bool rememberMe = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);

            var userDetail = await _userRepository.GetUserByEmailAsync(userEmail)
                ?? throw new Exception("User not found while generating JWT token.");

            var user = await _userRepository.GetUserByEmailAsync(userEmail);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, userEmail),
        new Claim("username", userDetail.UserName),
        new Claim(ClaimTypes.Email, userDetail.UserEmail),
        new Claim(ClaimTypes.Role, user.UserRole),

    };

            if (rememberMe)
            {
                claims.Add(new Claim("RememberMe", "True"));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion
        #region  ValidatToken
        public ClaimsPrincipal? ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}