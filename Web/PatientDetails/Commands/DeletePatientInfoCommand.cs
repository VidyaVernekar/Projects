using MediatR;
using PatientDetails.Models;

namespace PatientDetails.Commands
{
    public record DeletePatientInfoCommand(PatientInfo patientInfo) : IRequest;
}


