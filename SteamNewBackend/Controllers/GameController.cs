﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models.RequestClasses;
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
        public IActionResult AddGame([FromForm] NewGameRequest game)
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
                    return Ok(game);
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

        [HttpGet("getOwned/{id}")]
        public IActionResult GetOwnedGames([FromRoute] int id)
        {
            try
            {
                var games = _mariaDb.UserGames.Include(x => x.Game)
                    .Where(ug => ug.User_Id == id).Select(x => x.Game).ToList();

                if (games != null) return NotFound();
                else return Ok(games);
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
        public IActionResult UpdateGame([FromForm] UpdateGameRequest newGame)
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
