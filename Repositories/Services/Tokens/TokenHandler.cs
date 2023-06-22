using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Models.DTO;
using FnolApiVersion2.O.Models.Domains;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;

namespace FnolApiVersion2.O.Repositories.Services.Tokens
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        private readonly FnolDbContext _context;
        public TokenHandler(IConfiguration configuration,FnolDbContext context)
        {
            _configuration=configuration;
            _context=context;
        }
        //Method that generates token from the user details received
        public  Task<string> CreateTokenAsync(User user)
        {
            //Create Claims
           var claims = new List<Claim>();
           claims.Add(new Claim(ClaimTypes.GivenName,user.FirstName));
           claims.Add(new Claim(ClaimTypes.Surname,user.LastName));
           claims.Add(new Claim(ClaimTypes.Email,user.EmailAddress));
           claims.Add(new Claim(type:"userID",value:user.Id.ToString()));
            var activeStatus =  _context.fnolDetails.Where(x => x.UserID == user.Id).ToList();
            if(activeStatus.Count == 0)
            {
                claims.Add(new Claim(type:"activeCase",value:false.ToString()));
            }
            else
            {
                foreach(var item in activeStatus)
            {
                if(item.ActiveStatus == true && item.UserID == user.Id)
                {
                    claims.Add(new Claim(type:"activeCase",value:true.ToString()));
                    break;
                }
                else
                {
                    claims.Add(new Claim(type:"activeCase",value:false.ToString()));
                }
            }
            }
           //Loop into Roles
           user.Roles.ForEach((role)=> 
           {
                claims.Add(new Claim(ClaimTypes.Role,role));
           });
            //Creating credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials : credentials
            );
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}