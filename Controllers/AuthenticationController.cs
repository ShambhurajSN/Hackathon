using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Data;
using Newtonsoft.Json.Linq;
using FnolApiVersion2.O.Models.DTO;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Repositories;
using Microsoft.AspNetCore.Mvc;
using FnolApiVersion2.O.Repositories.Services.Users;
using FnolApiVersion2.O.Repositories.Services.Tokens;
using FnolApiVersion2.O.Repositories.Services.Validate;

namespace FinalFnolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IUserRepository _repository;
        private readonly ITokenHandler _tokenHandler;

        private readonly IValidateRepository _validate;

        //Constructor of Controller that takes IUserRepository,ITokenHandler as parameters
        public AuthenticationController(IUserRepository repository,ITokenHandler tokenHandler,IValidateRepository validate)
        {
            _tokenHandler = tokenHandler;
            _repository=repository;
            _validate = validate;
        }

        //Api Endpoint that is used to Log In to the Application
        //Endpoint that takes username,password and generates a JWT Token with expiry time of Token 
        [Route("LogIn")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserDto userDto)
        {
            var authenticatedUser = await _repository.AuthenticateUserAsync(userDto.userName,userDto.password);
            Token tokenObj = new Token();
            if(authenticatedUser != null)
            {
                var token = await _tokenHandler.CreateTokenAsync(authenticatedUser);
                tokenObj.TokenGenerated =token; 
                tokenObj.TokenExpiry=DateTime.Now.AddMinutes(5);
                
                // var validationStatus = _validate.Authenticate(tokenObj.TokenGenerated);

                // if(validationStatus)
                // {
                //     return Ok("Logged In Successfully");
                // }
                return Ok(tokenObj);
                
                
            }
            return BadRequest("Username or Password is Incorrect");
        }
        
        
    }
}