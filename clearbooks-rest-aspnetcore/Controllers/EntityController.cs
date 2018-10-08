// Copyright (c) 2016-2018 Perry Ismangil
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
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace clearbooks_rest_aspnetcore.Controllers
{
    /// <summary>
    /// Maintaning customers and suppliers
    /// </summary>
    [ApiController]
    public class EntityController : ControllerBase
    {
        /// <summary>
        /// Instantiate ClearBooks service client
        /// </summary>
        /// <returns>Instance of ClearBooks service client</returns>
        private static AccountingPortClient NewClearBooksClient()
        {
            return new AccountingPortClient();
        }

        /// <summary>
        /// Retrieves apiKey from request header
        /// </summary>
        /// <returns>API Key</returns>
        private string GetAPIKeyHeader()
        {
            var headers = this.Request.Headers;

            //System.Diagnostics.Trace.TraceInformation("Headers: {0}", headers);

            Microsoft.Extensions.Primitives.StringValues headerValues;

           

            headers.TryGetValue("apiKey", out headerValues);

            // System.Diagnostics.Trace.TraceInformation("Header values: {0}", headerValues);

            return headerValues.FirstOrDefault();
        }

        // GET: api/getEntity/5
        /// <summary>
        /// Retrieve customer or supplier using ID
        /// </summary>
        /// <param name="id">Customer or supplied ID</param>
        /// <returns></returns>
        [Route("api/getEntity")]
        [HttpGet]
        public Entity Get(int id)
        {
            var cb = NewClearBooksClient();
            


  


                var auth = new authenticate();
                auth.apiKey = GetAPIKeyHeader();
                var eq = new EntityQuery();
                eq.id = new int[1];
                eq.id[0] = id;


                var cust = new Entity();
                
                Task<listEntitiesResponse> result = cb.listEntitiesAsync(auth, eq);
                if (result != null)
                {
                    cust = result.Result.listEntitiesResult[0];
                }

                return cust;

            
        }

        // POST: api/Entity
        /// <summary>
        /// Insert new customer or supplier
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>entity ID</returns>
        [Route("api/createEntity")]
        [HttpPost]
        public int  Post([FromBody]Entity entity)
        {
            var cb = NewClearBooksClient();
            

                var auth = new authenticate();
                auth.apiKey = GetAPIKeyHeader();

                Task<createEntityResponse> response = cb.createEntityAsync(auth, entity);

                return response.Result.createEntityResult;
            
        }

        /*
        // PUT: api/Entity/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Entity/5
        public void Delete(int id)
        {
        }
        */
    }
}
