using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Consumo_Api_Video_Club.Models
{
    public class UsuarioModel
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string passwordUsuario { get; set; }
    }
}