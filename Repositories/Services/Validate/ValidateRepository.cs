using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace FnolApiVersion2.O.Repositories.Services.Validate
{
    public class ValidateRepository : IValidateRepository
    {
        private readonly IConfiguration _configuration;
        public ValidateRepository(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        public bool Authenticate(string token) 
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            SecurityToken validatedToken;
            var validator = new JwtSecurityTokenHandler();

            // These need to match the values used to generate the token
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidIssuer = _configuration["Jwt:Issuer"];
            validationParameters.ValidAudience = _configuration["Jwt:Audience"];
            validationParameters.IssuerSigningKey = key;
            validationParameters.ValidateIssuerSigningKey = true;
            validationParameters.ValidateAudience = true;

            if (validator.CanReadToken(token))
            {
                try
                {
                    ClaimsPrincipal principal;
                
                    // This line throws if invalid
                    principal = validator.ValidateToken(token, validationParameters, out validatedToken);

                    // If we got here then the token is valid
                    if (principal.HasClaim(c => c.Type == ClaimTypes.Email))
                    {
                        return true;
                    }

                }
                catch(Exception)
                {
                    return false;
                }
                
            }
            return false;
        }
    }
}