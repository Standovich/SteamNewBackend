using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.RequestClasses;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Authorize]
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
        public IActionResult AddDeveloper([FromForm] FormDevTeamRequest dev)
        {
            try
            {
                DevTeam newDev = new()
                {
                    DevTeam_name = dev.Name
                };

                if (_mariaDb.DevTeams.Where(d => d.DevTeam_name == newDev.DevTeam_name).Any())
                    return BadRequest();
                else
                {
                    _mariaDb.DevTeams.Add(newDev);
                    _mariaDb.SaveChanges();
                    return Ok(dev);
                }
            }
            catch
            {
                return NotFound();
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
                return NotFound();
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
                return NotFound();
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

        [HttpPut("updateDeveloper")]
        public IActionResult UpdateDeveloper([FromForm] DevTeam newDev)
        {
            try
            {
                var dev = _mariaDb.DevTeams.FirstOrDefault(d => d.Id == newDev.Id);
                if (dev != null)
                {
                    dev.DevTeam_name = newDev.DevTeam_name;
                    _mariaDb.SaveChanges();
                    return Ok(newDev);
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
