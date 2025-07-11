using System;
using System.Collections.Generic;

namespace HousingService.API.Models
{
    public partial class Apartments
    {
        public int Id { get; set; }
        public double Area { get; set; }
        public int HouseId { get; set; }

        public virtual Houses House { get; set; } = null!;
    }
}
