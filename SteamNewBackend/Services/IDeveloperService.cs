using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Services
{
    public interface IDeveloperService
    {
        Task<int> AddDeveloper(NewDevTeam dev);
        Task<DevTeam> GetDeveloper(int id);
        Task<List<DevTeam>> GetAllDevelopers();
        Task<int> DeleteDeveloper(int id);
        Task<int> UpdateDeveloper(DevTeam newDev);
    }
}
