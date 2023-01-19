using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.RequestClasses;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly ILogger<MariaDbContext> _logger;
        private readonly MariaDbContext _mariaDb;
        public DeveloperController(ILogger<MariaDbContext> logger, MariaDbContext context)
        {
            _logger = logger;
            _mariaDb = context;
        }

        [HttpPost("addDeveloper")]
        public IActionResult Add([FromForm] FormDevTeamRequest dev)
        {
            try
            {
                DevTeam newDev = new DevTeam()
                {
                    DevTeam_name = dev.Name
                };

                if (_mariaDb.DevTeams.Where(d => d.DevTeam_name == newDev.DevTeam_name).Any())
                    return BadRequest("Developer team already exists!");
                else
                {
                    _mariaDb.DevTeams.Add(newDev);
                    _mariaDb.SaveChanges();
                    return Ok(dev);
                }
            }
            catch
            {
                return NotFound("Database couldn't be reached.");
            }
        }

        [HttpGet("getDeveloper/{id}")]
        public IActionResult GetDeveloper(int id)
        {
            try
            {
                var dev = _mariaDb.DevTeams.FirstOrDefault(d => d.Id == id);

                if (dev != null) return Ok(dev);
                else return NotFound();
            }
            catch
            {
                return NotFound("Database couldn't be reached.");
            }
        }

        [HttpGet("getDevelopers")]
        public IActionResult GetDevelopers()
        {
            try
            {
                var devTeams = _mariaDb.DevTeams.ToList();
                
                if(devTeams != null) return Ok(devTeams);
                else return NotFound();
            }
            catch
            {
                return NotFound("Database couldn't be reached.");
            }
        }

        [HttpDelete("deleteDeveloper/{id}")]
        public IActionResult DeleteDeveloper([FromRoute] int id)
        {
            try
            {
                var dev = _mariaDb.DevTeams.FirstOrDefault(d => d.Id == id);
                if (dev != null)
                {
                    _mariaDb.DevTeams.Remove(dev);
                    _mariaDb.SaveChanges();
                    return Ok(dev);
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
