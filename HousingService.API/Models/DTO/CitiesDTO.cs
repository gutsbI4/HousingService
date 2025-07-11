namespace HousingService.API.Models.DTO
{
    public class CitiesDTO
    {
        public CitiesDTO(Cities cities)
        {
            Id = cities.Id;
            Name = cities.Name;
            CountOfHouses = cities.Streets.SelectMany(street => street.Houses).Count();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountOfHouses {  get; set; }

    }
}
