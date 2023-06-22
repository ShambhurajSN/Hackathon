using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Models.DTO;
using FnolApiVersion2.O.Models.Domains;

namespace FnolApiVersion2.O.Models.Domains
{
    //Model that holds proper users to their roles together
    public class User_Role
    {
        public Guid Id {get;set;}
        public Guid UserID {get;set;}
        public User user {get;set;}
        public Guid RoleID {get;set;}
        public Role role {get;set;}
    }
}