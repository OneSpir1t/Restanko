using System;
using System.Collections.Generic;

namespace Restanko.Entities
{
    public partial class Repair
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int RepairTypeId { get; set; }
        public DateOnly DateOfRepair { get; set; }
        public DateOnly DateEndOfRepair { get; set; }

        public virtual Machine Machine { get; set; } = null!;
        public virtual Repairtype RepairType { get; set; } = null!;
    }
}
