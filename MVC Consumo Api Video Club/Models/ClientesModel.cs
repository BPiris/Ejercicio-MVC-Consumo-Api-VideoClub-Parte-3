using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Consumo_Api_Video_Club.Models
{
    public class ClientesModel
    {
        public int idCliente { get; set; }
        public string nombreCliente { get; set; }
        public string apellidosCliente { get; set; }

        //Externas al Modelo
        public List<PeliculasModel> PeliculasCliente { get; set; }

        public int idPeliculaAlquiler { get; set; }
    }
}