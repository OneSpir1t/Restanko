using System;
using System.Collections.Generic;

namespace Restanko.Entities
{
    public partial class Repairtype
    {
        public Repairtype()
        {
            Repairs = new HashSet<Repair>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Duration { get; set; }
        public int Cost { get; set; }

        public virtual ICollection<Repair> Repairs { get; set; }
    }
}
