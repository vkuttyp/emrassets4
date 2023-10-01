using Assets.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assets.Api;

public class Helpers
{
    static readonly TimeSpan TokenLifeTime = TimeSpan.FromDays(10);
    public static string BuildToken(ArpUser userInfo, string secret, string issuer, string audience)
    {
        var claims = new List<Claim>()
            {
            new(JwtRegisteredClaimNames.Jti,userInfo.IdNo!),
            new(ClaimTypes.Name,userInfo.name!),
            new("Email",userInfo.Email!),
            new("UserId",userInfo.id.ToString()),
            new("name",userInfo.name!),
            new("Title", userInfo.Title!),
            new("GroupId", userInfo.GroupId.ToString()),
            new("UserTypeId",userInfo.UserTypeId.ToString()),
            new("LoginId", userInfo.IdNo!),
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

    public static bool GetBoolValue(string val)
    {
        if (val == null) return false;
        bool boolVal = false;
        bool.TryParse(val, out boolVal);
        return boolVal;
    }

    static string path = "LDAP://emrbaha.com";
    public static ADUserInfo ADLogin(string username, string password)
    {
        try
        {
            using (var entry = new DirectoryEntry(path, username, password))
            {
                using (var searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = $"(samaccountname={username})";
                    var result = searcher.FindOne();
                    if (result == null) return null;

                    return GetADUser(result);
                }
            }

        }
        catch (Exception ex)
        {
            return null;
        }
    }
    private static ADUserInfo GetADUser(System.DirectoryServices.SearchResult result)
    {
        var user = new ADUserInfo();
        var fields = result.Properties;
        foreach (string ldapField in fields.PropertyNames)
        {
            foreach (var mycollection in fields[ldapField])
            {
                if (ldapField == "samaccountname")
                {
                    user.IdNo = mycollection.ToString();
                }
                if (ldapField == "name")
                {
                    user.FullName = mycollection.ToString();
                }
                if (ldapField == "mobile")
                {
                    user.MobileNo = mycollection.ToString();
                }
                if (ldapField == "mail")
                {
                    user.Email = mycollection.ToString();
                }
                if (ldapField == "givenname")
                {
                    user.FirstName = mycollection.ToString();
                }
                if (ldapField == "sn")
                {
                    user.LastName = mycollection.ToString();
                }
            }
        }
        return user;
    }
}
