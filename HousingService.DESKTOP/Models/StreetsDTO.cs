using System.IO;

namespace HousingService.DESKTOP.Models
{
    public class StreetsDTO
    {
        public int Id {  get; set; }
        public string Name { get; set; } = null!;
        public int CountOfHouses {  get; set; }
    }
}
