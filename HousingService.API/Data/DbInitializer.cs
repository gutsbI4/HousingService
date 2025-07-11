using HousingService.API.Models;

namespace HousingService.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HousingServiceDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cities.Any())
            {
                return;
            }


            var cities = new Cities[]
            {
                new Cities { Name = "Ярославль" },
                new Cities {Name = "Москва" },
                new Cities {  Name = "Новосибирск" },
                new Cities { Name = "Архангельск" },
                new Cities { Name = "Курск" }
            };
            context.Cities.AddRange(cities);
            context.SaveChanges();

            var streets = new Streets[]
            {
                new Streets { Name = "Пушкина", CityId = 1 },
                new Streets {  Name = "Пожарского", CityId = 1 },
                new Streets { Name = "Ленина", CityId = 2 },
                new Streets { Name = "Гоголя", CityId = 3 },
                new Streets { Name = "Минина", CityId = 4 },
                new Streets { Name = "Космонавтов", CityId = 5 }
            };
            context.Streets.AddRange(streets);
            context.SaveChanges();

            var houses = new Houses[]
            {
                new Houses { Number = "12", StreetId = 1 },
                new Houses { Number = "2", StreetId = 2 },
                new Houses { Number = "45А", StreetId = 3 },
                new Houses { Number = "1А", StreetId = 4 },
                new Houses { Number = "2В", StreetId = 5 }
            };
            context.Houses.AddRange(houses);
            context.SaveChanges();

            // 4. Квартиры
            var apartments = new Apartments[]
            {
                new Apartments { Id = 6, Area = 67.88, HouseId = 5 },
                new Apartments { Id = 12, Area = 47.43, HouseId = 2 },
                new Apartments { Id = 21, Area = 89.54, HouseId = 4 },
                new Apartments { Id = 23, Area = 23, HouseId = 1 },
                new Apartments { Id = 56, Area = 34, HouseId = 3 }
            };
            context.Apartments.AddRange(apartments);
            context.SaveChanges();
        }
    }
}
