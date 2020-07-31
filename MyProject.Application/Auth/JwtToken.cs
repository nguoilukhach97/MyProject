using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MyProject.Application.Auth
{
    public class JwtToken : IJwtToken
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly ILogger<JwtToken> _logger;
        private readonly string _signingKey;
        private readonly IConfiguration _config;
        public JwtToken(ILogger<JwtToken> logger,IConfiguration config)
        {
            if (_jwtSecurityTokenHandler == null)
                _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _logger = logger;
            _config = config;
        }
        public JwtSecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor) => _jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        public string WriteToken(JwtSecurityToken jwt) => _jwtSecurityTokenHandler.WriteToken(jwt);
        

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            try
            {
                var tokenValidParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey)),
                    ValidateLifetime = false
                };
                var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidParameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }
                return principal;
            }
            catch (Exception e)
            {
                _logger.LogError("Token bị sai :"+e);
                return null;
            }
        }

        public string GenerateToken(Guid id, IEnumerable<string> roles, string userName)
        {
            if (roles == null)
            {
                roles = new List<string>();
            }
            var claims = roles
                .Select(r => new Claim(ClaimTypes.Role, r))
                .Append(new Claim(ClaimTypes.Name, id.ToString()))
                .Append(new Claim("Username", userName));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Tokens:Issuer"],
                Audience = _config["Tokens:Issuer"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };
            var token = _jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return _jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
