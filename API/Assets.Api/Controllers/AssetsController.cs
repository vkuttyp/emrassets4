using Assets.Data.DataAccess;
using Assets.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Assets.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AssetsController : ControllerBase
    {
        readonly IMyDb _db;
        readonly IConfiguration _config;
        readonly ILogger _logger;

        public AssetsController(IMyDb db, IConfiguration config, ILogger<AssetsController> log)
        {
            _db = db;
            _config = config;
            _logger = log;
        }
        [HttpGet("{beneficieryId}")]
        public async Task<IActionResult> AssetDeliveriesByBeneficiary(int beneficieryId)
        {
            if (beneficieryId == 0) 
                return BadRequest();
            int userId;
            int.TryParse(User.FindFirst("UserId")?.Value, out userId);
            var deliveries= await _db.AssetDeliveriesByBeneficiary(userId, beneficieryId);
            if(deliveries is null) return NotFound();

            return Ok(deliveries);
        }
        [HttpGet]
        public async Task<IActionResult> AssetDeliveriesForCurrentUser()
        {
            int userId;
            int.TryParse(User.FindFirst("UserId")?.Value, out userId);
            var deliveries = await _db.AssetDeliveriesByBeneficiary(userId, userId);
            if (deliveries is null) return NotFound();

            return Ok(deliveries);
        }
        [HttpGet]
        public async Task<IActionResult> SubordinateDetails()
        {
            int userId;
            int.TryParse(User.FindFirst("UserId")?.Value, out userId);
            var subordinates = await _db.SubordinateDetails(userId);
            if (subordinates is null) return NotFound();

            return Ok(subordinates);
        }
    }
}
