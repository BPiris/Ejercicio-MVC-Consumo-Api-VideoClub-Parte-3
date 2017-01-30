using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Consumo_Api_Video_Club.Controllers
{
    public class BaseController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (Session["idInstalacion"] == null && User.Identity.IsAuthenticated)
            //{
            //    Session.Clear();
            //    CustomPrincipal.Logout();
            //    filterContext.Result = new RedirectResult("/LogIn/Index");
            //}

            var cont = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var acc = filterContext.ActionDescriptor.ActionName;            

        }


    }
}