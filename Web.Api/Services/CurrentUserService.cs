using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Web.Application.Common.Interfaces;

namespace Web.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _context;

        public CurrentUserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public bool IsAuthenticated
        {
            get
            {
                return _context.HttpContext.User.Identity.IsAuthenticated;
            }
        }

        public Guid UserId
        {
            get
            {
                var userId = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? _context.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

                return Guid.Parse(userId);
            }
        }
        public List<string> Roles
        {
            get
            {
                var userRoles = _context.HttpContext.User.FindAll("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Select(r => r.Value).ToList<string>();
                return userRoles;
            }
        }
    }
}
