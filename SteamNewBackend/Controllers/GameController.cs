using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models.RequestClasses;
using SteamNewBackend.Models;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<MariaDbContext> _logger;
        private readonly MariaDbContext _mariaDb;
        public GameController(ILogger<MariaDbContext> logger, MariaDbContext context)
        {
            _logger = logger;
            _mariaDb = context;
        }

        [HttpPost("addGame")]
        public IActionResult AddGame([FromForm] FormGameRequest game)
        {
            try
            {
                Game newGame = new()
                {
                    Game_Name = game.Name,
                    Game_RelDate = game.ReleaseDate,
                    Game_Description = game.Description,
                    Game_Price = game.Price,
                    DevTeam_Id = game.DevTeamId
                };

                if (_mariaDb.Games.Where(g => g.Game_Name == newGame.Game_Name).Any())
                    return BadRequest("Game already exists!");
                else
                {
                    _mariaDb.Games.Add(newGame);
                    _mariaDb.SaveChanges();
                    return Ok(game);
                }
            }
            catch
            {
                return NotFound("Database couldn't be reached.");
            }
        }

        [HttpGet("getGame/{id}")]
        public IActionResult GetGame([FromRoute] int id)
        {
            try
            {
                var game = _mariaDb.Games.FirstOrDefault(g => g.Id == id);
                if (game != null) return Ok(game);
                else return NotFound();
            }
            catch
            {
                return NotFound("Database couldn't be reached.");
            }
        }

        [HttpGet("getGames")]
        public IActionResult GetGames()
        {
            try
            {
                var games = _mariaDb.Games.ToList();

                if (games != null) return Ok(games);
                else return NotFound();
            }
            catch
            {
                return NotFound("Database couldn't be reached.");
            }
        }

        [HttpDelete("deleteGame/{id}")]
        public IActionResult DeleteGame([FromRoute] int id)
        {
            try
            {
                var game = _mariaDb.Games.FirstOrDefault(g => g.Id == id);
                if (game != null)
                {
                    _mariaDb.Games.Remove(game);
                    _mariaDb.SaveChanges();
                    return Ok(game);
                }
                else return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("updateGame")]
        public IActionResult UpdateGame([FromForm] Game newGame)
        {
            try
            {
                var game = _mariaDb.Games.FirstOrDefault(g => g.Id == newGame.Id);
                if (game != null)
                {
                    game.Game_Name = newGame.Game_Name;
                    game.Game_RelDate = newGame.Game_RelDate;
                    game.Game_Description = newGame.Game_Description;
                    game.Game_Price = newGame.Game_Price;
                    game.DevTeam_Id = newGame.DevTeam_Id;
                    _mariaDb.SaveChanges();
                    return Ok(newGame);
                }
                else return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
