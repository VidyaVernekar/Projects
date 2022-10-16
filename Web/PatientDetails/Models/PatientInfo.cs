using System;
using System.Collections.Generic;

namespace PatientDetails.Models
{
    public partial class PatientInfo
    {
        public PatientInfo()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        public int Id { get; set; }
        public DateTime? Dob { get; set; }
        public string? EmailId { get; set; }
        public string? Phone { get; set; }
        public int? StatusId { get; set; }
        public string? Address { get; set; }
        public string? PatientName { get; set; }
        public virtual PatientStatus? Status { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
