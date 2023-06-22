using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Repositories.Services.Case
{
    //Repository used in Controller for case status updating and user id allocation to case
    public interface ICaseRepository
    {
        Task<string> CloseCaseAsync(string fnolID);
        Task<string> OpenCaseAsync(string fnolID);
        Task<bool> UpdateUserID(string userName,string fnolID);
    }
}