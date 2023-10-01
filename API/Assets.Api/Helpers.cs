using Assets.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assets.Api
{
    public class Helpers
    {
        static readonly TimeSpan TokenLifeTime = TimeSpan.FromDays(10);
        public static string BuildToken(UserTest userInfo, string secret, string issuer, string audience)
        {
            var claims = new List<Claim>()
{
    new(JwtRegisteredClaimNames.Jti,userInfo.id!),
    new(ClaimTypes.Name,userInfo.name!),
    new("Email",userInfo.Email!),
    new("UserId",userInfo.id!),
    new("name",userInfo.name!),
    new("Title", userInfo.Title!),
    new("GroupId", userInfo.GroupId.ToString()),
    new("UserTypeId",userInfo.UserTypeId.ToString()),
    new("LoginId", userInfo.LoginId!),
};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: issuer,
               audience: audience,
               claims: claims,
               expires: DateTime.UtcNow.Add(TokenLifeTime),
               signingCredentials: creds);
            var jwt = tokenHandler.WriteToken(token);
            return jwt;
        }
    }
}
