using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using FnolApiVersion2.O.Security.Requirements;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using FnolApiVersion2.O.Data;

namespace FnolApiVersion2.O.Security.Handlers
{
    public class CanOnlyAccessToHisResourceHandler : AuthorizationHandler<UserAccessToHisResourceOnlyRequirement>
    {
        private readonly FnolDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CanOnlyAccessToHisResourceHandler(FnolDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context=context;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        //Handler that checks for the requirement and returns the status of task
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAccessToHisResourceOnlyRequirement requirement)
        {
            
            var loggedInUserID = _httpContextAccessor.HttpContext.User.FindFirst("userID");
            if(loggedInUserID ==null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var requestedFnolID = _httpContextAccessor.HttpContext.Request.Query["FnolID"].ToString();

            var resourceRequested = _context.fnolDetails.Where(x => x.FnolID == requestedFnolID).FirstOrDefault();
            
            if(resourceRequested != null)
            {
                if(resourceRequested.UserID.ToString() == loggedInUserID.Value.ToString() || context.User.IsInRole("manager") || context.User.IsInRole("admin"))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;

        }
    }
}