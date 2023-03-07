using System;
using System.Collections.Generic;

namespace Restanko.Entities
{
    public partial class Machine
    {
        public Machine()
        {
            Repairs = new HashSet<Repair>();
        }

        public int Id { get; set; }
        public int MarkId { get; set; }
        public int MachineTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public int YearOfManufacture { get; set; }

        public virtual Machinetype MachineType { get; set; } = null!;
        public virtual Mark Mark { get; set; } = null!;
        public virtual ICollection<Repair> Repairs { get; set; }
    }
}
