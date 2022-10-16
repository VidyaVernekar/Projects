
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatientDetails.Models;

namespace PatientDetails.Queries
{
    public record GetPatientInfoQueriesByID(int Id) : IRequest<PatientInfo>;
}

