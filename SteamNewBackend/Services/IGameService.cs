using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Services
{
    public interface IGameService
    {
        Task<int> AddGame(NewGame game);
        Task<Game> GetGame(int id);
        Task<List<Game>> GetAllGames();
        Task<List<Game>> GetGamesByDev(int id);
        Task<List<Game>> GetOwnedGames(string username);
        Task<int> DeleteGame(int id);
        Task<int> UpdateGame(UpdateGame newGame);
    }
}
