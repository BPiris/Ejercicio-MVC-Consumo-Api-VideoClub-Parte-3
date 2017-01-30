using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_Consumo_Api_Video_Club.App_Start;
using MVC_Consumo_Api_Video_Club.Seguridad;

namespace MVC_Consumo_Api_Video_Club.Controllers
{

    [AllowAnonymous]
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(String nombreUsuario, String passwordUsuario)
        {
            ViewBag.errorLoginIncorrecto = "false";

            if (ModelState.IsValid)
            {

                if (Membership.ValidateUser(nombreUsuario, passwordUsuario))
                {
                    UnityWebActivator.Start();

                    FormsAuthentication.RedirectFromLoginPage(nombreUsuario, false);                    
                }
                else
                {
                    ViewBag.errorLoginIncorrecto = "true";
                    ModelState.AddModelError("", "Datos de autenticacion incorrectos.");
                }
            }

            return View();
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            CustomPrincipal.Logout();

            return RedirectToAction("Index", "Login");
        }

    }
}
