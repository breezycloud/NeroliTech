using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeroliTech.Server.Models;

namespace NeroliTech.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionFindingsController : ControllerBase
    {
        private readonly NeroliDBContext _context;

        public InspectionFindingsController(NeroliDBContext context)
        {
            _context = context;
        }

        // GET: api/InspectionFindings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreshipmentInspectionFinding>>> GetPreshipmentInspectionFindings()
        {
            return await _context.PreshipmentInspectionFindings.ToListAsync();
        }

        // GET: api/InspectionFindings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PreshipmentInspectionFinding>> GetPreshipmentInspectionFinding(Guid id)
        {
            var preshipmentInspectionFinding = await _context.PreshipmentInspectionFindings.FindAsync(id);

            if (preshipmentInspectionFinding == null)
            {
                return NotFound();
            }

            return preshipmentInspectionFinding;
        }

        // PUT: api/InspectionFindings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreshipmentInspectionFinding(Guid id, PreshipmentInspectionFinding preshipmentInspectionFinding)
        {
            if (id != preshipmentInspectionFinding.Id)
            {
                return BadRequest();
            }

            _context.Entry(preshipmentInspectionFinding).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreshipmentInspectionFindingExists(id))
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

        // POST: api/InspectionFindings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PreshipmentInspectionFinding>> PostPreshipmentInspectionFinding(PreshipmentInspectionFinding preshipmentInspectionFinding)
        {
            _context.PreshipmentInspectionFindings.Add(preshipmentInspectionFinding);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPreshipmentInspectionFinding", new { id = preshipmentInspectionFinding.Id }, preshipmentInspectionFinding);
        }

        // DELETE: api/InspectionFindings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PreshipmentInspectionFinding>> DeletePreshipmentInspectionFinding(Guid id)
        {
            var preshipmentInspectionFinding = await _context.PreshipmentInspectionFindings.FindAsync(id);
            if (preshipmentInspectionFinding == null)
            {
                return NotFound();
            }

            _context.PreshipmentInspectionFindings.Remove(preshipmentInspectionFinding);
            await _context.SaveChangesAsync();

            return preshipmentInspectionFinding;
        }

        private bool PreshipmentInspectionFindingExists(Guid id)
        {
            return _context.PreshipmentInspectionFindings.Any(e => e.Id == id);
        }
    }
}
