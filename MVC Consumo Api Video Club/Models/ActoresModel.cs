using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Consumo_Api_Video_Club.Models
{
    public class ActoresModel
    {
        public int idActores { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }

        //Externa al modelo
        public int SueldoActorEnPelicula { get; set; }

        //Necesaria para el uso de MVC
        public int idPelicula { get; set; }
    }
}