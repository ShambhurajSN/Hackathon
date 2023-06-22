using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Models.DTO;
using FnolApiVersion2.O.Repositories;
using FnolApiVersion2.O.Repositories.Services.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FnolApiVersion2.O.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationRepository _repository;
        //Constructor that takes IRegistrationRepository as a parameter
        public RegistrationController(IRegistrationRepository repository)
        {
            _repository=repository;
        }

        //Api Endpoint that is used for registering a user to the application
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegistrationDto regUser)
        {
            var userStatus = await _repository.RegisteringUserAsync(regUser);
            if(userStatus)
            {
                return Ok("Successfully Registered");
            }
            return BadRequest("Registration is Not Successfull");
        }
        //Api EndPoint that is used to register a role to the newly registered user
        //AccessToOrg Policy ensures that only employees of organisation is having access to this end points like employee,manager,admin etc
        //Returns the status of Role assignment
        [Route("RegisterRoleToUser")]
        [HttpPost]
        [Authorize(Policy = "AccessToOrg")]
        public async Task<IActionResult> RegisteringUserRole(RoleAssigningDto roleAssigningDto)
        {
            var userID = await _repository.GettingUserIdByName(roleAssigningDto.UserName);

            if(userID.ToString() != string.Empty)
            {
                var RoleAssignStatus = await _repository.RoleAssigningToUserAsync(roleAssigningDto,userID);
                if(RoleAssignStatus)
                {
                    return Ok("Role assigned Successfully");
                }
                else
                {
                    return BadRequest("Role has not Assigned Successfully");
                }
            }
            return NotFound("Username not Found");
        }
    }
}