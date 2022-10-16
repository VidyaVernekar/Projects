using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PatientDetails.Models;
using PatientDetails.Services;
using MediatR;
using PatientDetails.Queries;
using PatientDetails.Handlers;
using PatientDetails.Commands;

namespace PatientDetails.Controllers
{
    [Route("api/patientinfo")]
    [ApiController]
    public class PatientinfoController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public PatientinfoController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }
     
        // GET: api/PatientInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientInfo>>> GetPatientInfos()
        {
            return await _mediatR.Send(new GetPatientInfoQueries());
        }
        //Alternate way;
      //  public async Task<ActionResult<IEnumerable<PatientInfo>>> GetPatientInfos() => await _mediatR.Send(new GetPatientInfoQueries());

        // GET: api/PatientInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientInfo>> GetPatientInfo(int id)
        {
            var patientInfo = await _mediatR.Send(new GetPatientInfoQueriesByID(id));

            if (patientInfo == null)
            {
                return NotFound();
            }

            return patientInfo;
        }

        // PUT: api/PatientInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientInfo(int id, PatientInfo patientInfo)
        {
            if (id != patientInfo.Id)
            {
                return BadRequest();
            }
            try
            {
                await _mediatR.Send(new UpdatePatientInfoCommand(id, patientInfo));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PatientInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PatientInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatientInfo>> PostPatientInfo(PatientInfo patientInfo)
        {
           await _mediatR.Send(new CreatePatientInfoCommand(patientInfo));

            return CreatedAtAction("GetPatientInfo", new { id = patientInfo.Id }, patientInfo);
        }

        // DELETE: api/PatientInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientInfo(int id)
        {
            var patientInfo = await _mediatR.Send(new GetPatientInfoQueriesByID(id));
            if (patientInfo == null)
            {
                return NotFound();
            }

            await _mediatR.Send(new DeletePatientInfoCommand(patientInfo));

            return Ok();
        }

        private async Task<bool> PatientInfoExists(int id)
        {
            return await _mediatR.Send(new IsExistsPatientInfoCommand(id));
        }

        [Route("endpoint")]
        [HttpPost]
        public async Task<IActionResult> Save(List<PatientDetailsODSheet> patientsList)
        {
            List<PatientDetailsODSheet> formatNotCorrectList = await _mediatR.Send(new CreatePatientInfoBySpreadSheetCommand(patientsList));
           
            if (formatNotCorrectList.Count > 0 || formatNotCorrectList.Count == 0)
                return Ok(formatNotCorrectList);
            else
                return BadRequest();
        }
        
    }
}
