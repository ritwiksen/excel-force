using System;

namespace ExcelForce.Foundation.CoreServices.Models.Configuration
{
    public class ApiConfiguration
    {
        public string ProductionUrl { get; set; }

        public string LocalUrl { get; set; }

        public bool? IsEnvironmentProduction { get; set; }

        public string GetUrl()
        {
            if (!IsEnvironmentProduction.HasValue)
            {
                throw new InvalidOperationException("Cannot get environment URL till environment type isn't set");
            }

            return IsEnvironmentProduction.Value
                  ? ProductionUrl
                  : LocalUrl;
        }
    }
}
