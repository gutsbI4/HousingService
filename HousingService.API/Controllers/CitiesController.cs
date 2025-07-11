using HousingService.API.Data;
using HousingService.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HousingService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private HousingServiceDbContext _context;
        public CitiesController(HousingServiceDbContext housingServiceDbContext)
        {
            _context = housingServiceDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitiesDTO>>> GetCities()
        {
            try
            {
                var cities = await _context.Cities
                                         .Include(c => c.Streets)
                                          .ThenInclude(s => s.Houses)
                                         .ToListAsync();

                if (cities == null || !cities.Any())
                {
                    return NotFound("No cities found.");
                }

                return Ok(cities.ConvertAll(x => new CitiesDTO(x)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
