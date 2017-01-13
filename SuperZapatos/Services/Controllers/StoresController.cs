using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services.Models;
using WebApiBasicAuth.Filters;

namespace Services.Controllers
{
   
    public class StoresController : ApiController
    {
        private ManagerContext db = new ManagerContext();
        
        // GET api/stores
        [IdentityBasicAuthentication]
        [Authorize]
        public StoresApiCollection Get()
        {
            var result = new StoresApiCollection();
            try
            {
                result.stores = db.Stores.ToArray();
                result.success = true;
                result.total_elements = db.Stores.Count();
            }
            catch (Exception excp) {
                result.success = false;
                result.error_code = 0;
                result.error_msg = excp.Message;
            }
            return result;
        }

        // GET api/stores/5
        [IdentityBasicAuthentication]
        [Authorize]
        public StoreApiCollection Get(string id)
        {
            var result = new StoreApiCollection();
            result.success = false;

            int n;
            bool isNumeric = int.TryParse(id, out n);

            if (isNumeric)
            {
                result.store = db.Stores.SingleOrDefault(s => s.id == n);

                if (result.store != null)
                {
                    result.success = true;
                }
                else
                {
                    result.error_code = 404;
                    result.error_msg = ServiceError.Response[404];
                }
            }
            else {
                result.error_code = 400;
                result.error_msg = ServiceError.Response[400];
            }

            return result;
        }

    }
}
