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
    public class ShippingDetailsController : ControllerBase
    {
        private readonly NeroliDBContext _context;

        public ShippingDetailsController(NeroliDBContext context)
        {
            _context = context;
        }

        // GET: api/ShippingDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExporterShippingDetail>>> GetExporterShippingDetails()
        {
            return await _context.ExporterShippingDetails.ToListAsync();
        }

        // GET: api/ShippingDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExporterShippingDetail>> GetExporterShippingDetail(Guid id)
        {
            var exporterShippingDetail = await _context.ExporterShippingDetails.FindAsync(id);

            if (exporterShippingDetail == null)
            {
                return NotFound();
            }

            return exporterShippingDetail;
        }

        // PUT: api/ShippingDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExporterShippingDetail(Guid id, ExporterShippingDetail exporterShippingDetail)
        {
            if (id != exporterShippingDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(exporterShippingDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExporterShippingDetailExists(id))
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

        // POST: api/ShippingDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExporterShippingDetail>> PostExporterShippingDetail(ExporterShippingDetail exporterShippingDetail)
        {
            _context.ExporterShippingDetails.Add(exporterShippingDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExporterShippingDetail", new { id = exporterShippingDetail.Id }, exporterShippingDetail);
        }

        // DELETE: api/ShippingDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExporterShippingDetail>> DeleteExporterShippingDetail(Guid id)
        {
            var exporterShippingDetail = await _context.ExporterShippingDetails.FindAsync(id);
            if (exporterShippingDetail == null)
            {
                return NotFound();
            }

            _context.ExporterShippingDetails.Remove(exporterShippingDetail);
            await _context.SaveChangesAsync();

            return exporterShippingDetail;
        }

        private bool ExporterShippingDetailExists(Guid id)
        {
            return _context.ExporterShippingDetails.Any(e => e.Id == id);
        }
    }
}
