using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace clearbooks_rest.Controllers
{
    public class CountryCodeController : ApiController
    {

        // GET: api/CountryCode/5
        /// <summary>
        /// Given the country name returns two letter code
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns>Two letter country code</returns>
        public string Get(string countryName)
        {
            var countries = new Bia.Countries.Iso3166Countries(StringComparer.OrdinalIgnoreCase);

            return countries.GetAlpha2CodeByName(countryName);
        }
    }
}
