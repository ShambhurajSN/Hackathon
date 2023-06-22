using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Models.Domains;
//using FnolApiVersion2.O.Repositories.Services.
using Microsoft.EntityFrameworkCore;

namespace FnolApiVersion2.O.Repositories.Services.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly FnolDbContext _context;

        public UserRepository(FnolDbContext context)
        {
            _context=context;
            
        }
        //Method that authenticates user through password 
        public async Task<User> AuthenticateUserAsync(string userName, string password)
        {
            var user =await _context.Users.Where(x => x.UserName == userName && x.Password == password).FirstOrDefaultAsync();
            
            if(user == null)
            {
                return null;
            }
            var activeCaseDetails = await _context.fnolDetails.Where(x => x.UserID == user.Id).FirstOrDefaultAsync();
            if(activeCaseDetails != null)
            {
                user.activeCase = true;
            }
            else
            {
                user.activeCase=false;
            }
            var userRoles =await _context.User_Roles.Where(x=> x.UserID == user.Id).ToListAsync();
            if(userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach(var userRole in userRoles)
                {
                    var role = await _context.Roles.Where(x => x.ID == userRole.RoleID).FirstOrDefaultAsync();
                    user.Roles.Add(role.RoleName);
                   
                }
            }
            return user;
            
        }
    }
}