using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationFW.App_Start;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApplicationFW.App_Start
{
    public class SwaggerConfig
    {
        public static void Register() {
            GlobalConfiguration.Configuration
                  .EnableSwagger(c =>
                  {
                      c.UseFullTypeNameInSchemaIds();
                      c.MultipleApiVersions(
                          ResolveVersionSupportByRouteConstraint,
                          (vc) =>
                          {
                              vc.Version("v1", "Routing API Version 1");
                              //vc.Version("v0", "Test methods");
                          });
                      c.ResolveConflictingActions(apiDescs => apiDescs.First());
                      c.IncludeXmlComments(GetXmlCommentsPath());
                      c.DescribeAllEnumsAsStrings();
                  })
                  .EnableSwaggerUi(c =>
                  {
                      c.EnableDiscoveryUrlSelector();
                      c.DisableValidator();
                  });
        }

        private static bool ResolveVersionSupportByRouteConstraint(ApiDescription apiDesc, string targetApiVersion)
        {
            string namespaceVersion = targetApiVersion.Replace("v", "Version");
            string controllerNamespace = apiDesc.ActionDescriptor.ControllerDescriptor.ControllerType.Namespace;
            return controllerNamespace.Split('.').Any(x => x == namespaceVersion);
        }

        private static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\WebApplicationFW.xml", AppDomain.CurrentDomain.BaseDirectory);
        }
    }

}