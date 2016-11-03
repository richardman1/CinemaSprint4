using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Diagnostics.CodeAnalysis;
using WebMatrix.WebData;
using System.Web.Routing;
using System.Web.Optimization;
using IVH7_Cinema.WebUI.Website.App_Start;


namespace IVH7_Cinema.WebUI.Website
{
    [ExcludeFromCodeCoverage]
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            WebSecurity.InitializeDatabaseConnection("EFDbContext", "UserProfile", "UserId", "Username", true);
        }
    }
}
