using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Security.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace FnolApiVersion2.O.Security.Handlers
{
    public class UserCanOnlyHaveNonActiveCaseHandler : AuthorizationHandler<ActiveCaseForUserRequirement>
    {
        private readonly FnolDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserCanOnlyHaveNonActiveCaseHandler(FnolDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context=context;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        //Handler that checks for the requirement and returns the status of task
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveCaseForUserRequirement requirement)
        {
            var loggedInUserID = _httpContextAccessor.HttpContext.User.FindFirst("userID");
            if(loggedInUserID ==null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

                if( context.User.IsInRole("customer"))
                {
                    var activeCasesOfUser = _context.fnolDetails.Where(x=>x.UserID.ToString() == loggedInUserID.Value.ToString() && x.ActiveStatus == false).FirstOrDefault();
                    if(activeCasesOfUser != null)
                    {
                        context.Succeed(requirement);
                    }
                }
                else if(context.User.IsInRole("manager") || context.User.IsInRole("admin"))
                {
                    var activeCasesOfUser = _context.fnolDetails.Where(x=>x.UserID.ToString() == loggedInUserID.Value.ToString()).ToList();
                     if(activeCasesOfUser != null)
                    {
                        context.Succeed(requirement);
                    }
                }
                else
                {
                    context.Fail();
                }
            
            return Task.CompletedTask;


        }
    }
}