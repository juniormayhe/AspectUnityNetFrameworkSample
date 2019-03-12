using Application.Shared;
using Microsoft.Owin;
using System;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;

namespace WebApplicationFW
{
    /// <summary>
    /// 
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IUnityContainer GetConfiguredContainerApi()
        {
            return ContainerWebApi.Value;
        }

        /// <summary>
        /// The container web API
        /// </summary>
        private static readonly Lazy<IUnityContainer> ContainerWebApi = new Lazy<IUnityContainer>(() =>
        {
            IUnityContainer container = new UnityContainer();

            // add interception
            container.AddNewExtension<Interception>();
            container.RegisterType<IRepository, Repository>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<TenantInterceptor>());

            container.RegisterInstance(container);

            //container.RegisterType<IRepository, Repository>();

            container.RegisterType<IOwinContext, OwinContext>();

            container.RegisterType<HttpContextBase>(new InjectionFactory(p => new HttpContextWrapper(HttpContext.Current)));

            return container;
            
        });

        
    }
}