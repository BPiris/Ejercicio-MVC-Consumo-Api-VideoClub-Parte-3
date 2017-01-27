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
    public class PeliculasController : Controller
    {
        [Dependency]
        public Servicios<PeliculasModel> _Peliculas { get; set; }

        // GET: Peliculas
        public ActionResult Index()
        {
            return View(_Peliculas.Get());
        }

        // GET: Peliculas/Details/5
        public ActionResult Details(int id)
        {
            Session["idPelicula"] = id;
            return View(_Peliculas.Get(id));
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
                await _Peliculas.Add(collection);
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
            return View(_Peliculas.Get(id));
        }

        // POST: Peliculas/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, PeliculasModel collection)
        {
            try
            {
                await _Peliculas.Update(collection);
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        // GET: Peliculas/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_Peliculas.Get(id));
        }

        // POST: Peliculas/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, PeliculasModel collection)
        {
            try
            {
                await _Peliculas.Delete(collection.idPelicula);
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }
    }
}
