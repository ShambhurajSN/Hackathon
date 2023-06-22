using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Models.DTO
{
    //DTO that holds Incident Details
    public class IncidentDetailsDto
    {
        public string? CauseOfIncident {get;set;}
        public string? IncidentDate {get; set;}
        public string? DamagedParts {get; set;}
        public string? Description {get;set;}
        public string? AddressOfIncident {get;set;}
        public string? Landmark {get; set;}
    }
}