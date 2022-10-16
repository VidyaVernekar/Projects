using MediatR;
using PatientDetails.Commands;
using PatientDetails.Models;
using PatientDetails.Services;

namespace PatientDetails.Handlers
{
    public class UpdatePatientInfoHandler : IRequestHandler<UpdatePatientInfoCommand, PatientInfo>,
        IRequestHandler<IsExistsPatientInfoCommand, bool>
    {
        private IPatientinfo _patientinfo;
        public UpdatePatientInfoHandler(IPatientinfo patientinfo)
        {
            _patientinfo = patientinfo;
        }

        public async Task<PatientInfo> Handle(UpdatePatientInfoCommand request, CancellationToken cancellationToken)
        {
            _patientinfo.PutPatientInfo(request.id, request.patientInfo);
            return await Task.FromResult(request.patientInfo);
        }

        public async Task<bool> Handle(IsExistsPatientInfoCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_patientinfo.PatientInfoExists(request.id));
        }
    }
}

