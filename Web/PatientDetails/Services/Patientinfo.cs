using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientDetails.Models;
using System.Text.RegularExpressions;

namespace PatientDetails.Services
{
    public class Patientinfo : IPatientinfo
    {
        private PatientDetailsContext db;
        public Patientinfo(PatientDetailsContext context)
        {
            db = context;
        }
        public async void DeletePatientInfo(PatientInfo patientInfo)
        {
            db.PatientInfos.Remove(patientInfo);
            await db.SaveChangesAsync();
        }

        public async Task<Drug> DrugExists(string drugname, string drugIdFormat)
        {
            return await db.Drugs.FirstOrDefaultAsync(u => u.DrugIDformat == drugIdFormat && u.DrugName == drugname);
        }

        public async Task<PatientInfo> GetPatientInfo(int id)
        {
            return await Task.FromResult(db.PatientInfos.Find(id));
        }

        public async Task<ActionResult<IEnumerable<PatientInfo>>> GetPatientInfos()
        {
            return await Task.FromResult(db.PatientInfos.ToList());
        }

        public async Task<PatientInfo> PatientExists(string email, string name)
        {
            return await db.PatientInfos.FirstOrDefaultAsync(u => u.EmailId == email && u.PatientName == name);
        }

        public bool PatientInfoExists(int id)
        {
            return db.PatientInfos.Any(e => e.Id == id);
        }

        public void PostDrugInfo(Drug drug)
        {
            db.Drugs.Add(drug);
            db.SaveChangesAsync();
        }

        public void PostPatientInfo(PatientInfo patientInfo)
        {
            db.PatientInfos.Add(patientInfo);
            db.SaveChangesAsync();
        }

        public void PostPreInfo(Prescription pre)
        {
            db.Prescriptions.Add(pre);
            db.SaveChangesAsync();

        }

        public void PutPatientInfo(int id, PatientInfo patientInfo)
        {
            db.Entry(patientInfo).State = EntityState.Modified;
            db.SaveChangesAsync();
        }
        public async Task<List<PatientDetailsODSheet>> PatientSpreedShetSave(List<PatientDetailsODSheet> patientsList)
        {
            String charPattern = @"\d{5}-\d{4}-\d{2}";
            List<PatientDetailsODSheet> formatNotCorrectList = new List<PatientDetailsODSheet>();
            foreach (var data in patientsList)
            {
                if (data.patientName == null || !Regex.IsMatch(data.patientName.Trim(), @"^[a-zA-Z\s*]{5,30}$"))
                {
                    formatNotCorrectList.Add(data);
                    continue;
                }
                else if (data.phone == null || !Regex.IsMatch(data.phone, @"^[0-9]{10,10}$"))
                {
                    formatNotCorrectList.Add(data);
                    continue;
                }
                else if (data.email == null || !Regex.IsMatch(data.email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    formatNotCorrectList.Add(data);
                    continue;
                }
                else if (data.drugId == null || !Regex.IsMatch(data.drugId, charPattern))
                {
                    formatNotCorrectList.Add(data);
                    continue;
                }
                else if (data.dob == null)
                {
                    formatNotCorrectList.Add(data);
                    continue;
                }
                else
                {
                    int? DrugId = null;
                    var DrugExist = await DrugExists(data.drugName, data.drugId);
                    if (DrugExist == null)
                    {
                        var drug = new Drug() { DrugName = data.drugName, DrugIDformat = data.drugId };
                        db.Drugs.Add(drug);
                        db.SaveChanges();
                        DrugId = drug.DrugId;
                    }
                    int? PatientId = null;
                    var PateintExist = await PatientExists(data.email, data.patientName);
                    if (PateintExist == null)
                    {
                        var petient = new PatientInfo()
                        {
                            PatientName = data.patientName,
                            Dob = DateTime.Parse(data.dob),
                            Phone = data.phone,
                            EmailId = data.email,
                            StatusId = 1,
                            Address = data.address
                        };
                        db.PatientInfos.Add(petient);
                        db.SaveChanges();
                        PatientId = petient.Id;
                    }
                    PatientId = PatientId != null ? PatientId : PateintExist.Id;
                    DrugId = DrugId != null ? DrugId : DrugExist.DrugId;
                    if (PatientId != null && DrugId != null)
                    {
                        var pre = new Prescription() { DrugId = DrugId, PatId = PatientId };
                        db.Prescriptions.Add(pre);
                        db.SaveChanges();
                    }
                }
            }
            return formatNotCorrectList;
        }
    }
}
