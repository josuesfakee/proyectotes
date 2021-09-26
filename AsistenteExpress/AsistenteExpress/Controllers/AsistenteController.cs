using AsistenteExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsistenteExpress.Controllers
{
    //Just test
    public class AsistenteController : Controller
    {
        private EdenredContext _con = new EdenredContext();
        // GET: Asistente
        public ActionResult Index()
        {
            Proceso proceso = new Proceso();
            proceso.Campañas = _con.Campanias.ToList().ConvertAll(Campaña =>
            {
                return new SelectListItem()
                {
                    Text = Campaña.Descripcion.ToString(),
                    Value = Campaña.Id.ToString(),
                    Selected = false
                };
            });
            proceso.Perfiles = _con.Perfiles.ToList().ConvertAll(Campaña =>
            {
                return new SelectListItem()
                {
                    Text = Campaña.Descripcion.ToString(),
                    Value = Campaña.Id.ToString(),
                    Selected = false
                };
            });
            proceso.Motivos = _con.Motivos.ToList().ConvertAll(Campaña =>
            {
                return new SelectListItem()
                {
                    Text = Campaña.Descripcion.ToString(),
                    Value = Campaña.Id.ToString(),
                    Selected = false
                };
            });
            proceso.SubMotivos = _con.Submotivos.ToList().ConvertAll(Campaña =>
            {
                return new SelectListItem()
                {
                    Text = Campaña.Descripcion.ToString(),
                    Value = Campaña.Id.ToString(),
                    Selected = false
                };
            });

            return View(proceso);
        }

        // GET: Asistente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Asistente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asistente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asistente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Asistente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asistente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Asistente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
