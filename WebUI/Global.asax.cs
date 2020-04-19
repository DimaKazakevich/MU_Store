using Domain.Entities;
using Ninject;
using System.Web.Mvc;
using System.Web.Routing;
using WebUI.App_Start;
using WebUI.Infrastructure.Binders;
using System.Web.Optimization;

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

            //use class BasketModelBinder for create instanses Basket
            ModelBinders.Binders.Add(typeof(Basket), new BasketModelBinder());
        }
    }
}
