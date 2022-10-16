using MediatR;
using PatientDetails.Queries;
using PatientDetails.Models;
using Microsoft.EntityFrameworkCore;
using PatientDetails.Services;
using Microsoft.AspNetCore.Mvc;

namespace PatientDetails.Handlers
{
    public class GetPatientInfoListHandler : IRequestHandler<GetPatientInfoQueries, ActionResult<IEnumerable<PatientInfo>>>,
        IRequestHandler<GetPatientInfoQueriesByID, PatientInfo>
    {
        private IPatientinfo _patientinfo;
        public GetPatientInfoListHandler(IPatientinfo patientinfo)
        {
            _patientinfo = patientinfo;
        }
        public async Task<ActionResult<IEnumerable<PatientInfo>>> Handle(GetPatientInfoQueries request, CancellationToken cancellationToken)
        {
            return await _patientinfo.GetPatientInfos();
        }

        public async Task<PatientInfo> Handle(GetPatientInfoQueriesByID request, CancellationToken cancellationToken)
        {
            return await _patientinfo.GetPatientInfo(request.Id);
        }
    }
}
