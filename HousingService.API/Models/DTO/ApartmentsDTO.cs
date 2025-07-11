namespace HousingService.API.Models.DTO
{
    public class ApartmentsDTO
    {
        public ApartmentsDTO(Apartments apartments)
        {
            Number = apartments.Id;
            Area = apartments.Area;
        }
        public int Number {  get; set; }
        public double Area {  get; set; }
    }
}
