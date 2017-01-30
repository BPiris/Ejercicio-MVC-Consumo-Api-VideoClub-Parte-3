using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MVC_Consumo_Api_Video_Club.Seguridad
{
    public class CustomIdentity: IIdentity
    {
        public string Name { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
        public IIdentity Identidad { get; set; }

        public CustomIdentity(IIdentity identidad)
        {
            Identidad = identidad;
        }
    }
}