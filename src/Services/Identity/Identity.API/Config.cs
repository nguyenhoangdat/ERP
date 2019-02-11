using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Identity.API
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "office",
                    UserClaims = { "office_number" }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("GatewayAPI", "The Global ApiGateway")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "ERPAdminWeb", // PUBLIC Unique Identifier of the connected client (Id, GUID, ...)
                    ClientName = "ERP Admin Web Application written in React",
                    RequireConsent = false,

                    /*
                     * http://docs.identityserver.io/en/latest/topics/grant_types.html
                     * Implicit - interactive user
                     * ClientCredentials - no interactive user, use the clientid/secret for authentication
                     */
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = { }, // Url for redirection after user has loged in /signin-oidc
                    PostLogoutRedirectUris = { }, // where to redirect after logout (/signout-callback-oidc)
                                        
                    // scopes that client has access to
                    AllowedScopes = {
                        // IdentityResources
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "office",
                        "location",

                        // ApiResources
                        "GatewayAPI"
                    }
                }
            };
        }
    }
}
