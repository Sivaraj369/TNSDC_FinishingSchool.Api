using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TNSDC_FinishingSchool.Domain.Models;

namespace TNSDC_FinishingSchool.Bussiness.JWT
{
    public class JwtUtils : IJwtUtils
    {
        private readonly IConfiguration _config;

        public JwtUtils(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string GenerateJwtToken(string CandidateId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("JwtSettings")["Key"]?.ToString());
            int tokenExpiryInHours = int.Parse(_config.GetSection("JwtSettings")["TokenExpiryInHours"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id",CandidateId),}),
                Expires = DateTime.UtcNow.AddHours(tokenExpiryInHours),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //public RefreshToken GenerateRefreshToken(string ipAddress)
        //{
        //    var refreshToken = new RefreshToken
        //    {
        //        Token = getUniqueToken(),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        Created = DateTime.UtcNow,
        //        CreatedByIp = ipAddress,
        //    };
        //    return refreshToken;

        //    string getUniqueToken()
        //    {
        //        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        //        var IsTokenUnique=!_context.Users.Any(u=>u.refreshTokens.Any(t=>t.Token==token));
        //        if(!IsTokenUnique)
        //            return getUniqueToken();
        //        return token;
        //    }

        //}

        public int? ValidateJwtToken(string Token)
        {
            if (Token == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("JwtSettings")["Key"]?.ToString());
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
                tokenHandler.ValidateToken(Token, tokenValidationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}
