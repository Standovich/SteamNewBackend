using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Authentication;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.DbRequestClasses;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<MariaDbContext> _logger;
        private readonly MariaDbContext _mariaDb;
        public UserController(ILogger<MariaDbContext> logger, MariaDbContext context)
        {
            _logger = logger;
            _mariaDb = context;
        }

        [HttpPost("addUser")]
        public IActionResult AddUser([FromForm] FormUserRequest user)
        {
            try
            {
                if (user == null) return BadRequest();

                User newUser = new()
                {
                    User_Name = user.Username,
                    User_Password = PasswordHasher.HashPassword(user.Password),
                    Role = user.Role,
                    DevTeam_Id = user.DevTeam
                };

                if (_mariaDb.Users.Where(u => u.User_Name == newUser.User_Name).Any())
                    return BadRequest(new
                    {
                        Message = "User already exists!"
                    });
                else
                {
                    _mariaDb.Users.Add(newUser);
                    _mariaDb.SaveChanges();
                    return Ok(user);
                }
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] FormUserRequest request)
        {
            try
            {
                var user = _mariaDb.Users.FirstOrDefault(u => u.User_Name == request.Username);
                if (user != null)
                {
                    if (PasswordHasher.VerifyPassword(request.Password, user.User_Password))
                    {
                        user.Token = JwtHandler.CreateJwt(user);
                        return Ok(new
                        {
                            Token = user.Token
                        });
                    }
                    else return BadRequest(new
                    {
                        Message = "Wrong password!"
                    });

                }
                else return BadRequest(new
                {
                    Message = "User not found!"
                });
            }
            catch
            {
                return NotFound(new
                {
                    Message = "Login failed!"
                });
            }
        }

        [Authorize]
        [HttpGet("getUser/{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _mariaDb.Users.FirstOrDefault(u => u.Id == id);

                if (user != null) return Ok(user);
                else return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _mariaDb.Users.ToList();

                if (users != null) return Ok(users);
                else return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("deleteUser/{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            try
            {
                var user = _mariaDb.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    _mariaDb.Users.Remove(user);
                    _mariaDb.SaveChanges();
                    return Ok(user);
                }
                else return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPut("updateUser")]
        public IActionResult UpdateUser([FromForm] User newUser)
        {
            try
            {
                var user = _mariaDb.Users.FirstOrDefault(u => u.Id == newUser.Id);
                if (user != null)
                {
                    user.User_Name = newUser.User_Name;
                    user.User_Password = newUser.User_Password;
                    user.Role = newUser.Role;
                    user.DevTeam_Id = newUser.DevTeam_Id;
                    _mariaDb.SaveChanges();
                    return Ok(newUser);
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
