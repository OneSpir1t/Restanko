using System;
using System.Collections.Generic;

namespace Restanko.Entities
{
    public partial class Mark
    {
        public Mark()
        {
            Machines = new HashSet<Machine>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string? Name { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Machine> Machines { get; set; }
    }
}
