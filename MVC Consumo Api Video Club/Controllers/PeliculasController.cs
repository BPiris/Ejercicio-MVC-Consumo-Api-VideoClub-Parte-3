using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MVC_Consumo_Api_Video_Club.Models;
using MVC_Consumo_Api_Video_Club.Servicios;

namespace MVC_Consumo_Api_Video_Club.Controllers
{
    public class PeliculasController : BaseController
    {
        [Dependency]
        public Servicios<PeliculasModel> _Peliculas { get; set; }

        // GET: Peliculas
        public ActionResult Index()
        {
            ViewBag.desplegableAno = new SelectList(ObtenerListadoAnos(), "idAno", "ano");            

            return View(Session["listaTemporal"] == null ? _Peliculas.Get((String)Session["usuarioLogin"], (String)Session["passLogin"]) : Session["listaTemporal"] as List<PeliculasModel>);
        }

        // GET: Peliculas/Details/5
        public ActionResult Details(int id)
        {
            Session["idPelicula"] = id;
            return View(_Peliculas.Get(id, (String)Session["usuarioLogin"], (String)Session["passLogin"]));
        }

        // GET: Peliculas/Create
        public ActionResult Create()
        {
            var pelicula = new PeliculasModel();

            if (Session["PeliculaCreada"] != null)
                pelicula = (PeliculasModel) Session["PeliculaCreada"];

            return View(pelicula);
        }

        // POST: Peliculas/Create
        [HttpPost]
        public async Task<ActionResult> Create(PeliculasModel collection)
        {
            Session["PeliculaCreada"] = collection;

            try
            {
                await _Peliculas.Add(collection, (String)Session["usuarioLogin"], (String)Session["passLogin"]);
                Session["PeliculaCreada"] = null;
            }
            catch
            {

            }
            return RedirectToAction("Index");

        }

        // GET: Peliculas/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_Peliculas.Get(id, (String)Session["usuarioLogin"], (String)Session["passLogin"]));
        }

        // POST: Peliculas/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, PeliculasModel collection)
        {
            try
            {
                await _Peliculas.Update(collection, (String)Session["usuarioLogin"], (String)Session["passLogin"]);
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        // GET: Peliculas/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_Peliculas.Get(id, (String)Session["usuarioLogin"], (String)Session["passLogin"]));
        }

        // POST: Peliculas/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, PeliculasModel collection)
        {
            try
            {
                await _Peliculas.Delete(collection.idPelicula, (String)Session["usuarioLogin"], (String)Session["passLogin"]);
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult BuscarEnPeliculas(String txtBusquedaPelicula, int? idAno)
        {
            var miDic = new Dictionary<String,String>() { { "txtBusquedaPelicula", txtBusquedaPelicula }, { "anoPelicula", idAno.ToString()} };
            Session["listaTemporal"] = _Peliculas.Get(miDic, (String)Session["usuarioLogin"], (String)Session["passLogin"]);
            return RedirectToAction("Index");
        }
    }
}
