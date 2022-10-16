using System;
using System.Collections.Generic;

namespace PatientDetails.Models
{
    public partial class Drug
    {
        public Drug()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        public int DrugId { get; set; }
        public string? DrugName { get; set; }
        public string? DrugIDformat { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
