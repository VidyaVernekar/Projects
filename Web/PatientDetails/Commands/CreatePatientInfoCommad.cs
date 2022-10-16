using MediatR;
using PatientDetails.Models;


namespace PatientDetails.Commands
{
    public record CreatePatientInfoCommand(PatientInfo patientInfo) : IRequest<PatientInfo>;

    public record CreatePatientInfoBySpreadSheetCommand(List<PatientDetailsODSheet> patientOD) : IRequest<List<PatientDetailsODSheet>>;

}
