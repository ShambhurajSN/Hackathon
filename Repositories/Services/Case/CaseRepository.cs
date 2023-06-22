using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FnolApiVersion2.O.Data;
using Microsoft.EntityFrameworkCore;

namespace FnolApiVersion2.O.Repositories.Services.Case
{
    //Repository Used for updating case status in Database and updating UserIDs to cases created by employees
    public class CaseRepository : ICaseRepository
    {
        private readonly FnolDbContext _context;
        public CaseRepository(FnolDbContext context)
        {
            _context=context;
        }
        //Method used to Update user ID to case that was created by the employees
        public async Task<bool> UpdateUserID(string userName,string fnolID)
        {
            var userDetails = await _context.Users.Where(x=> x.UserName == userName).FirstOrDefaultAsync();
            var fnolDetails = await _context.fnolDetails.Where(x=>x.FnolID == fnolID).FirstOrDefaultAsync();
            if(fnolDetails != null && userDetails != null)
            {
                fnolDetails.UserID = userDetails.Id;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        //Method used to Close a status
        public async Task<string> CloseCaseAsync(string fnolID)
        {
            var fnolDetails = await _context.fnolDetails.Where(x => x.FnolID == fnolID).FirstOrDefaultAsync();
            if(fnolDetails != null)
            {
              fnolDetails.ActiveStatus = false;
              await _context.SaveChangesAsync();
              return "Closed";
            }
            else
            {
                return "Case Not Found";
            }
        }
        //Method used to Open a status
        public async Task<string> OpenCaseAsync(string fnolID)
        {
            var fnolDetails = await _context.fnolDetails.Where(x => x.FnolID == fnolID).FirstOrDefaultAsync();
            if(fnolDetails != null)
            {
              fnolDetails.ActiveStatus = true;
              await _context.SaveChangesAsync();
              return "Open";
            }
            else
            {
                return "Case Not Found";
            }
        }
    }
}