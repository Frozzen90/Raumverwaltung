﻿using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using Raumverwaltung.Controllers;

namespace Raumverwaltung
{
    public class Global : HttpApplication
    {
        private static MainController _MainController;

        public static MainController cMainController { get => _MainController; set => _MainController = value; }

        void Application_Start(object sender, EventArgs e)
        {
            cMainController = new MainController();
            cMainController.LadeDaten(true);
            // Code, der beim Anwendungsstart ausgeführt wird
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}