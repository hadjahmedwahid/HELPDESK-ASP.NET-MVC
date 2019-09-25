﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HelpDesk
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string con = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            //Start SqlDependency with application initialization
            SqlDependency.Start(con);
        }

        protected void Application_End()
        {
            //Stop SQL dependency
            SqlDependency.Stop(con);
        }
    }
}
