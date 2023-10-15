using Assets.Data.DataAccess;
using Assets.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{beneficiaryId}")]
        public async Task<IActionResult> CarDeliveriesByBeneficiary(int beneficiaryId)
        {
            if (beneficiaryId == 0)
                return BadRequest();
            int.TryParse(User.FindFirst("UserId")?.Value, out int userId);
            var deliveries = await _db.CarDeliveryDetailsByBeneficiary(userId, beneficiaryId);
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

        [HttpPost]
        public async Task<IActionResult> RequestCar([FromBody]CarRequest? request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            int.TryParse(User.FindFirst("UserId")?.Value, out int userId);
            request.UserId = userId;
            request = await _db.CarRequestUpdate(request);
            return Ok(request);
        }

        [HttpGet("{beneficiaryId}")]
        public async Task<IActionResult> CarRequestDetailsByBeneficiary(int beneficiaryId)
        {
            if (beneficiaryId == 0)
                return BadRequest();
            int userId;
            int.TryParse(User.FindFirst("UserId")?.Value, out userId);
            var deliveries = await _db.CarRequestDetailsByBeneficiary(userId, beneficiaryId) ?? new List<CarRequest>();

            return Ok(deliveries);
        }

        // Car Manager Response
        [HttpPost]
        public async Task<IActionResult> ResponseCar([FromBody] CarManagerResponse? response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            int.TryParse(User.FindFirst("UserId")?.Value, out int userId);
            response.ManagerId = userId;
            response = await _db.CarManagerResponseUpdate(response);
            return Ok(response);
        }

        [HttpGet("{typeId}")]
        public async Task<IActionResult> GetListItems(int typeId)
        {
            if (typeId == 0)
                return BadRequest();
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            var items = await _db.GetListItems(typeId, userId) ?? new List<MyListItem>();

            return Ok(items);
        }

        [HttpGet]
        public async Task<IActionResult> CarVotingPendingList()
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            var items = await _db.CarVotingPendingList(userId);

            return Ok(items);
        }

        // Car board voting
        [HttpPost]
        public async Task<IActionResult> CarVoting([FromBody] CarVotingDetail? vote)
        {
            //if (response == null) throw new ArgumentNullException(nameof(response));
            //int.TryParse(User.FindFirst("UserId")?.Value, out int userId);
            //response.ManagerId = userId;
            //response = await _db.CarManagerResponseUpdate(response);
            return Ok(vote);
        }
    }
}