using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Models.Domains
{
    //Model used to hold Roles of Application
    public class Role
    {
        public Guid ID {get;set;}

        public string? RoleName {get;set;}

        //NavigationProperty
        public List<User_Role> UserRoles {get;set;}
    }
}