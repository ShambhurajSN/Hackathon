using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Models.DTO;
using FnolApiVersion2.O.Models.Domains;

namespace FnolApiVersion2.O.Repositories.Services.Users
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync (string userName,string password);
    }
}