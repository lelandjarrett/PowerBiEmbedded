// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// ----------------------------------------------------------------------------

namespace active_directory_aspnetcore_webapp_openidconnect_v2.Controllers
{
    using active_directory_aspnetcore_webapp_openidconnect_v2.Models;
    using active_directory_aspnetcore_webapp_openidconnect_v2.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System;
    using System.Text.Json;

    public class EmbedInfoController : Controller
    {
        private readonly PbiEmbedService pbiEmbedService;
        private readonly IOptions<PBIAD> PBIAD;
        private readonly IOptions<PowerBI> powerBI;

        public EmbedInfoController(PbiEmbedService pbiEmbedService, IOptions<PBIAD> PBIAD, IOptions<PowerBI> powerBI)
        {
            this.pbiEmbedService = pbiEmbedService;
            this.PBIAD = PBIAD;
            this.powerBI = powerBI;
        }

        /// <summary>
        /// Returns Embed token, Embed URL, and Embed token expiry to the client
        /// </summary>
        /// <returns>JSON containing parameters for embedding</returns>
        [HttpGet]
        public string GetEmbedInfo()
        {
            try
            {
                // Validate whether all the required configurations are provided in appsettings.json
                string configValidationResult = ConfigValidatorService.ValidateConfig(PBIAD, powerBI);
                if (configValidationResult != null)
                {
                    HttpContext.Response.StatusCode = 400;
                    return configValidationResult;
                }

                EmbedParams embedParams = pbiEmbedService.GetEmbedParams(new Guid(powerBI.Value.WorkspaceId), new Guid(powerBI.Value.ReportId));
                return JsonSerializer.Serialize<EmbedParams>(embedParams);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return ex.Message + "\n\n" + ex.StackTrace;
            }
        }
    }
}
