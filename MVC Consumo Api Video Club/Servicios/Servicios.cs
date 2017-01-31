using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using MVC_Consumo_Api_Video_Club.Seguridad;
using MVC_Consumo_Api_Video_Club.Utilidades;

namespace MVC_Consumo_Api_Video_Club.Servicios
{
    public class Servicios<TModel> : IServicios<TModel>
    {
        private String _urlBase;

        public String UrlBase
        {
            get { return _urlBase; }
            set { _urlBase = value; }
        }

        public Servicios(String url)
        {
            UrlBase = url;
        }

        public async Task Add(TModel modelo, String user, String pass)
        {
            var serializado = Serializacion<TModel>.Serializar(modelo);

            using (var handle = new HttpClientHandler())
            {
                handle.Credentials = new NetworkCredential(user, Cifrado.GetSHA1(pass));

                using (var client = new HttpClient(handle))
                {
                    var contenido = new StringContent(serializado);
                    contenido.Headers.ContentType =
                        new MediaTypeHeaderValue("application/json");
                    await client.PostAsync(new Uri(UrlBase), contenido);
                }
            }
        }

        public async Task Delete(int id, String user, String pass)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.Credentials = new NetworkCredential(user, Cifrado.GetSHA1(pass));

                using (var client = new HttpClient(handler))
                {
                    var res = await client.DeleteAsync(new Uri(UrlBase + "/" + id));

                    var miobjeto = await res.Content.ReadAsStreamAsync();

                    using (var miStream = new StreamReader(miobjeto))
                    {
                        var resultado = miStream.ReadToEnd();
                        Serializacion<int>.Deserializar(resultado);
                    }
                }
            }
        }

        public List<TModel> Get(String user, String pass)
        {
            List<TModel> lista;
            var peticion = WebRequest.Create(UrlBase);
            peticion.Method = "GET";

            String encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(user + ":" + pass));
            peticion.Headers.Add("Authorization", "Basic " + encoded);

            var res = peticion.GetResponse();

            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    lista = Serializacion<List<TModel>>.Deserializar(resultado);
                }

            }
            return lista;
        }

        public List<TModel> Get(Dictionary<string, string> args, String user, String pass)
        {
            var argumentos = "?";
            var n = args.Count;
            var i = 1;

            foreach (var arg in args.Keys)
            {
                argumentos += arg + "=" + args[arg];

                if (i < n)
                {
                    argumentos += "&";
                    i++;
                }
            }

            List<TModel> lista;

            var cl = WebRequest.Create(UrlBase + argumentos);

            cl.Method = "GET";
            
            String encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(user + ":" + pass));
            cl.Headers.Add("Authorization", "Basic " + encoded);
            
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    lista = Serializacion<List<TModel>>.Deserializar(resultado);
                }
            }
            return lista;
        }

        public TModel Get(int id, String user, String pass)
        {
            TModel lista;

            var cl = WebRequest.Create(UrlBase + "/" + id);

            cl.Method = "GET";

            String encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(user + ":" + pass));
            cl.Headers.Add("Authorization", "Basic " + encoded);

            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    lista = Serializacion<TModel>.Deserializar(resultado);
                }
            }
            return lista;
        }

        public async Task Update(TModel modelo, String user, String pass)
        {
            var serializado = Serializacion<TModel>.Serializar(modelo);
            using (var handler = new HttpClientHandler())
            {
                handler.Credentials = new NetworkCredential(user, Cifrado.GetSHA1(pass));

                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(serializado);

                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var res = await client.PutAsync(new Uri(UrlBase), contenido);

                    var miobj = await res.Content.ReadAsStreamAsync();

                    using (var mistream = new StreamReader(miobj))
                    {
                        var resultado = mistream.ReadToEnd();
                        Serializacion<int>.Deserializar(resultado);
                    }
                }
            }
        }
    }
}