using Microsoft.EntityFrameworkCore;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Services
{
    public class DeveloperService : IDeveloperService
    {
        MariaDbContext _mariaDb;
        public DeveloperService(MariaDbContext mariaDb)
        {
            _mariaDb = mariaDb;
        }

        public async Task<int> AddDeveloper(NewDevTeam newDeveloperData)
        {
            DevTeam newDeveloper = new()
            {
                DevTeam_name = newDeveloperData.Name
            };

            if (await _mariaDb.DevTeams.Where(developer => 
                developer.DevTeam_name == newDeveloper.DevTeam_name).AnyAsync())
            {
                return 0;
            }
            else
            {
                await _mariaDb.DevTeams.AddAsync(newDeveloper);
                await _mariaDb.SaveChangesAsync();
                return 1;
            }
        }

        public async Task<int> DeleteDeveloper(int developerId)
        {
            var developer = await _mariaDb.DevTeams
                .FirstOrDefaultAsync(developer => developer.Id == developerId);
            if (developer != null)
            {
                _mariaDb.DevTeams.Remove(developer);
                await _mariaDb.SaveChangesAsync();
                return 1;
            }
            else return 0;
        }

        public async Task<DevTeam> GetDeveloper(int developerId)
        {
            var developer = await _mariaDb.DevTeams
                .FirstOrDefaultAsync(developer => developer.Id == developerId);

            if (developer != null) return developer;
            else return null;
        }

        public async Task<List<DevTeam>> GetAllDevelopers()
        {
            var developers = await _mariaDb.DevTeams.ToListAsync();

            if (developers != null) return developers;
            else return null;
        }

        public async Task<int> UpdateDeveloper(DevTeam updateDeveloperData)
        {
            var developer = await _mariaDb.DevTeams
                .FirstOrDefaultAsync(developer => 
                developer.Id == updateDeveloperData.Id);

            if (developer != null)
            {
                developer.DevTeam_name = updateDeveloperData.DevTeam_name;
                await _mariaDb.SaveChangesAsync();
                return 1;
            }
            else return 0;
        }
    }
}
