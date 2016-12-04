using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using DAL_RememberWords.Initializators;

namespace RememberWords
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new WordInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
