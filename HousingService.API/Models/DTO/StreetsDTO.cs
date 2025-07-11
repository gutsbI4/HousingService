using System.IO;

namespace HousingService.API.Models.DTO
{
    public class StreetsDTO
    {
        public StreetsDTO(Streets street)
        {
            Id = street.Id;
            Name = street.Name;
            CountOfHouses = street.Houses.Count;
        }
        public int Id {  get; set; }
        public string Name { get; set; } = null!;
        public int CountOfHouses {  get; set; }
    }
}
