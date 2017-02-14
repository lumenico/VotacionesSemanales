using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Votaciones.Core;

namespace Votaciones
{
    public class Global : HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SemanasObserver.semanaHandler();
        }                
        void Session_Start(object sender, EventArgs e)
        {
            var username = Context.User.Identity.Name.Split('\\')[1];
            CurrentUser.set(username);
            
        }
    }
}