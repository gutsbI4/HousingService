using System;
using System.Collections.Generic;

namespace HousingService.API.Models
{
    public partial class Houses
    {
        public Houses()
        {
            Apartments = new HashSet<Apartments>();
        }

        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public int StreetId { get; set; }

        public virtual Streets Street { get; set; } = null!;
        public virtual ICollection<Apartments> Apartments { get; set; }
    }
}
