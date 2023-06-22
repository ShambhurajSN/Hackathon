using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Repositories.Services.Validate
{
    public interface IValidateRepository
    {
        bool Authenticate(string token);
    }
}