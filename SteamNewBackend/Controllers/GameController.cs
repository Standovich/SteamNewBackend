using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models.Dto;
using SteamNewBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Authorize]
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
        public IActionResult AddGame([FromForm] NewGame game)
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
                    return BadRequest();
                else
                {
                    _mariaDb.Games.Add(newGame);
                    _mariaDb.SaveChanges();
                    return Ok(new
                    {
                        Message = "Game successfully created!"
                    });
                }
            }
            catch
            {
                return NotFound();
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
                return NotFound();
            }
        }

        [HttpGet("getGames")]
        public IActionResult GetGames()
        {
            try
            {
                var games = _mariaDb.Games.ToList();

                if (games != null) return Ok(games);
                else return BadRequest(new
                {
                    Message = "There are no games."
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getGamesByDev/{id}")]
        public IActionResult GetGamesByDev([FromRoute] int id)
        {
            try
            {
                var games = _mariaDb.Games.Where(g => g.DevTeam_Id == id).ToList();

                if (games != null) return Ok(games);
                else return BadRequest(new
                {
                    Message = "There are no games."
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getOwned/{username}")]
        public IActionResult GetOwnedGames([FromRoute] string username)
        {
            try
            {
                var user = _mariaDb.Users.FirstOrDefault(u => u.User_Name == username);
                var games = _mariaDb.UserGames.Include(g => g.Game)
                    .Where(u => u.UserId == user.Id).Select(g => g.Game).ToArray();

                if (games != null) return Ok(games);
                else return NotFound(new
                {
                    Message = "User owns no games!"
                });
            }
            catch
            {
                return NotFound();
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
                    return Ok(new
                    {
                        Message = "Game successfully deleted!"
                    });
                }
                else return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("updateGame")]
        public IActionResult UpdateGame([FromForm] UpdateGame newGame)
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
                    return Ok(new
                    {
                        Message = "Game successfully updated!"
                    });
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
