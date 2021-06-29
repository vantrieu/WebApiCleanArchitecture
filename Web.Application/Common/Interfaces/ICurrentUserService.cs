using System;

namespace Web.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
    }
}
