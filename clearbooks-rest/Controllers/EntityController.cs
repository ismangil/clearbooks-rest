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
    /// <summary>
    /// Maintaning customers and suppliers
    /// </summary>
    public class EntityController : ApiController
    {
        private static ClearBooksService.AccountingPortClient NewClearBooksClient()
        {
            return new ClearBooksService.AccountingPortClient();
        }

        // GET: api/Entity
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Entity/5
        /// <summary>
        /// Retrieve customer or supplier using ID
        /// </summary>
        /// <param name="id">Customer or supplied ID</param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public ClearBooksService.Entity Get(int id, string apiKey)
        {
            using (var cb = NewClearBooksClient())
            {
                var auth = new ClearBooksService.authenticate();
                auth.apiKey = apiKey;
                var eq = new ClearBooksService.EntityQuery();
                eq.id = new int[1];
                eq.id[0] = id;


                var cust = new ClearBooksService.Entity();
                var result = cb.listEntities(auth, eq);

                if (result.Length > 0)
                {
                    cust = result[0];
                }


                return cust;

            }
        }

        // POST: api/Entity
        /// <summary>
        /// Insert new customer or supplier
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="apiKey"></param>
        /// <returns>entity ID</returns>
        public int  Post([FromBody]ClearBooksService.Entity entity, string apiKey)
        {
            using (var cb = NewClearBooksClient())
            {
                var auth = new ClearBooksService.authenticate();
                auth.apiKey = apiKey;

                return cb.createEntity(auth, entity);
            }
        }

        // PUT: api/Entity/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Entity/5
        public void Delete(int id)
        {
        }
    }
}
