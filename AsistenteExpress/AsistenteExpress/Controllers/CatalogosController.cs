using AsistenteExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AsistenteExpress.Controllers
{
    public class CatalogosModel { public List<SelectListItem> Catalogos { get; set; } }
    public class CatalogosController : Controller
    {
        private EdenredContext _con = new EdenredContext();
        // GET: Catalogos
        public ActionResult Index()
        {
            CatalogosModel model = new CatalogosModel();
            model.Catalogos = new List<SelectListItem>();
            model.Catalogos.Add(new SelectListItem()
            {
                Text = "Compañias",
                Value = "1",
                Selected = false
            });
            model.Catalogos.Add(new SelectListItem()
            {
                Text = "Perfiles",
                Value = "2",
                Selected = false
            });
            model.Catalogos.Add(new SelectListItem()
            {
                Text = "Tipos",
                Value = "3",
                Selected = false
            });
            model.Catalogos.Add(new SelectListItem()
            {
                Text = "Motivos",
                Value = "4",
                Selected = false
            });
            model.Catalogos.Add(new SelectListItem()
            {
                Text = "Submotivos",
                Value = "5",
                Selected = false
            });
            return View(model);
        }

        // GET: Catalogos/Details/5
        public ActionResult Details(int idCatalogo)
        {
            List<Catalogos> catalogos = new List<Catalogos>();
            
            switch (idCatalogo)
            {
                case 1:
                    catalogos.AddRange(_con.Campanias.Where(x => x.Estatus).ToList());
                    break;
                case 2:
                    catalogos.AddRange(_con.Perfiles.Where(x => x.Estatus).ToList());
                    break;
                case 3:
                    catalogos.AddRange(_con.Tipos.Where(x => x.Estatus).ToList());
                    break;
                case 4:
                    catalogos.AddRange(_con.Motivos.Where(x => x.Estatus).ToList());
                    break;
                case 5:
                    catalogos.AddRange(_con.Submotivos.Where(x => x.Estatus).ToList());
                    break;
                default:
                    catalogos.AddRange(_con.Campanias.Where(x => x.Estatus).ToList());
                    catalogos.AddRange(_con.Perfiles.Where(x => x.Estatus).ToList());
                    catalogos.AddRange(_con.Tipos.Where(x => x.Estatus).ToList());
                    catalogos.AddRange(_con.Motivos.Where(x => x.Estatus).ToList());
                    catalogos.AddRange(_con.Submotivos.Where(x => x.Estatus).ToList());
                    break;
            }
            return Json(catalogos, JsonRequestBehavior.AllowGet);
        }

        // GET: Catalogos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalogos/Create
        [HttpPost]
        public ActionResult Create(CatalogosTransport collection)
        {
            try
            {
                Catalogos catalogos = null;
                if (collection.Id == 0)
                {
                    switch (collection.IdCatalogo)
                    {
                        case 1:
                            Campanias campaña = new Campanias();
                            campaña.Descripcion = collection.Descripcion;
                            campaña.Estatus = true;
                            _con.Campanias.Add(campaña);
                            catalogos = campaña;
                            break;
                        case 2:
                            Perfiles perfil = new Perfiles();
                            perfil.Descripcion = collection.Descripcion;
                            perfil.Estatus = true;
                            _con.Perfiles.Add(perfil);
                            catalogos = perfil;
                            break;
                        case 3:
                            Tipos tipo = new Tipos();
                            tipo.Descripcion = collection.Descripcion;
                            tipo.Estatus = true;
                            _con.Tipos.Add(tipo);
                            catalogos = tipo;
                            break;
                        case 4:
                            Motivos motivo = new Motivos();
                            motivo.Descripcion = collection.Descripcion;
                            motivo.Estatus = true;
                            _con.Motivos.Add(motivo);
                            catalogos = motivo;
                            break;
                        case 5:
                            Submotivos submotivos = new Submotivos();
                            submotivos.Descripcion = collection.Descripcion;
                            submotivos.Estatus = true;
                            _con.Submotivos.Add(submotivos);
                            catalogos = submotivos;
                            break;
                    }
                }
                else //is edit
                {
                    switch (collection.IdCatalogo)
                    {
                        case 1:
                            Campanias campaña = _con.Campanias.Where(x=>x.Id == collection.Id).SingleOrDefault();
                            campaña.Descripcion = collection.Descripcion;
                            campaña.Estatus = true;
                            catalogos = campaña;
                            break;
                        case 2:
                            Perfiles perfil = _con.Perfiles.Where(x => x.Id == collection.Id).SingleOrDefault();
                            perfil.Descripcion = collection.Descripcion;
                            perfil.Estatus = true;
                            catalogos = perfil;
                            break;
                        case 3:
                            Tipos tipo =  _con.Tipos.Where(x => x.Id == collection.Id).SingleOrDefault();
                            tipo.Descripcion = collection.Descripcion;
                            tipo.Estatus = true;
                            catalogos = tipo;
                            break;
                        case 4:
                            Motivos motivo = _con.Motivos.Where(x => x.Id == collection.Id).SingleOrDefault();
                            motivo.Descripcion = collection.Descripcion;
                            motivo.Estatus = true;
                            _con.Motivos.Add(motivo);
                            catalogos = motivo;
                            break;
                        case 5:
                            Submotivos submotivos = _con.Submotivos.Where(x => x.Id == collection.Id).SingleOrDefault();
                            submotivos.Descripcion = collection.Descripcion;
                            submotivos.Estatus = true;
                            catalogos = submotivos;
                            break;
                    }
                }
                
                _con.SaveChanges();

                return Json(catalogos);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }

        // GET: Catalogos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Catalogos/Edit/5
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

        // GET: Catalogos/Delete/5
        public ActionResult Delete(int IdCatalogo, int Id)
        {
            switch (IdCatalogo)
            {
                case 1:
                    Campanias campañia = _con.Campanias.Where(x => x.Id == Id).SingleOrDefault();
                    _con.Campanias.Remove(campañia);
                    break;
                case 2:
                    Perfiles perfil = _con.Perfiles.Where(x => x.Id == Id).SingleOrDefault();
                    _con.Perfiles.Remove(perfil);
                    break;
                case 3:
                    Tipos tipo = _con.Tipos.Where(x => x.Id == Id).SingleOrDefault();
                    _con.Tipos.Remove(tipo);
                    break;
                case 4:
                    Motivos motivo = _con.Motivos.Where(x => x.Id == Id).SingleOrDefault();
                    _con.Motivos.Remove(motivo);
                    break;
                case 5:
                    Submotivos submotivos = _con.Submotivos.Where(x => x.Id == Id).SingleOrDefault();
                    _con.Submotivos.Remove(submotivos);
                    break;
            }
            _con.SaveChanges();
            return Json(new { response = "ok" }, JsonRequestBehavior.AllowGet);
        }

        // POST: Catalogos/Delete/5
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

    public class CatalogosTransport : Catalogos
    {
        public int IdCatalogo { get; set; }
    }
}
