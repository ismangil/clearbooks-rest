// Copyright (c) 2016 Perry Ismangil
// This file is part of clearbooks-rest project.
//
// clearbooks-rest project is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
//  clearbooks-rest project is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Affero General Public License for more details.
//
//  You should have received a copy of the GNU Affero General Public License
//  along with clearbooks-rest project.  If not, see <http://www.gnu.org/licenses/>.

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
