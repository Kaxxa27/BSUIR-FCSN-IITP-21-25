using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace WEB_153503_IdentityServer
{
    public static class Config
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(name: "WEB", displayName: "My WEB Project"),
                new ApiScope(name: "read", displayName: "Read data."),
                new ApiScope(name: "write", displayName: "Write data."),
                new ApiScope(name: "delete", displayName: "Delete data."),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("my-secret-key".Sha256()) },

                    AllowedScopes = { "read", "write", "profile"}
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "WEB",
                    ClientSecrets = { new Secret("my-secret-key".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:7001/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:7001/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:7001/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "WEB"
                    }                 
                },

                new Client
                {
                    ClientId = "blazorApp",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = { "https://localhost:7146/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:7146/authentication/logout-callback" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "read", "write" }
                },

            };
    }
}