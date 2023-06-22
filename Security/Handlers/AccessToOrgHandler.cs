using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Security.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace FnolApiVersion2.O.Security.Handlers
{
    public class AccessToOrgHandler : AuthorizationHandler<UserIncidentAccessToOrgRequirement>
    {
        private readonly FnolDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccessToOrgHandler(FnolDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context=context;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        //Handler that checks for the requirement and returns the status of task
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIncidentAccessToOrgRequirement requirement)
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
                if(context.User.IsInRole("manager") || context.User.IsInRole("admin") || context.User.IsInRole("employee") )
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
          
        }
    }
}