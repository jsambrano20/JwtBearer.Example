using JwtBearer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtBearer.Services
{
    public class TokenService
    {
        public string Generate(User user)
        {
            // Cria uma instância do JwtSecurityTokenHandler
            // TODO: Objeto que gera o token
            var handler = new JwtSecurityTokenHandler();

            // Gera uma key em bytes
            var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);

            var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2)
            };

            // Gera um Token
            var token = handler.CreateToken(tokenDescriptor);

            // Gera uma string do Token
            return handler.WriteToken(token);
        }
    
        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));

            foreach(var role in user.Roles)
                ci.AddClaim(new Claim(ClaimTypes.Role, role));

            return ci;  
        }
    }
}
