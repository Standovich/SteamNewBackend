using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;
using SteamNewBackend.Services;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;
        private readonly MariaDbContext _mariaDb;
        public DeveloperController(IDeveloperService developerService, MariaDbContext context)
        {
            _developerService = developerService;
            _mariaDb = context;
        }

        [Authorize]
        [HttpPost("addDeveloper")]
        public async Task<IActionResult> AddDeveloper([FromForm] NewDevTeam newDeveloper)
        {
            try
            {
                if (newDeveloper == null) return BadRequest();

                var result = await _developerService.AddDeveloper(newDeveloper);
                if (result == 0) return BadRequest();

                return Ok(new
                {
                    Message = "Developer successfully created!"
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getDeveloper/{developerId}")]
        public async Task<IActionResult> GetDeveloper(int developerId)
        {
            try
            {
                var developer = await _developerService.GetDeveloper(developerId);

                if (developer == null) return NotFound(new
                {
                    Message = "Users doesn't exist!"
                });
                return Ok(developer);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getDevelopers")]
        public async Task<IActionResult> GetAllDevelopers()
        {
            try
            {
                var developers = await _developerService.GetAllDevelopers();

                if (developers == null) return NotFound();
                return Ok(developers);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("deleteDeveloper/{developerId}")]
        public async Task<IActionResult> DeleteDeveloper([FromRoute] int developerId)
        {
            try
            {
                var result = await _developerService.DeleteDeveloper(developerId);
                if (result == 0) return NotFound();
                return Ok(new
                {
                    Message = "Developer successfully deleted!"
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPut("updateDeveloper")]
        public async Task<IActionResult> UpdateDeveloper([FromForm] DevTeam updateDeveloperData)
        {
            try
            {
                if (updateDeveloperData == null) return BadRequest();

                var result = await _developerService
                    .UpdateDeveloper(updateDeveloperData);
                if (result == 0) return NotFound();
                return Ok(new
                {
                    Message = "Developer successfully updated!"
                });
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
