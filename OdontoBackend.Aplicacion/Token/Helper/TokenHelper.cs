using Microsoft.IdentityModel.Tokens;
using OdontoBackend.Aplicacion.ViewModels.User;
using OdontoBackend.Domain.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace OdontoBackend.Services.Api.Configurations.Token.Helper
{
    public class TokenHelper
    {
        public const string Issuer = "https://codingsonata.com";
        public const string Audience = "https://codingsonata.com";
        public const string Secret = "p0GXO6VuVZLRPef0tyO9jCqK4uZufDa6LP4n8Gj+8hQPB30f94pFiECAnPeMi5N6VT3/uscoGH7+zJrv4AuuPg==";
        public static async Task<string> GenerateAccessToken(UserViewModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(Secret);
            Debug.WriteLine("key" + key);
            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.codigoUsuario.ToString())
            });
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = Issuer,
                Audience = Audience,
                Expires = DateTime.UtcNow.AddMinutes(10),// DateTime.Now.AddMinutes(301),//5 HORAS,//DateTime.Now.AddMinutes(5),
                SigningCredentials = signingCredentials,
            };
            Debug.WriteLine(DateTime.UtcNow);
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return await Task.Run(() => tokenHandler.WriteToken(securityToken));
        }
        public static async Task<string> GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            await System.Threading.Tasks.Task.Run(() => randomNumberGenerator.GetBytes(secureRandomBytes));
            var refreshToken = Convert.ToBase64String(secureRandomBytes);
            return refreshToken;
        }
    }
}
