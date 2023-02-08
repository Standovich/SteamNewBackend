using Microsoft.EntityFrameworkCore;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Services
{
    public class UserGameService : IUserGameService
    {
        MariaDbContext _mariaDb;
        public UserGameService(MariaDbContext mariaDb)
        {
            _mariaDb = mariaDb;
        }

        public async Task<int> Purchase(Purchase purchase)
        {
            UserGame newUserGame = new()
            {
                GameId = purchase.GameId,
                UserId = purchase.UserId
            };

            if (await _mariaDb.UserGames.Where(userGame => userGame.GameId == purchase.GameId
            && userGame.UserId == purchase.UserId).AnyAsync())
                return 2;

            await _mariaDb.UserGames.AddAsync(newUserGame);
            await _mariaDb.SaveChangesAsync();
            return 1;
        }
    }
}
