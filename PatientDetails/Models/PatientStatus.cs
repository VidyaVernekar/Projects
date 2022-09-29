using System;
using System.Collections.Generic;

namespace PatientDetails.Models
{
    public partial class PatientStatus
    {
        public PatientStatus()
        {
            PatientInfos = new HashSet<PatientInfo>();
        }

        public int StatusId { get; set; }
        public string? StatusName { get; set; }

        public virtual ICollection<PatientInfo> PatientInfos { get; set; }
    }
}
