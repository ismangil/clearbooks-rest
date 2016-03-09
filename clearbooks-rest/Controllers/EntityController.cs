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
