using Assets.Data.DataAccess;
using Assets.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            //login.MachineName = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
            //User user = null;
            //string name = login.MachineName;
            //bool useAD = Helpers.GetBoolValue(_config["AppSettings:UseActiveDirectory"]!);
            //if (useAD)
            //{
            //    var adUser = Helpers.ADLogin(login.UserName, login.Password);
            //    if (adUser == null)
            //    {
            //        await _db.UpdateLoginHistory(2, name, login.UserName, 0);
            //        return BadRequest(ModelState);
            //    }
            //    else
            //    {
            //        user = await _db.LoginByADInfo(adUser);
            //        await _db.UpdateLoginHistory(2, name, login.UserName, 1);
            //    }
            //}
            //else
            //{
            //    user = await _db.Login(login.UserName, login.Password);
            //    if (user == null)
            //        await _db.UpdateLoginHistory(1, name, login.UserName, 0);
            //    else
            //        await _db.UpdateLoginHistory(1, name, login.UserName, 1);

            //}
            //if (user == null)
            //{
            //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //    return BadRequest(ModelState);
            //}
            string key = _config["JwtSettings:Key"]!;
            string issuer = _config["JwtSettings:Issuer"]!;
            string audience = _config["JwtSettings:Audience"]!;
            int idNo= 1052052436;
            //int.TryParse(login.UserName, out idNo);
            var arpUser=await _db.GetArpUserTreeByUser(idNo);
            var token = Helpers.BuildToken(arpUser, key, issuer, audience);
            arpUser.Token = token;
            return Ok(arpUser);
        }
    }
}
