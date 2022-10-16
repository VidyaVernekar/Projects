using System;
using System.Collections.Generic;

namespace PatientDetails.Models
{
    public partial class Login
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
    }
}
