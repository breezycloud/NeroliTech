using NeroliTech.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeroliTech.Server.Services
{
    public class ExporterServices
    {
        public static ExporterDeclaration[] _declarations = JsonSerializer.Deserialize<ExporterDeclaration[]>
            (File.ReadAllText("exporters.json"));



    }
}
