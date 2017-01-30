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
    public class ActoresController : BaseController
    {
        [Dependency]
        public Servicios<ActoresModel> _Actores { get; set; }
        // GET: Actores
        public ActionResult Index()
        {
            return View(_Actores.Get());
        }

        // GET: Actores/Details/5
        public ActionResult Details(int id, int? idPelicula, int? sueldo)
        {
            var actor = _Actores.Get(id);

            if (idPelicula != null)
                actor.idPelicula = (int)idPelicula;
            if (sueldo != null)
                actor.SueldoActorEnPelicula = (int)sueldo;

            return View(actor);
        }

        // GET: Actores/Create
        public ActionResult Create(int? idPelicula)
        {
            var actor = new ActoresModel();

            if (Session["ActorCreado"] != null)
                actor = (ActoresModel)Session["ActorCreado"];

            if (idPelicula != null)
                actor.idPelicula = (int)idPelicula;

            return View(actor);
        }

        // POST: Actores/Create
        [HttpPost]
        public async Task<ActionResult> Create(ActoresModel collection)
        {
            Session["ActorCreado"] = collection;

            try
            {
                await _Actores.Add(collection);
                Session["ActorCreado"] = null;
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        // GET: Actores/Edit/5
        public ActionResult Edit(int id, int? idPelicula, int? sueldo)
        {
            var actor = _Actores.Get(id);

            if (idPelicula != null) 
                actor.idPelicula = (int)idPelicula;
            if (sueldo != null)
                actor.SueldoActorEnPelicula = (int) sueldo;

            return View(actor);
        }

        // POST: Actores/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ActoresModel collection)
        {
            try
            {
                await _Actores.Update(collection);
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        // GET: Actores/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_Actores.Get(id));
        }

    }
}
