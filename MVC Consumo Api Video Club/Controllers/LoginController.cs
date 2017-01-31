using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Practices.Unity;
using MVC_Consumo_Api_Video_Club.App_Start;
using MVC_Consumo_Api_Video_Club.Models;
using MVC_Consumo_Api_Video_Club.Seguridad;
using MVC_Consumo_Api_Video_Club.Servicios;

namespace MVC_Consumo_Api_Video_Club.Controllers
{

    [AllowAnonymous]
    public class LoginController : Controller
    {
        [Dependency]
        public Servicios<UsuarioModel> _Usuarios { get; set; }

        private bool ComprobarUsuario(String nombreUsuario, String password)
        {
            var miDic = new Dictionary<String, String>() { { "username", nombreUsuario }, { "password", Cifrado.GetSHA1(password) } };

            Session["usuarioLogin"] = nombreUsuario;
            Session["passLogin"] = Cifrado.GetSHA1(password);

            var usuarioTemp = _Usuarios.Get(miDic,(String)Session["usuarioLogin"], (String)Session["passLogin"]);

            return usuarioTemp.Any();
        }

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

                //if (Membership.ValidateUser(nombreUsuario, passwordUsuario))
                if (ComprobarUsuario(nombreUsuario, passwordUsuario))
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
