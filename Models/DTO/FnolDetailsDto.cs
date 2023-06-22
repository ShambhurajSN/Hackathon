using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Models.DTO
{
    //DTO used to show short information of fnol details
    public class FnolDetailsDto
    {
        public string? PolicyID {get ;set;}
        public string? ReporterName {get; set;}
        public string? ReportedDate {get; set;}
    }
}