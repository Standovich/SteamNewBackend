using Microsoft.EntityFrameworkCore;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Services
{
    public class GameService : IGameService
    {
        MariaDbContext _mariaDb;
        public GameService(MariaDbContext mariaDb)
        {
            _mariaDb = mariaDb;
        }

        public async Task<int> AddGame(NewGame newGameData)
        {
            Game newGame = new()
            {
                Game_Name = newGameData.Name,
                Game_RelDate = newGameData.ReleaseDate,
                Game_Description = newGameData.Description,
                Game_Price = newGameData.Price,
                DevTeam_Id = newGameData.DevTeamId
            };

            if (await _mariaDb.Games.Where(game => 
            game.Game_Name == newGame.Game_Name).AnyAsync())
                return 2;

            await _mariaDb.Games.AddAsync(newGame);
            await _mariaDb.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DeleteGame(int gameId)
        {
            var game = await _mariaDb.Games.FirstOrDefaultAsync(game => game.Id == gameId);
            if (game == null) return 0;

            _mariaDb.Games.Remove(game);
            await _mariaDb.SaveChangesAsync();
            return 1;
        }

        public async Task<List<Game>> GetAllGames()
        {
            var games = await _mariaDb.Games.ToListAsync();
            if (games == null) return null;
            return games;
        }

        public async Task<Game> GetGame(int gameId)
        {
            var game = await _mariaDb.Games.FirstOrDefaultAsync(game => game.Id == gameId);
            if (game == null) return null;
            return game;
        }

        public async Task<List<Game>> GetGamesByDev(int developerId)
        {
            var games = await _mariaDb.Games.Where(game => 
            game.DevTeam_Id == developerId).ToListAsync();
            if (games == null) return null;
            return games;
        }

        public async Task<List<Game>> GetOwnedGames(string username)
        {
            var user = await _mariaDb.Users
                .FirstOrDefaultAsync(user => user.User_Name == username);
            var games = await _mariaDb.UserGames.Include(game => game.Game)
                .Where(user => user.UserId == user.Id).Select(game => game.Game)
                .ToListAsync();

            if (games == null) return null;
            return games;
        }

        public async Task<int> UpdateGame(UpdateGame updateGameData)
        {
            var game = await _mariaDb.Games
                .FirstOrDefaultAsync(game => game.Id == updateGameData.Id);
            if (game == null) return 0;

            game.Game_Name = updateGameData.Game_Name;
            game.Game_RelDate = updateGameData.Game_RelDate;
            game.Game_Description = updateGameData.Game_Description;
            game.Game_Price = updateGameData.Game_Price;
            game.DevTeam_Id = updateGameData.DevTeam_Id;
            await _mariaDb.SaveChangesAsync();
            return 1;
        }
    }
}
