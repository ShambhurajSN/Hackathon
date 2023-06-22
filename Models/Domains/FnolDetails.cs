using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FnolApiVersion2.O.Models.Domains
{
    //Class that takes Policy and Reporter details
    [Table("FnolDetails")]
    public class FnolDetails
    {
        [Key]
        [Column("Fnol_ID")]
        public string? FnolID { get; set; }

        [Column("Policy_ID")]
        public string? PolicyID {get ;set;}

        [Column("Reporter_Name")]
        public string? ReporterName {get; set;}
 
        [Column("ReportedDate")]
        public string? ReportedDate {get; set;}

        [Column("UserID")]
        public Guid? UserID {get;set;} 

        [Column("ActiveCase")]
        public bool ActiveStatus {get;set;}
        public IncidentDetails incidentDetails  {get;set;}
        public User userDetails {get;set;}
        
    }
    
}