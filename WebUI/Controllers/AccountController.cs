using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action(
                       "ConfirmEmail", "Account",
                       new { userId = user.Id, code = code },
                       protocol: Request.Url.Scheme);

                    await UserManager.SendEmailAsync(user.Id,
                       "Confirm your account",
                       "Hi, " + user.UserName + "! Please confirm your account by clicking this <a href=\""
                                                       + callbackUrl + "\">link.</a>");
                    // ViewBag.Link = callbackUrl;   // Used only for initial demo.
                    return View("_Register");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            AuthenticationManager.SignOut();
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password.");
                }
                else
                {
                    if (user.EmailConfirmed == true)
                    {
                        ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                        if (string.IsNullOrEmpty(model.ReturnUrl))
                        {
                            return RedirectToAction("List", "Products");
                        }
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email not confirmed.");
                    }
                }
            }
            ViewBag.returnUrl = model.ReturnUrl;
            return View(model);
        }
        public ActionResult Logout(string returnUrl)
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            return Redirect(returnUrl);
        }

        private IOrderUnitOfWork _orderUnitOfWork;
        public AccountController(IOrderUnitOfWork orderUnitOfWork)
        {
            _orderUnitOfWork = orderUnitOfWork;
        }

        
        public ViewResult Account()
        {
            //var ss = _orderUnitOfWork.Orders.GetAll().ToList().Where(x => x.UserId == User.Identity.GetUserId());

            //var model = (new OrdersViewModel
            //{
            //    Order = _orderUnitOfWork.Orders.GetAll().ToList().Where(x => x.UserId == User.Identity.GetUserId())

            //});

            //foreach(var item in ss)
            //{
            //    if(item.)
            //    (model.OrderDetails as List<OrderDetails>).Add()
            //}
            return View(new OrdersViewModel
            {
                Order = _orderUnitOfWork.Orders.GetAll().ToList().Where(x => x.UserId == User.Identity.GetUserId()),
                OrderDetails = _orderUnitOfWork.OrderDetails.GetAll()
            });
        }

        [HttpPost]
        [ActionName("removeOrder")]
        public ViewResult RemoveOrder(int orderId)
        {
            Order order = _orderUnitOfWork.Orders.GetAll().FirstOrDefault(item => item.Id == orderId);

            if (order != null)
            {
                foreach (var item in _orderUnitOfWork.OrderDetails.GetAll().Where(x => x.OrderId == order.Id))
                {
                    _orderUnitOfWork.OrderDetails.Delete(item);
                }
                _orderUnitOfWork.OrderDetails.Save();


                _orderUnitOfWork.Orders.Delete(order);
                _orderUnitOfWork.Orders.Save();
            }

            return Account();
        }
    }
}