using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FnolApiVersion2.O.Models.Domains
{
    //Class that holds Incident Details
    [Table("IncidentDetails")]
    public class IncidentDetails
    {
        [Key]
        [Column("Incident_ID")]
        public string? IncidentID {get;set;}

        [Column("CauseOfIncident")]
        public string? CauseOfIncident {get;set;}

        [Column("IncidentDate")]
        public string? IncidentDate {get; set;}

        [Column("PartsDamaged")]
        public string? DamagedParts {get; set;}

        [Column("Description")]
        [MaxLength(256)]
        public string? Description {get;set;}

        [Column("AddressOfIncident")]
        [MaxLength(256)]
        public string? AddressOfIncident {get;set;}

        [Column("Landmark")] 
        [StringLength(25)]
        public string? Landmark {get; set;}

        [Column("Fnol_ID")]
        public string? FnolID {get;set;}
        public FnolDetails fnolDetails {get;set;}

        public DriverDetails driverDetails {get; set;}

        public IncidentPicture incidentPicture {get;set;}
    }
}