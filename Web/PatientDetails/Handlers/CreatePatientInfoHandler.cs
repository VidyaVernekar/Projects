using MediatR;
using PatientDetails.Commands;
using PatientDetails.Models;
using PatientDetails.Services;

namespace PatientDetails.Handlers
{
    public class CreatePatientInfoHandler : IRequestHandler<CreatePatientInfoCommand, PatientInfo>, IRequestHandler<CreatePatientInfoBySpreadSheetCommand, List<PatientDetailsODSheet>>
    {
        private IPatientinfo _patientinfo;
        public CreatePatientInfoHandler(IPatientinfo patientinfo)
        {
            _patientinfo = patientinfo;
        }
        public async Task<PatientInfo> Handle(CreatePatientInfoCommand request, CancellationToken cancellationToken)
        {
            _patientinfo.PostPatientInfo(request.patientInfo);
            return await Task.FromResult(request.patientInfo);
        }

        public async Task<List<PatientDetailsODSheet>> Handle(CreatePatientInfoBySpreadSheetCommand request, CancellationToken cancellationToken)
        {
            return await _patientinfo.PatientSpreedShetSave(request.patientOD);
        }
    }
}
