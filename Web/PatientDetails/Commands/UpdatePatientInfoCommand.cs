using MediatR;
using PatientDetails.Models;

namespace PatientDetails.Commands
{
    public record UpdatePatientInfoCommand(int id,PatientInfo patientInfo) : IRequest<PatientInfo>;

    public record IsExistsPatientInfoCommand(int id) : IRequest<bool>;

}
