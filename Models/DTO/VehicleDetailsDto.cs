using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Models.DTO
{
    //DTO that holds Vehicle Details
    public class VehicleDetailsDto
    {
        public string? VehicleRegNumber{get; set;}
        public string? VehicleType{get; set;}
        public string? VehicleMaker {get; set;}
        public string? VehicleModel{get; set;}
        public string? RCNumber {get;set;}
    }
}