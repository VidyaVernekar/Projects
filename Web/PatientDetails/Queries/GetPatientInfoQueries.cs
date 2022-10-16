using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatientDetails.Models;

namespace PatientDetails.Queries
{
    public record GetPatientInfoQueries : IRequest<ActionResult<IEnumerable<PatientInfo>>>;
}
