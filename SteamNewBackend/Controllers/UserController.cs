using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("createUser")]
        public IActionResult Create([FromForm] FormUserRequest user)
        {
            try
            {
                User newUser = new User()
                {
                    User_Name = user.Username,
                    User_Passwd = user.Passwd
                };

                if (_mariaDb.Users.Where(u => u.User_Name == newUser.User_Name).Any())
                    return BadRequest("User already exists!");
                else
                {
                    _mariaDb.Users.Add(newUser);
                    _mariaDb.SaveChanges();
                    return Ok(user);
                }
            }
            catch
            {
                return NotFound("Database couldn't be reached.");
            }
        }

        [HttpGet("login")]
        public IActionResult Login([FromForm] FormUserRequest request)
        {
            try
            {
                User user = _mariaDb.Users.Where(u => u.User_Name == request.Username).FirstOrDefault();
                if (user != null)
                {
                    if (user.User_Passwd == request.Passwd)
                        return Ok(user);
                    else return NotFound("Entered password is wrong!");

                }
                else return NotFound("User doesn't exist!");
            }
            catch
            {
                return NotFound("Database couldn't be reached.");
            }
        }

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
                return NotFound("Database couldn't be reached.");
            }
        }

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
                return NotFound("Database couldn't be reached.");
            }
        }

        //[HttpPatch]
        //public IResult Update()
        //{
        //    return View();
        //}

        //[HttpDelete]
        //public IResult Delete()
        //{
        //    return View();
        //}
    }
}
