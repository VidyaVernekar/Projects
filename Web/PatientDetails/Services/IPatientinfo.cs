using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientDetails.Models;

namespace PatientDetails.Services
{
    public interface IPatientinfo
    {
        Task<ActionResult<IEnumerable<PatientInfo>>> GetPatientInfos();
        Task<PatientInfo> GetPatientInfo(int id);
        void PutPatientInfo(int id, PatientInfo patientInfo);
        void PostPatientInfo(PatientInfo patientInfo);
        bool PatientInfoExists(int id);
        Task<Drug> DrugExists(string drugname, string drugIdFormat);
        Task<PatientInfo> PatientExists(string email, string name);
        void PostDrugInfo(Drug drug);
        void PostPreInfo(Prescription pre);
        void DeletePatientInfo(PatientInfo patientInfo);
        Task<List<PatientDetailsODSheet>> PatientSpreedShetSave(List<PatientDetailsODSheet> patientsList);
    }
}
