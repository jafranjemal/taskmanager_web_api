using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TaskManagerAPI.Data;
using Unity.Injection;
using Unity;
using Serilog;

namespace TaskManagerAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()            
            .CreateLogger();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
