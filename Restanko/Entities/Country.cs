using System;
using System.Collections.Generic;

namespace Restanko.Entities
{
    public partial class Country
    {
        public Country()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Mark> Marks { get; set; }
    }
}
