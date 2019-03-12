using Application.Shared;
using Application.Shared.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApplicationFW.Controllers.Version1
{
    /// <summary>
    /// merchants
    /// </summary>
    // http://localhost:47354/merchants/tenant/10000
    [RoutePrefix("merchants")]
    public class MerchantsController : ApiController
    {
        private readonly IRepository repository;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repository"></param>
        public MerchantsController(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Get merchants by tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Merchant>))]
        [HttpGet, Route("tenant/{tenantId}"), ResponseType(typeof(IEnumerable<Merchant>))]
        public IHttpActionResult GetByTenant(int tenantId)
        {
            var list = repository.GetMerchants(tenantId) ?? new List<Merchant>();

            return this.Ok(list.ToList());
        }
    }
}