using Ninject;
using System.Web.Mvc;

namespace WebUI.App_Start
{
    public class NinjectWebCommon
    {
        public static void RegisterServices(IKernel kernel)
        {
            DependencyResolver.SetResolver(new Infrastructure.NinjectDependencyResolver(kernel));
        }
    }
}