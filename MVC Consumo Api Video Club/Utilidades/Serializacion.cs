using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MVC_Consumo_Api_Video_Club.Utilidades
{
    public class Serializacion<T>
    {
        public static T Deserializar(String obj)
        {
            var ser = new JavaScriptSerializer();
            var objres = ser.Deserialize<T>(obj);
            return objres;
        }

        public static String Serializar(T obj)
        {
            var ser = new JavaScriptSerializer();
            var objres = ser.Serialize(obj);
            return objres;
        }
    }
}