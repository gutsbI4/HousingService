using HousingService.API.Data;
using HousingService.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HousingService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StreetsController : ControllerBase
    {
        private HousingServiceDbContext _dbContext;
        public StreetsController(HousingServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("/cities/{city_id}/streets")]
        public async Task<ActionResult<IEnumerable<StreetsDTO>>> GetStreetsByCity(int city_id)
        {
            try
            {
                var cityExists = await _dbContext.Cities.AnyAsync(c => c.Id == city_id);
                if (!cityExists)
                {
                    return NotFound($"City with ID {city_id} not found.");
                }

                var streets = await _dbContext.Streets
                                            .Where(s => s.CityId == city_id)
                                            .Include(s => s.Houses)
                                            .ToListAsync();

                if (streets == null || !streets.Any())
                {
                    return Ok(new List<StreetsDTO>());
                }

                var streetDTOs = streets.Select(s => new StreetsDTO(s)).ToList();

                return Ok(streetDTOs);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
