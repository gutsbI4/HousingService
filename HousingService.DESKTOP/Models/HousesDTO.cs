namespace HousingService.DESKTOP.Models

{
    public class HousesDTO
    {
        public int Id {  get; set; }
        public string FullAddress { get; set; } = null!;
        public int CountOfApartments {  get; set; }
        public List<ApartmentsDTO> Apartments { get; set; } = new List<ApartmentsDTO>();
    }
}
