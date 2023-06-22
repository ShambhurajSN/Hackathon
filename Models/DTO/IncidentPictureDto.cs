using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Models.DTO
{
    //DTO that holds Incident Pictures in form of byte arrays that is used in Database
    public class IncidentPictureDto
    {
        public byte[] FrontImage {get;set;}
        public byte[] BackImage {get;set;}
        public byte[] LeftImage {get;set;}
        public byte[] RightImage {get;set;}
    }
}