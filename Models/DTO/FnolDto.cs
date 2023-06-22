using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Models.DTO
{
    //DTO that is user to take all details from the user about case and used to transfer into Models later
    public class FnolDto
    {
        public string? PolicyID {get ;set;}
        public string? ReporterName {get; set;}
        public string? ReportedDate {get; set;}
        public string? CauseOfIncident {get;set;}
        public string? IncidentDate {get; set;}
        public string? DamagedParts {get; set;}
        public string? Description {get;set;}
        public string? AddressOfIncident {get;set;}
        public string? Landmark {get; set;}
        public string? DriverName{get; set;}
        public string? DriverLicenseNumber {get; set;}
        public string? DriverLicenseType {get; set;}
        public string? VehicleRegNumber{get; set;}
        public string? VehicleType{get; set;}
        public string? VehicleMaker {get; set;}
        public string? VehicleModel{get; set;}
        public string? RCNumber {get;set;}
        public IFormFile? FrontImage {get;set;}
        public IFormFile? BackImage {get;set;}
        public IFormFile? LeftImage {get;set;}
        public IFormFile? RightImage {get;set;}
    }
}