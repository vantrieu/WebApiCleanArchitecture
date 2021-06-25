using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Domain.Common;
using Web.Domain.Entities;
using Web.Infrastructure.Models;

namespace Web.Infrastructure.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService) : base(options, operationalStoreOptions)
        {
            this._currentUserService = currentUserService;
        }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach(var entry in ChangeTracker.Entries<AuditEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.Entity.CreatedBy = _currentUserService.UserId;

                        entry.Entity.CreatedAt = DateTime.UtcNow;

                        break;

                    case EntityState.Modified:

                        entry.Entity.CreatedBy = _currentUserService.UserId;

                        entry.Entity.CreatedAt = DateTime.UtcNow;

                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
