using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Consumo_Api_Video_Club.Models
{
    public class Actores_Peliculas_IncrementalModel
    {
        public int idActores_Peliculas { get; set; }
        public int idActores { get; set; }
        public int idPeliculas { get; set; }
        public int Sueldo { get; set; }
    }
}