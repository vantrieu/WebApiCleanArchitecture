using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Web.Infrastructure.IdentityServer
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
             {
                  new ApiScope("CleanArchitecture.api", "Clean Architecture .Net Core API")
             };

        public static IEnumerable<Client> Clients => new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "CleanArchitecture.api" }
                },

                new Client
                {
                    ClientId = "swagger",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris =           { $"https://localhost:44328/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"https://localhost:44328/swagger/oauth2-redirect.html" },
                    AllowedCorsOrigins =     { $"https://localhost:44328" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "CleanArchitecture.api"
                    }
                },
            };
    }
}
