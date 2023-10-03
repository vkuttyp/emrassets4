using Assets.Data.DataAccess;
using Assets.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Assets.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMyDb _db;
        readonly IConfiguration _config;
        readonly ILogger _logger;

        public UsersController(IMyDb db, IConfiguration config, ILogger<UsersController> log)
        {
            _db = db;
            _config = config;
            _logger = log;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginData login)
        {
            if (login == null || login.UserName == null || login.Password == null)
                return BadRequest();
            login.MachineName = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
            string name = login.MachineName;
            bool useAD = Helpers.GetBoolValue(_config["AppSettings:UseActiveDirectory"]!);
            ArpUser? arpUser= null;
            int idNo = 0;
            int.TryParse(login.UserName, out idNo);
            int status= 0;
            if (useAD)
            {
                var adUser = Helpers.ADLogin(login.UserName, login.Password);
                status = adUser == null ? 0 : 1;
                await _db.UpdateLoginHistory(2, name, login.UserName, status);
                if (adUser == null)
                {
                    var problem = new Problem(400, "Invalid User name or Password", "Invalid User name or Password", "Invalid Credentials");
                    string json = JsonSerializer.Serialize(problem);
                    return Unauthorized(json);
                }
                arpUser = await _db.GetArpUserTreeByUser(idNo);
                if (arpUser == null)
                {
                    var problem = new Problem(500, "ARP could not be retrieved", "ARP could not be retrieved", "Could not retrieve ARP user information");
                    string json = JsonSerializer.Serialize(problem);
                    return NotFound(json);
                }
            }
            else
            {
                arpUser = await _db.Login(idNo, login.Password);
                status=arpUser == null ? 0 : 1;
                await _db.UpdateLoginHistory(1, name, login.UserName, status);
               if(arpUser == null)
                {
                    var problem = new Problem(400, "Invalid User name or Password", "Invalid User name or Password", "Invalid Credentials");
                    string json = JsonSerializer.Serialize(problem);
                    return Unauthorized(json);
                }

            }
         
            string key = _config["JwtSettings:Key"]!;
            string issuer = _config["JwtSettings:Issuer"]!;
            string audience = _config["JwtSettings:Audience"]!;
            
            var token = Helpers.BuildToken(arpUser, key, issuer, audience);
            arpUser.Token = token;
            return Ok(arpUser);
        }
    }
}
