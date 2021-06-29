using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Application.Common.Interfaces;
using Web.Infrastructure.Data;
using Web.Infrastructure.IdentityServer;
using Web.Infrastructure.Models;

namespace Web.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => 
                provider.GetRequiredService<ApplicationDbContext>());

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => 
                options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
               .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
               .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
               .AddInMemoryClients(IdentityServerConfig.Clients)
               .AddAspNetIdentity<ApplicationUser>()
               .AddProfileService<CustomProfileService>()
               .AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddLocalApi("Bearer", option =>
                {
                    option.ExpectedScope = "CleanArchitecture.api";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                });
            });


            return services;
        }
    }
}
