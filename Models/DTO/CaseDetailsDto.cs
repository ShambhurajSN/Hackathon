using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Models.DTO
{
    public class CaseDetailsDto
    {
        public string? UserID {get ;set;}
        public string? FnolID {get ;set;}
        public string? PolicyID {get ;set;}
        public string? ReporterName {get; set;}
        public string? ReportedDate {get; set;}
        public bool? CaseStatus {get;set;}
    }
}