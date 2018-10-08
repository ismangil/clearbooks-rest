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

namespace clearbooks_rest.Controllers
{
    [ApiController]
    public class InvoiceController : ControllerBase
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

        /*
        // GET: api/Invoice
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */
        // GET: api/Invoice/5
        [Route("api/getInvoice")]
        [HttpGet]
        public Invoice Get(int invoiceID, string ledger)
        {

                var auth = new authenticate();

                auth.apiKey = GetAPIKeyHeader();

                var query = new InvoiceQuery();

                query.ledger = ledger;

                query.id = new int[1];

                query.id[0] = invoiceID;

                var cb = NewClearBooksClient();
                Task<listInvoicesResponse> response  = cb.listInvoicesAsync(auth, query);

                return response.Result.listInvoicesResult[0];

        }

        // POST: api/createInvoice
        /// <summary>
        /// Multi-currency field:
        /// 3 = USD
        /// 2 = EUR
        /// 1 = GBP
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [Route("api/createInvoice")]
        [HttpPost]
        public InvoiceReturn Post([FromBody]Invoice invoice)
        {
            var cb = NewClearBooksClient();
            
                var auth = new authenticate();
                auth.apiKey = GetAPIKeyHeader();

                Task<createInvoiceResponse> response =  cb.createInvoiceAsync(auth, invoice);

                return response.Result.createInvoiceResult;

        }

        /*
        // PUT: api/Invoice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Invoice/5
        public void Delete(int id)
        {
        }
        */
    }
}
