using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Models.DTO;
using FnolApiVersion2.O.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FnolApiVersion2.O.Repositories.Services.Fnol
{
    //Repository related to CRUD operations on the FNOL data received from the user
    public interface IFnolRepository
    {
        Task<string> AddFnolDetailsToDbAsync(FnolDto fnolDto,Claim userID);
        Task<List<FnolDetailsAdminDto>> GetAllFnolDetailsAsync();
        Task<FnolCustomerDto> GetFnolByIDAsync(string FnolID);
        Task<bool> DeleteFnolByIDAsync(string FnolID);
        Task<bool> UpdateFnolByIDAsync(string FnolID,FnolDto fnolDto);
        Task<List<CaseDetailsDto>> GetAllOnlyFnolDetailsAsync();
        Task<IncidentPicture> GetIncidentPicturesByID(string FnolID);
        
    }
}