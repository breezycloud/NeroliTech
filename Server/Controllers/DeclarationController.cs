using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeroliTech.Server.Services;
using NeroliTech.Shared;

namespace NeroliTech.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeclarationController : ControllerBase
    {

        [HttpGet]
        public ExporterDeclaration[] Get()
        {
            var content = ExporterServices._declarations.ToArray();
            return content;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExporterDeclaration>> Get(string id)
        {
            var declaration = ExporterServices._declarations.Where(i => i.CciNo == id).FirstOrDefault();
            if (declaration == null)
            {
                return NotFound();
            }
            await Task.Delay(100);
            return Ok();
        }

    }
}
