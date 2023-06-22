using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Repositories.Services.Roles
{
    public interface IRoleAssign
    {
        Task<bool> AssignRolesInDBAsync(string roleName);
    }
}