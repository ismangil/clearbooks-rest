using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace clearbooks_rest.Controllers
{
    public class CountriesController : ApiController
    {
        /// <summary>
        /// Retrieves two-letter country code
        /// </summary>
        /// <param name="countryName">Full country name</param>
        /// <returns>Two-letter country code</returns>
        [Route("api/GetCountryCode")]
        public string GetCountryCode(string countryName)
        {
            var countries = new Bia.Countries.Iso3166Countries(StringComparer.OrdinalIgnoreCase);

            return countries.GetAlpha2CodeByName(countryName);
        }
    }
}
