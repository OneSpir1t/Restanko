using System;
using System.Collections.Generic;

namespace Restanko.Entities
{
    public partial class Machinetype
    {
        public Machinetype()
        {
            Machines = new HashSet<Machine>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Machine> Machines { get; set; }
    }
}
