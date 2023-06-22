using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace FnolApiVersion2.O.Repositories.Services.Roles
{
    public class RoleAssign : IRoleAssign
    { 
        private readonly FnolDbContext _context;
        public RoleAssign(FnolDbContext context)
        {
            _context = context;
        }
        //Method that assigns Roles to the application
        public async Task<bool> AssignRolesInDBAsync(string roleName)
        {
            var role = await _context.Roles.Where(x => x.RoleName == roleName.ToLower()).Select(x => new Role()
            {
                ID = x.ID,
                RoleName=x.RoleName

            }).FirstOrDefaultAsync();

            if(role == null)
            {
                Role roleAssign = new Role();
                roleAssign.ID = Guid.NewGuid();
                roleAssign.RoleName = roleName.ToLower();
                await _context.Roles.AddAsync(roleAssign);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
            
        }
    }
}