using Domain.Entities;
using System.Web.Mvc;

namespace WebUI.Infrastructure.Binders
{
    public class BasketModelBinder : IModelBinder
    {
        private const string sessionKey = "Basket";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get basket object from session
            Basket basket = null;
            if (controllerContext.HttpContext.Session != null)
            {
                basket = (Basket)controllerContext.HttpContext.Session[sessionKey];
            }

            // create basket oject if it not found the session
            if (basket == null)
            {
                basket = new Basket();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = basket;
                }
            }

            return basket;
        }
    }
}