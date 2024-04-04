using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using TaskManagerAPI.Data;
using TaskManagerAPI.Filters;
using Unity;
using Unity.AspNet.WebApi;

namespace TaskManagerAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Register global exception filter
            config.Filters.Add(new GlobalExceptionFilter());

            // configure DB Context
            //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //var dbContext = new DataContext();


            // Register DbContext with dependency resolver
            // No built-in DI container; so i used third-party libraries like Unity
            // config.DependencyResolver = new UnityDependencyResolver(new UnityContainer().RegisterInstance(dbContext));


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
