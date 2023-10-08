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
    public class CarsController : ControllerBase
    {
        readonly IMyDb _db;
        readonly IConfiguration _config;
        readonly ILogger _logger;

        public CarsController(IMyDb db, IConfiguration config, ILogger<CarsController> log)
        {
            _db = db;
            _config = config;
            _logger = log;
        }
        [HttpGet("{beneficieryId}")]
        public async Task<IActionResult> CarDeliveriesByBeneficiary(int beneficieryId)
        {
            if (beneficieryId == 0)
                return BadRequest();
            int userId;
            int.TryParse(User.FindFirst("UserId")?.Value, out userId);
            var deliveries = await _db.CarDeliveryDetailsByBeneficiary(userId, beneficieryId);
            if (deliveries is null) return NotFound();

            return Ok(deliveries);
        }
        [HttpGet]
        public async Task<IActionResult> CarDeliveriesForCurrentUser()
        {
            int userId;
            int.TryParse(User.FindFirst("UserId")?.Value, out userId);
            var deliveries = await _db.CarDeliveryDetailsByBeneficiary(userId, userId);
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