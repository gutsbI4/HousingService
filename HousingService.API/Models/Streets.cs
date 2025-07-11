using System;
using System.Collections.Generic;

namespace HousingService.API.Models
{
    public partial class Streets
    {
        public Streets()
        {
            Houses = new HashSet<Houses>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CityId { get; set; }

        public virtual Cities City { get; set; } = null!;
        public virtual ICollection<Houses> Houses { get; set; }
    }
}
