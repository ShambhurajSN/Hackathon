using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Repositories;
using FnolApiVersion2.O.Repositories.Services.Case;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FnolApiVersion2.O.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ICaseRepository _repository;
        //Constructor of Controller that takes ICaseRepository as parameter
        public EmployeeController(ICaseRepository repository)
        {
            _repository=repository;
        }
        //Api EndPoint used to change the status of the User case Default will be false in Database
        //Authorization Policy that allows only to the Employees(employee,admin,manager)
        [Route("UpdateCaseStatus")]
        [HttpPatch]
        [Authorize(Policy ="AccessToOrg")]
        public async Task<IActionResult> UpdateStatusOfCaseByFnolID(string fnolID,bool updateStatusOfCase)
        {
            if(updateStatusOfCase == false)
            {
                var ClosedStatus = await _repository.CloseCaseAsync(fnolID);
                return Ok("Case is"+ClosedStatus);
            }
            if(updateStatusOfCase == true)
            {
                var OpenStatus = await _repository.OpenCaseAsync(fnolID);
                return Ok("Case is"+OpenStatus);
            }
            return BadRequest();
        }
        //Api EndPoint that is used to allocate case access to the user that are created by employees of Organisation
        //Authorization Policy that allows only to the Employees(employee,admin,manager)
        [Route("UpdateUserStatus")]
        [HttpPatch]
        [Authorize(Policy = "AccessToOrg")]
        public async Task<IActionResult> UpdateUserIDForCaseByUserName(string userName,string fnolID)
        {
            var status = await _repository.UpdateUserID(userName,fnolID);
            if(status)
            {
                return Ok($"case updated to user {userName}");
            }
            return BadRequest("Username or Case Id incorrect");
        }
    }
}