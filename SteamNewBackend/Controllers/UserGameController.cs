using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;
using SteamNewBackend.Services;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserGameController : ControllerBase
    {
        private readonly IUserGameService _userGameSerivce;
        private readonly MariaDbContext _mariaDb;
        public UserGameController(IUserGameService userGameService, MariaDbContext context)
        {
            _userGameSerivce = userGameService;
            _mariaDb = context;
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> Purchase([FromForm] Purchase purchase)
        {
            try
            {
                if (purchase == null) return BadRequest();

                var result = await _userGameSerivce.Purchase(purchase);
                if (result == 2) return BadRequest(new
                {
                    Message = "You already own this game!"
                });
                return Ok(new
                {
                    Message = "Your purchase was successful!"
                });
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
