using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Models.Domains
{
    //Model that holds User Details
    public class User
    {
         public Guid Id { get; set; }
        public string UserName {get;set;}
        public string EmailAddress{get;set;}
        public string Password { get; set; }
        public string FirstName{get;set;}
        public string LastName { get; set; }

        [NotMapped]
        public List<string> Roles {get;set;}

        [NotMapped]
        public bool activeCase {get;set;}

        //Navigation Property
        public List<User_Role> UserRoles {get;set;}

        public List<FnolDetails> FnolDetails {get;set;}
        
    }
}