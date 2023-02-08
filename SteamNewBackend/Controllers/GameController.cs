using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models.Dto;
using SteamNewBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SteamNewBackend.Services;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly MariaDbContext _mariaDb;
        public GameController(IGameService gameService, MariaDbContext context)
        {
            _gameService = gameService;
            _mariaDb = context;
        }

        [HttpPost("addGame")]
        public async Task<IActionResult> AddGame([FromForm] NewGame newGameData)
        {
            try
            {
                if (newGameData == null) return BadRequest();

                var result = await _gameService.AddGame(newGameData);
                if(result == 2) return BadRequest();
                return Ok(new
                {
                    Message = "Game successfully created!"
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getGame/{gameId}")]
        public async Task<IActionResult> GetGame([FromRoute] int gameId)
        {
            try
            {
                var game = await _gameService.GetGame(gameId);
                if(game == null) return NotFound();
                return Ok(game);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getGames")]
        public async Task<IActionResult> GetGames()
        {
            try
            {
                var games = await _gameService.GetAllGames();
                if(games == null) return NotFound();
                return Ok(games);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getGamesByDev/{developerId}")]
        public async Task<IActionResult> GetGamesByDev([FromRoute] int developerId)
        {
            try
            {
                var games = await _gameService.GetGamesByDev(developerId);
                if(games == null) return NotFound();
                return Ok(games);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getOwned/{username}")]
        public async Task<IActionResult> GetOwnedGames([FromRoute] string username)
        {
            try
            {
                var games = await _gameService.GetOwnedGames(username);
                if(games == null) return NotFound();
                return Ok(games);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("deleteGame/{gameId}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int gameId)
        {
            try
            {
                var result = await _gameService.DeleteGame(gameId);
                if(result == 0) return NotFound();
                return Ok(new
                {
                    Message = "Game successfully deleted!"
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("updateGame")]
        public async Task<IActionResult> UpdateGame([FromForm] UpdateGame updateGameData)
        {
            try
            {
                if(updateGameData == null) return NotFound();

                var result = await _gameService.UpdateGame(updateGameData);
                if (result == 0) return NotFound();
                return Ok(new
                {
                    Message = "Game successfully updated!"
                });
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
