using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FnolApiVersion2.O.Models.Domains
{
    //Class that holds Vehicle Details of Incident
    [Table("VehicleDetails")]
    public class VehicleDetails
    {
        [Key]
        [Column("Vehicle_ID")]
        public string? VehicleID {get;set;}

        [Column("VehicleRegistrationNumber")]
        public string? VehicleRegNumber{get; set;}

        [Column("VehicleType")]
        public string? VehicleType{get; set;}

        [Column("VehicleMaker")]
        public string? VehicleMaker {get; set;}

        [Column("VehicleModel")]
        public string? VehicleModel{get; set;}

        [Column("RegistrationCertificateNumber")]
        public string? RCNumber {get;set;}

        [Column("Driver_ID")]
        public string? DriverID {get;set;}
        public DriverDetails driverDetails {get;set;}
    }
}