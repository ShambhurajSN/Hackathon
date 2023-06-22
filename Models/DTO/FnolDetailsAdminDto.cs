using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Models.DTO
{
    //Fnol Admin DTO used to show all details of Case with ID fields included in it
    public class FnolDetailsAdminDto
    {
        public string? FnolID {get; set;}
        public string? PolicyID {get ;set;}
        public string? ReporterName {get; set;}
        public string? LossDate {get; set;}
        public string? IncidentID {get;set;}
        public string? CauseOfIncident {get;set;}
        public string? IncidentDate {get; set;}
        public string? DamagedParts {get; set;}
        public string? Description {get;set;}
        public string? AddressOfIncident {get;set;}
        public string? Landmark {get; set;}
        public string? DriverID {get; set;}
        public string? DriverName{get; set;}
        public string? DriverLicenseNumber {get; set;}
        public string? DriverLicenseType {get; set;}
        public string? VehicleId {get; set;}
        public string? VehicleRegNumber{get; set;}
        public string? VehicleType{get; set;}
        public string? VehicleMaker {get; set;}
        public string? VehicleModel{get; set;}
        public string? RCNumber {get;set;}
        public string? PictureID{get;set;}
    }
}