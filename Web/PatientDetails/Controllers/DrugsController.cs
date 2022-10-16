using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientDetails.Models;

namespace PatientDetails.Controllers
{
    [Route("api/drugs")]
    [ApiController]
    [Authorize]

    public class DrugsController : ControllerBase
    {
        private readonly PatientDetailsContext _context;

        public DrugsController(PatientDetailsContext context)
        {
            _context = context;
        }

        // GET: api/Drugs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drug>>> GetDrugs()
        {
            return await _context.Drugs.ToListAsync();
        }

        // GET: api/Drugs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drug>> GetDrug(int id)
        {
            var drug = await _context.Drugs.FindAsync(id);

            if (drug == null)
            {
                return NotFound();
            }

            return drug;
        }
        // POST: api/Drugs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Drug>> PostDrug(Drug drug)
        {
            _context.Drugs.Add(drug);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrug", new { id = drug.DrugId }, drug);
        }

        private bool DrugExists(int id)
        {
            return _context.Drugs.Any(e => e.DrugId == id);
        }
    }
}
