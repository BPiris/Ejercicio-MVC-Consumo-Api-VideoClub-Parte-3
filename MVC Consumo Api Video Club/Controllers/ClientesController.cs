﻿using System;
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
    public class ClientesController : BaseController
    {
        [Dependency]
        public Servicios<ClientesModel> _Clientes { get; set; }

        [Dependency]
        public Servicios<PeliculasModel> _Peliculas { get; set; }

        // GET: Clientes
        public ActionResult Index()
        {
            return View(_Clientes.Get((String)Session["usuarioLogin"], (String)Session["passLogin"]));
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            var miDic = new Dictionary<String,String>() { {"peliculasSinALquilar", true.ToString()} };
            var peliculasLibre = _Peliculas.Get(miDic, (String)Session["usuarioLogin"], (String)Session["passLogin"]);

            ViewBag.ListadoLibres = new SelectList(peliculasLibre, "idPelicula", "nombrePelicula");

            return View(_Clientes.Get(id, (String)Session["usuarioLogin"], (String)Session["passLogin"]));
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        public async Task<ActionResult> Create(ClientesModel cliente)
        {
            try
            {
                await _Clientes.Add(cliente, (String)Session["usuarioLogin"], (String)Session["passLogin"]);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_Clientes.Get(id, (String)Session["usuarioLogin"], (String)Session["passLogin"]));
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ClientesModel editarCliente)
        {
            try
            {
                await _Clientes.Update(editarCliente, (String)Session["usuarioLogin"], (String)Session["passLogin"]);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_Clientes.Delete(id, (String)Session["usuarioLogin"], (String)Session["passLogin"]));
        }

        public async Task<ActionResult> DevolverPeliculas(int idPeliculaDevolver)
        {
            var peliculaTemp = _Peliculas.Get(idPeliculaDevolver, (String)Session["usuarioLogin"], (String)Session["passLogin"]);
            var clienteTemp = (int)peliculaTemp.idCliente;
            peliculaTemp.idCliente = null;
            await _Peliculas.Update(peliculaTemp, (String)Session["usuarioLogin"], (String)Session["passLogin"]);

            return RedirectToAction("Details",new {id=clienteTemp});
        }

        public async Task<ActionResult> AlquilarPeliculas(FormCollection coleccion)
        {
            int idCliente = Convert.ToInt32(coleccion["idCliente"]);
            int idPeliculaAlquiler = Convert.ToInt32(coleccion["idPeliculaAlquiler"]);


            var peliculaTemp = _Peliculas.Get(idPeliculaAlquiler, (String)Session["usuarioLogin"], (String)Session["passLogin"]);
            peliculaTemp.idCliente = idCliente;
            await _Peliculas.Update(peliculaTemp, (String)Session["usuarioLogin"], (String)Session["passLogin"]);

            return RedirectToAction("Details", new { id = idCliente });
        }

    }
}
