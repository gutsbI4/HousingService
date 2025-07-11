using System;
using System.Collections.Generic;

namespace HousingService.API.Models
{
    public partial class Cities
    {
        public Cities()
        {
            Streets = new HashSet<Streets>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Streets> Streets { get; set; }
    }
}
