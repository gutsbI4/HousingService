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
                new Cities { Id = 1, Name = "Ярославль" },
                new Cities { Id = 2, Name = "Москва" },
                new Cities { Id = 3, Name = "Новосибирск" },
                new Cities { Id = 4, Name = "Архангельск" },
                new Cities { Id = 5, Name = "Курск" }
            };
            context.Cities.AddRange(cities);
            context.SaveChanges();

            var streets = new Streets[]
            {
                new Streets { Id = 1, Name = "Пушкина", CityId = 1 },
                new Streets { Id = 2, Name = "Пожарского", CityId = 1 },
                new Streets { Id = 3, Name = "Ленина", CityId = 2 },
                new Streets { Id = 4, Name = "Гоголя", CityId = 3 },
                new Streets { Id = 5, Name = "Минина", CityId = 4 },
                new Streets { Id = 6, Name = "Космонавтов", CityId = 5 }
            };
            context.Streets.AddRange(streets);
            context.SaveChanges();

            var houses = new Houses[]
            {
                new Houses { Id = 1, Number = "12", StreetId = 1 },
                new Houses { Id = 2, Number = "2", StreetId = 2 },
                new Houses { Id = 3, Number = "45А", StreetId = 3 },
                new Houses { Id = 4, Number = "1А", StreetId = 4 },
                new Houses { Id = 5, Number = "2В", StreetId = 5 }
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
