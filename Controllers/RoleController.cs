using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Repositories;
using FnolApiVersion2.O.Repositories.Services.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FnolApiVersion2.O.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleAssign _repository;
        //Constructor that takes IRoleAssign as a parameter
        public RoleController(IRoleAssign repository)
        {
            _repository = repository;
        }
        //Api Endpoint used to add new Roles to the application
        //This end point is protected by role based authorization only admin has access to it
        [Route("AssigningRoles")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignNewRoles(string roleName)
        {
            var roleAssignment = await _repository.AssignRolesInDBAsync(roleName);

            if(roleAssignment)
            {
                return Ok($"{roleName} : Role Added Successfully");
            }
            return BadRequest("Role already exists");
        }
    }
}