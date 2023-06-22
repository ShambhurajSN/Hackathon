using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FnolApiVersion2.O.Models.Domains
{
    //Model that holds Incident Pictures
    [Table("IncidentPictures")]
    public class IncidentPicture
    {
        [Key]
        [Column("Picture_ID")]
        public string? PictureID {get;set;}

        [Column("FrontImage")]
        public byte[]? FrontImage {get;set;}

        [Column("BackImage")]
        public byte[]? BackImage {get;set;}

        [Column("LeftImage")]
        public byte[]? LeftImage {get;set;}

        [Column("RightImage")]
        public byte[]? RightImage {get;set;}

        [Column("Incident_ID")]
        public string IncidentID {get;set;}
        public IncidentDetails incidentDetails{get;set;}
    }
}