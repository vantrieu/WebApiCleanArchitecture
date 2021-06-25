using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Web.Application.Common.Interfaces;

namespace Web.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
