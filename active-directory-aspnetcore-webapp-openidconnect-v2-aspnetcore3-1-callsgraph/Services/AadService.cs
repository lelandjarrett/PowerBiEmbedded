// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// ----------------------------------------------------------------------------

namespace active_directory_aspnetcore_webapp_openidconnect_v2.Services
{
    using active_directory_aspnetcore_webapp_openidconnect_v2.Models;
    using Microsoft.Extensions.Options;
    using Microsoft.Identity.Client;
    using System;
    using System.Linq;
    using System.Security;

    public class AadService
    {
        private readonly IOptions<PBIAD> PBIAD;

        public AadService(IOptions<PBIAD> PBIAD)  
        {
            this.PBIAD = PBIAD;
        }

        /// <summary>
        /// Generates and returns Access token
        /// </summary>
        /// <returns>AAD token</returns>
        public string GetAccessToken()
        {
            AuthenticationResult authenticationResult = null;
            if (PBIAD.Value.AuthenticationMode.Equals("masteruser", StringComparison.InvariantCultureIgnoreCase))
            {
                // Create a public client to authorize the app with the AAD app
                IPublicClientApplication clientApp = PublicClientApplicationBuilder.Create(PBIAD.Value.ClientId).WithAuthority(PBIAD.Value.AuthorityUri).Build();
                var userAccounts = clientApp.GetAccountsAsync().Result;
                try
                {
                    // Retrieve Access token from cache if available
                    authenticationResult = clientApp.AcquireTokenSilent(PBIAD.Value.Scope, userAccounts.FirstOrDefault()).ExecuteAsync().Result;
                }
                catch (MsalUiRequiredException)
                {
                    SecureString password = new SecureString();
                    foreach (var key in PBIAD.Value.PbiPassword)
                    {
                        password.AppendChar(key);
                    }
                    authenticationResult = clientApp.AcquireTokenByUsernamePassword(PBIAD.Value.Scope, PBIAD.Value.PbiUsername, password).ExecuteAsync().Result;
                }
            }

            // Service Principal auth is the recommended by Microsoft to achieve App Owns Data Power BI embedding
            else if (PBIAD.Value.AuthenticationMode.Equals("serviceprincipal", StringComparison.InvariantCultureIgnoreCase))
            {
                // For app only authentication, we need the specific tenant id in the authority url
                var tenantSpecificUrl = PBIAD.Value.AuthorityUri.Replace("organizations", PBIAD.Value.TenantId);

                // Create a confidential client to authorize the app with the AAD app
                IConfidentialClientApplication clientApp = ConfidentialClientApplicationBuilder
                                                                                .Create(PBIAD.Value.ClientId)
                                                                                .WithClientSecret(PBIAD.Value.ClientSecret)
                                                                                .WithAuthority(tenantSpecificUrl)
                                                                                .Build();
                // Make a client call if Access token is not available in cache
                authenticationResult = clientApp.AcquireTokenForClient(PBIAD.Value.Scope).ExecuteAsync().Result;
            }

            return authenticationResult.AccessToken;
        }
    }
}
