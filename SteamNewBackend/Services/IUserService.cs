using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Services
{
    public interface IUserService
    {
        Task<int> AddUser(NewUser user);
        Task<string> Login(LoginUser request);
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string username);
        Task<List<User>> GetAllUsers();
        Task<int> DeleteUser(int id);
        Task<int> UpdateUser(UpdateUser newUser);
        Task<string> UpdateUserAuthorized(UpdateUserAuth newUser);
    }
}
