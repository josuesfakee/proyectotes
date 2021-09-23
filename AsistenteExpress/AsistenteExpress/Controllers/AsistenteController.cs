using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsistenteExpress.Controllers
{
    public class AsistenteController : Controller
    {
        // GET: Asistente
        public ActionResult Index()
        {
            return View();
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
