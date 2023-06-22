using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Models.DTO
{
    //DTO used to hold User Details
    public class UserRegistrationDto
    {
        public string UserName {get;set;}
         public string FirstName{get;set;}
        public string LastName { get; set; }
        public string EmailAddress{get;set;}
        public string Password { get; set; }
       
    }
}