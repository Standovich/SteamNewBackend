using Microsoft.EntityFrameworkCore;
using SteamNewBackend.Authentication;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Services
{
    public class UserService : IUserService
    {
        MariaDbContext _mariaDb;
        public UserService(MariaDbContext mariaDb)
        {
            _mariaDb = mariaDb;
        }

        public async Task<int> AddUser(NewUser newUserData)
        {
            User newUser = new()
            {
                User_Name = newUserData.Username,
                User_Password = PasswordHasher.HashPassword(newUserData.Password),
                Role = newUserData.Role,
                DevTeam_Id = newUserData.DevTeam
            };

            if (await _mariaDb.Users.Where(user => user.User_Name == newUser.User_Name).AnyAsync())
                return 2;

            await _mariaDb.Users.AddAsync(newUser);
            await _mariaDb.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DeleteUser(int userId)
        {
            var user = await _mariaDb.Users.FirstOrDefaultAsync(user => user.Id == userId);
            if (user != null)
            {
                _mariaDb.Users.Remove(user);
                await _mariaDb.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _mariaDb.Users.ToListAsync();
            if (users == null) return null;
            return users;
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await _mariaDb.Users.FirstOrDefaultAsync(user => user.Id == userId);
            if (user == null) return null;
            return user;
        }

        public async Task<User> GetUserByName(string username)
        {
            var user = await _mariaDb.Users.FirstOrDefaultAsync(user => user.User_Name == username);
            if (user == null) return null;
            return user;
        }

        public async Task<string> Login(LoginUser loginRequest)
        {
            var user = await _mariaDb.Users
                .FirstOrDefaultAsync(user => user.User_Name == loginRequest.Username);
            if (user == null) return null;

            if (PasswordHasher.VerifyPassword(loginRequest.Password, user.User_Password))
            {
                user.Token = JwtHandler.CreateJwt(user);
                return user.Token;
            }
            return null;
        }

        public async Task<int> UpdateUser(UpdateUser updateUserData)
        {
            var user = await _mariaDb.Users.FirstOrDefaultAsync(user => user.Id == updateUserData.Id);
            if (user == null) return 0;

            user.User_Name = updateUserData.User_Name;
            if (updateUserData.User_Password != null)
                user.User_Password = PasswordHasher.HashPassword(updateUserData.User_Password);
            user.Role = updateUserData.Role;
            user.DevTeam_Id = updateUserData.DevTeam_Id;
            user.Token = JwtHandler.CreateJwt(user);
            _mariaDb.SaveChanges();
            return 1;
        }

        public async Task<string> UpdateUserAuthorized(UpdateUserAuth updateUserData)
        {
            var user = await _mariaDb.Users.FirstOrDefaultAsync(user => user.Id == updateUserData.Id);
            if (user == null) return null;

            if (PasswordHasher.VerifyPassword(updateUserData.User_OldPassword, user.User_Password))
            {
                user.User_Name = updateUserData.User_Name;
                if (updateUserData.User_NewPassword != null)
                    user.User_Password = PasswordHasher.HashPassword(updateUserData.User_NewPassword);
                user.Role = updateUserData.Role;
                user.DevTeam_Id = updateUserData.DevTeam_Id;
                user.Token = JwtHandler.CreateJwt(user);
                _mariaDb.SaveChanges();
                return user.Token;
            }
            return null;
        }
    }
}
