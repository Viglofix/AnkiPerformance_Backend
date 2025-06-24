using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
            new ApiScope("AnkiCommunication"),
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
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "http://localhost:5173/manager.html#/anki" },

                FrontChannelLogoutUri = "https://localhost:5137/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:5137/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "AnkiCommunication" }
            },

            // interactive client using code flow + pkce + bff support for safe token validation
            new Client
            {
                ClientId = "bff",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {"http://localhost:5173/signin-oidc","http://localhost:5155/signin-oidc","http://localhost:5000/signin-oidc"},

                PostLogoutRedirectUris = { "http://localhost:5000/signout-callback-oidc" },
                AllowedCorsOrigins = {"http://localhost:5155"},

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "AnkiCommunication" }
            },
        };
}
