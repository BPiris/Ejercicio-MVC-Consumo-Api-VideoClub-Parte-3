using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Consumo_Api_Video_Club.Models.Externos;

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

        public List<AnosModel> ObtenerListadoAnos()
        {
            var listaAnos = new List<AnosModel>();

            if (Session["listaAnos"] == null)
            {
                var idAno = 0;
                for (int i = 1920; i < 2017; i++)
                {
                    var ano = new AnosModel()
                    {
                        idAno = i,
                        ano = i.ToString()
                    };
                    listaAnos.Add(ano);
                }
                Session["listaAnos"] = listaAnos;
            }
            else
            {
                listaAnos = Session["listaAnos"] as List<AnosModel>;
            }

            return listaAnos;
        }

        public void GenerarSessionUsuario()
        {
            
        }


    }
}