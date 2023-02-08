using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Authentication;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;
using SteamNewBackend.Services;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly MariaDbContext _mariaDb;
        public UserController(IUserService userService, MariaDbContext context)
        {
            _userService = userService;
            _mariaDb = context;
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromForm] NewUser newUserData)
        {
            try
            {
                if (newUserData == null) return BadRequest();

                var result = await _userService.AddUser(newUserData);

                if(result == 2) return BadRequest(new
                {
                    Message = "User already exists!"
                });
                return Ok(new
                {
                    Message = "You have successfully signed up!"
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginUser loginRequest)
        {
            try
            {
                if(loginRequest == null) return BadRequest();

                var result = await _userService.Login(loginRequest);
                if(result == null) return BadRequest(new
                {
                    Message = "Username or password is wrong!"
                });
                return Ok(new
                {
                    Token = result
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("getUserById/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if(user == null) return NotFound();
                return Ok(user);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("getUserByName/{username}")]
        public async Task<IActionResult> GetUserByName(string username)
        {
            try
            {
                var user = await _userService.GetUserByName(username);
                if(user == null) return NotFound();
                return Ok(user);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                if (users == null) return NotFound();
                return Ok(users);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            try
            {
                var result = await _userService.DeleteUser(userId);
                if(result == 0) return NotFound();
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUser updateUserData)
        {
            try
            {
                if(updateUserData == null) return BadRequest();

                var result = await _userService.UpdateUser(updateUserData);
                if(result == 0) return NotFound();
                return Ok(new
                {
                    Message = "User credentials successfully changed!"
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPut("updateUserAuth")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserAuth updateUserData)
        {
            try
            {
                if (updateUserData == null) return BadRequest();

                var result = await _userService.UpdateUserAuthorized(updateUserData);
                if(result == null) return NotFound();
                return Ok(new
                {
                    Message = "User credentials successfully changed!",
                    Token = result
                });
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
