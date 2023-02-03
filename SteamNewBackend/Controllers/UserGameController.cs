using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserGameController : ControllerBase
    {
        private readonly ILogger<MariaDbContext> _logger;
        private readonly MariaDbContext _mariaDb;
        public UserGameController(ILogger<MariaDbContext> logger, MariaDbContext context)
        {
            _logger = logger;
            _mariaDb = context;
        }

        [HttpPost("purchase")]
        public IActionResult Purchase([FromForm] Purchase purchase)
        {
            try
            {
                UserGame userGame = new()
                {
                    GameId = purchase.GameId,
                    UserId = purchase.UserId
                };

                if (_mariaDb.UserGames.Where(ug => ug.GameId == purchase.GameId
                && ug.UserId == purchase.UserId).Any())
                    return BadRequest(new
                    {
                        Message = "User already owns this game!"
                    });
                else
                {
                    _mariaDb.UserGames.Add(userGame);
                    _mariaDb.SaveChanges();
                    return Ok(purchase);
                }
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
