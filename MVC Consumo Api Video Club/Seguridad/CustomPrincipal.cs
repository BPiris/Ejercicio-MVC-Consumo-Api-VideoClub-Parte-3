using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace MVC_Consumo_Api_Video_Club.Seguridad
{
    public class CustomPrincipal : IPrincipal
    {

        private CustomPrincipal() { }

        public CustomPrincipal(IIdentity identidad)
        {
            Identity = identidad;
        }

        public IIdentity Identity { get; private set; }

        public CustomIdentity CustomIdentity
        {
            get { return (CustomIdentity)Identity; }
            set { Identity = value; }
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();

            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(""), new string[] { });
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}