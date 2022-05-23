// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// ----------------------------------------------------------------------------

namespace active_directory_aspnetcore_webapp_openidconnect_v2.Models
{
    using Microsoft.PowerBI.Api.Models;
    using System.Collections.Generic;

    public class EmbedParams
    {
        // Type of the object to be embedded
        public string Type { get; set; }

        // Report to be embedded
        public List<EmbedReport> EmbedReport { get; set; }

        // Embed Token for the Power BI report
        public EmbedToken EmbedToken { get; set; }
    }
}
