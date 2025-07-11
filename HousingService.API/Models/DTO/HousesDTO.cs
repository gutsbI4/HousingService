namespace HousingService.API.Models.DTO
{
    public class HousesDTO
    {
        public HousesDTO(Houses house)
        {
            Id = house.Id;
            FullAddress = $"г. {house.Street?.City?.Name}, ул. {house.Street?.Name}, д. {house.Number}";
            CountOfApartments = house.Apartments.Count;
            Apartments = house.Apartments?.Select(a => new ApartmentsDTO(a)).ToList() ?? new List<ApartmentsDTO>();
        }
        public int Id {  get; set; }
        public string FullAddress { get; set; } = null!;
        public int CountOfApartments {  get; set; }
        public List<ApartmentsDTO> Apartments { get; set; } = new List<ApartmentsDTO>();
    }
}
