using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace WebApplicationFW
{
    /// <summary>
    /// 
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// Initialize Unity container and other components
        /// </summary>
        /// <returns></returns>
        public static IUnityContainer Initialise()
        {
            var container = UnityConfig.GetConfiguredContainerApi();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            return container;
        }
    }
}