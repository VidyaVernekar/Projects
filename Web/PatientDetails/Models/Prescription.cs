using System;
using System.Collections.Generic;

namespace PatientDetails.Models
{
    public partial class Prescription
    {
        public int PreId { get; set; }
        public int? DrugId { get; set; }
        public int? PatId { get; set; }

        public virtual Drug? Drug { get; set; }
        public virtual PatientInfo? Pat { get; set; }
    }
}
