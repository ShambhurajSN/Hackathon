using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FnolApiVersion2.O.Models.Domains
{
    //Model that holds Driver Details of Incident
    [Table("DriverDetails")]
    public class DriverDetails
    {
        [Key]
        [Column("Driver_ID")]
        public string? DriverID {get;set;}

        [Column("DriverName")]
        public string? DriverName{get; set;}

        [Column("DriverLicenseNumber")]
        public string? DriverLicenseNumber {get; set;}

        [Column("DriverLicenseType")]
        public string? DriverLicenseType {get; set;}

        [Column("Incident_ID")]
        public string? IncidentID {get;set;}
        public IncidentDetails incidentDetails {get;set;}
        public VehicleDetails vehicleDetails {get;set;}
    }
}