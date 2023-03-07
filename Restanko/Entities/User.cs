using System;
using System.Collections.Generic;

namespace Restanko.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Patryonomic { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }
}
