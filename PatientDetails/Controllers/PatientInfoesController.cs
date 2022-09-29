using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientDetails.Models;

namespace PatientDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientInfoesController : ControllerBase
    {
        private readonly PatientDetailsContext _context;

        public PatientInfoesController(PatientDetailsContext context)
        {
            _context = context;
        }

        // GET: api/PatientInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientInfo>>> GetPatientInfos()
        {
            return await _context.PatientInfos.ToListAsync();
        }

        // GET: api/PatientInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientInfo>> GetPatientInfo(int id)
        {
            var patientInfo = await _context.PatientInfos.FindAsync(id);

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

            _context.Entry(patientInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientInfoExists(id))
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
            _context.PatientInfos.Add(patientInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientInfo", new { id = patientInfo.Id }, patientInfo);
        }

        // DELETE: api/PatientInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientInfo(int id)
        {
            var patientInfo = await _context.PatientInfos.FindAsync(id);
            if (patientInfo == null)
            {
                return NotFound();
            }

            _context.PatientInfos.Remove(patientInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientInfoExists(int id)
        {
            return _context.PatientInfos.Any(e => e.Id == id);
        }
    }
}
