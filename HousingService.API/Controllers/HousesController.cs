using HousingService.API.Data;
using HousingService.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HousingService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HousesController : ControllerBase
    {
        private HousingServiceDbContext _dbContext;
        public HousesController(HousingServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("/cities/{city_id}/houses")]
        public async Task<ActionResult<IEnumerable<HousesDTO>>> GetHousesByCity(int city_id)
        {
            try
            {
                var cityExists = await _dbContext.Cities.AnyAsync(c => c.Id == city_id);
                if (!cityExists)
                {
                    return NotFound($"City with ID {city_id} not found.");
                }

                var houses = await _dbContext.Houses
                                         .Include(h => h.Street)
                                             .ThenInclude(s => s.City)
                                         .Include(h => h.Apartments) 
                                         .Where(h => h.Street.CityId == city_id)
                                         .ToListAsync();

                if (houses == null || !houses.Any())
                {
                    return Ok(new List<HousesDTO>());
                }

                var houseDTOs = houses.Select(h => new HousesDTO(h)).ToList();
                return Ok(houseDTOs);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("/streets/{street_id}/houses")]
        public async Task<ActionResult<IEnumerable<HousesDTO>>> GetHousesByStreet(int street_id)
        {
            try
            {
                var streetExists = await _dbContext.Streets.AnyAsync(s => s.Id == street_id);
                if (!streetExists)
                {
                    return NotFound($"Street with ID {street_id} not found.");
                }
                var houses = await _dbContext.Houses
                                         .Include(h => h.Street)
                                             .ThenInclude(s => s.City)
                                         .Include(h => h.Apartments) 
                                         .Where(h => h.StreetId == street_id)
                                         .ToListAsync();

                if (houses == null || !houses.Any())
                {
                    return Ok(new List<HousesDTO>());
                }

                var houseDTOs = houses.Select(h => new HousesDTO(h)).ToList();
                return Ok(houseDTOs);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
