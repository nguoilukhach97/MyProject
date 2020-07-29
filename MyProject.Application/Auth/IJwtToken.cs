using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyProject.Application.Auth
{
    public interface IJwtToken
    {
        JwtSecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor);
        string WriteToken(JwtSecurityToken jwt);
        ClaimsPrincipal GetPrincipalFromToken(string token);
        string GenerateToken(Guid id, IEnumerable<string> roles, string userName);
    }
}
