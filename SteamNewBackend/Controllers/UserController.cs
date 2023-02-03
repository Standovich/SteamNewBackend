using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Authentication;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

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
        public IActionResult AddUser([FromForm] NewUser user)
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
                    return Ok(new
                    {
                        Message = "You have successfully signed up!"
                    });
                }
            }
            catch
            {
                return NotFound(new
                {
                    Message = "Something went wrong!"
                });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginUser request)
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
                else return NotFound(new
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
        [HttpGet("getUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _mariaDb.Users.FirstOrDefault(u => u.Id == id);

                if (user != null) return Ok(user);
                else return BadRequest(new
                {
                    Message = "User doesn't exist!"
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("getUserByName/{username}")]
        public IActionResult GetUserByName(string username)
        {
            try
            {
                var user = _mariaDb.Users.FirstOrDefault(u => u.User_Name == username);
                if (user != null) return Ok(user);
                else return BadRequest(new
                {
                    Message = "User doesn't exist!"
                });
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
                else return BadRequest();
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
        public IActionResult UpdateUser([FromForm] UpdateUser newUser)
        {
            try
            {
                var user = _mariaDb.Users.FirstOrDefault(u => u.Id == newUser.Id);
                if (user != null)
                {
                    if (PasswordHasher.VerifyPassword(newUser.User_OldPassword, user.User_Password))
                    {
                        user.User_Name = newUser.User_Name;
                        user.User_Password = PasswordHasher.HashPassword(newUser.User_NewPassword);
                        user.Role = newUser.Role;
                        user.DevTeam_Id = newUser.DevTeam_Id;
                        user.Token = JwtHandler.CreateJwt(user);
                        _mariaDb.SaveChanges();
                        return Ok(new
                        {
                            Message = "User credentials successfully changed!",
                            Token = user.Token
                        });
                    }
                    else return BadRequest(new
                    {
                        Message = "Bad password!"
                    });
                }
                else return NotFound(new
                {
                    Message = "Something went wrong"
                });
            }
            catch
            {
                return NotFound(new
                {
                    Message = "spatne"
                });
            }
        }
    }
}
