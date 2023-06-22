using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Models.DTO;

namespace FnolApiVersion2.O.Repositories.Services.Register
{
    public interface IRegistrationRepository
    {
        Task<bool> RegisteringUserAsync(UserRegistrationDto regUser);

        Task<Guid> GettingUserIdByName(string userName);
        Task<bool> RoleAssigningToUserAsync(RoleAssigningDto roleAssigningDto,Guid userID);
    }
}