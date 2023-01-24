using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.RequestClasses;

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
        public IActionResult Purchase([FromForm] PurchaseRequest purchase)
        {
            try
            {
                UserGame userGame = new()
                {
                    Game_Id = purchase.GameId,
                    User_Id = purchase.UserId
                };

                if (_mariaDb.UserGames.Where(ug => ug.Game_Id == purchase.GameId
                && ug.User_Id == purchase.UserId).Any())
                    return BadRequest();
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
