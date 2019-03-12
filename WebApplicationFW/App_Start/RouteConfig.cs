using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplicationFW
{
    public static class RouteConfig
    {
        /// <summary>
        /// Setup all the routes exposed by the Routing Api
        /// </summary>
        /// <param name="config"></param>
        public static void Setup(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
            name: "swagger_root",
            routeTemplate: string.Empty,
            defaults: null,
            constraints: null,
            handler: new Swashbuckle.Application.RedirectHandler(message => message.RequestUri.ToString(), "swagger"));
            config.MapHttpAttributeRoutes();
        }
    }
}
