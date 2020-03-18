using Ninject;
using System.Web.Mvc;
using System.Web.Routing;
using WebUI.App_Start;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var kernel = new StandardKernel();
            NinjectWebCommon.RegisterServices(kernel);
        }
    }
}
