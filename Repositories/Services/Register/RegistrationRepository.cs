using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace FnolApiVersion2.O.Repositories.Services.Register
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly FnolDbContext _context;

        private readonly IMapper _mapper;

        public RegistrationRepository(FnolDbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        //Method used to Register user to application
        public async Task<bool> RegisteringUserAsync(UserRegistrationDto regUser)
        {
            var user = _mapper.Map<User>(regUser);
            if(user != null)
            {
                user.Id = Guid.NewGuid();
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        //Method that returns user ID by name
        public async Task<Guid> GettingUserIdByName(string userName)
        {
            var user = await _context.Users.Where(x => x.UserName == userName).Select( x => new User()
            {
                UserName = x.UserName,
                Id=x.Id,
                FirstName=x.FirstName,
                LastName=x.LastName,
                EmailAddress=x.EmailAddress
            }).FirstOrDefaultAsync();

            return user.Id;
        }

        //Method that assigns roles to the user 
        public async Task<bool> RoleAssigningToUserAsync(RoleAssigningDto roleAssigningDto,Guid userID)
        {
            var Role = await _context.Roles.Where(x => x.RoleName == roleAssigningDto.Role.ToLower()).Select(x=> new Role()
            {
                ID = x.ID,
                RoleName=x.RoleName
            }).FirstOrDefaultAsync();
            if(Role != null)
            {
                User_Role userRole = new User_Role();
                userRole.Id = Guid.NewGuid();
                userRole.RoleID=Role.ID;
                userRole.UserID=userID;

                await _context.User_Roles.AddAsync(userRole);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}