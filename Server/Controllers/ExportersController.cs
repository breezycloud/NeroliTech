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
    public class ExportersController : ControllerBase
    {
        private readonly NeroliDBContext _context;

        public ExportersController(NeroliDBContext context)
        {
            _context = context;
        }

        // GET: api/Exporters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExporterDeclaration>>> GetExporterDeclarations()
        {
            return await _context.ExporterDeclarations
                .Include(s => s.ExporterShippingDetails)
                .Include(u=> u.PreshipmentInspectionFindings)
                .ToListAsync();
        }

        // GET: api/Exporters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExporterDeclaration>> GetExporterDeclaration(Guid id)
        {
            var exporterDeclaration = await _context.ExporterDeclarations.FindAsync(id);

            if (exporterDeclaration == null)
            {
                return NotFound();
            }

            return exporterDeclaration;
        }

        // PUT: api/Exporters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExporterDeclaration(Guid id, ExporterDeclaration exporterDeclaration)
        {
            if (id != exporterDeclaration.Id)
            {
                return BadRequest();
            }

            _context.Entry(exporterDeclaration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExporterDeclarationExists(id))
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

        // POST: api/Exporters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExporterDeclaration>> PostExporterDeclaration(ExporterDeclaration exporterDeclaration)
        {
            _context.ExporterDeclarations.Add(exporterDeclaration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExporterDeclaration", new { id = exporterDeclaration.Id }, exporterDeclaration);
        }

        // DELETE: api/Exporters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExporterDeclaration>> DeleteExporterDeclaration(Guid id)
        {
            var exporterDeclaration = await _context.ExporterDeclarations.FindAsync(id);
            if (exporterDeclaration == null)
            {
                return NotFound();
            }

            _context.ExporterDeclarations.Remove(exporterDeclaration);
            await _context.SaveChangesAsync();

            return exporterDeclaration;
        }

        private bool ExporterDeclarationExists(Guid id)
        {
            return _context.ExporterDeclarations.Any(e => e.Id == id);
        }
    }
}
