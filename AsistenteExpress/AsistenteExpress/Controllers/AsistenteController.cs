using AsistenteExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AsistenteExpress.Controllers
{
    //Just test    
    public class AsistenteController : Controller
    {
        private EdenredContext _con = new EdenredContext();
        // GET: Asistente
        
        public ActionResult Index()
        {
            //ADMIN = El BARTO
            //USUARIO = USER
            Session["user"] = "El Barto"; 
            Proceso proceso = new Proceso();
            proceso.Campañas = _con.Campanias.Where(x => x.Estatus).ToList().ConvertAll(Campaña =>
            {
                return new SelectListItem()
                {
                    Text = Campaña.Descripcion.ToString(),
                    Value = Campaña.Id.ToString(),
                    Selected = false
                };
            });
            proceso.Perfiles = _con.Perfiles.Where(x => x.Estatus).ToList().ConvertAll(Campaña =>
            {
                return new SelectListItem()
                {
                    Text = Campaña.Descripcion.ToString(),
                    Value = Campaña.Id.ToString(),
                    Selected = false
                };
            });
            proceso.Motivos = _con.Motivos.Where(x => x.Estatus).ToList().ConvertAll(Campaña =>
            {
                return new SelectListItem()
                {
                    Text = Campaña.Descripcion.ToString(),
                    Value = Campaña.Id.ToString(),
                    Selected = false
                };
            });
            proceso.SubMotivos = _con.Submotivos.Where(x => x.Estatus).ToList().ConvertAll(Campaña =>
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

        // GET: Asistente/Procesos/5
        public ActionResult Procesos(int aIdCampania, int? aIdPerfil = null, int?aIdMotivo = null, int? aIdSubMotivos = null)
        => Json(_con.Procesos.Where(x => x.Estatus && x.IdCampaña == aIdCampania && x.IdPerfil == aIdPerfil && x.IdMotivo == aIdMotivo && x.IdSubMotivo == aIdSubMotivos).ToList(), JsonRequestBehavior.AllowGet);        

        // GET: Asistente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asistente/Create
        [HttpPost]
        public ActionResult Create(Proceso Model)
        {
            try
            {
                Procesos procesoN = new Procesos();
                procesoN.Id = 0;
                procesoN.Asunto = Model.Asunto;
                procesoN.IdCampaña = Model.IdCampania;
                procesoN.IdPerfil = Model.IdPerfil;
                procesoN.IdMotivo = Model.IdMotivo;
                procesoN.IdSubMotivo = Model.IdSubMotivo;
                procesoN.Estatus = true;
                _con.Procesos.Add(procesoN);
                _con.SaveChanges();
                return Json(procesoN);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        // GET: Asistente/Edit/5
        public ActionResult Edit(int aIdProceso, string aNombreProceso, int aIdCampania, int? aIdPerfil = null, int? aIdMotivo = null, int? aIdSubMotivos = null)
        {
            Procesos proceso = _con.Procesos.Where(x => x.Id == aIdProceso).SingleOrDefault();
            proceso.Asunto = aNombreProceso;
            _con.SaveChanges();
            return Json(_con.Procesos.Where(x => x.Estatus && x.IdCampaña == aIdCampania && x.IdPerfil == aIdPerfil && x.IdMotivo == aIdMotivo && x.IdSubMotivo == aIdSubMotivos).ToList(), JsonRequestBehavior.AllowGet);
        }

        // POST: Asistente/Edit/
        [HttpPost]
        public ActionResult Edit(Proceso Model)
        {
            try
            {
                Procesos proceso = _con.Procesos.Where(x => x.Id == Model.IdProceso).SingleOrDefault();
                proceso.Asunto = Model.Asunto;
                proceso.IdCampaña = Model.IdCampania;
                proceso.IdPerfil = Model.IdPerfil;
                proceso.IdMotivo = Model.IdMotivo;
                proceso.IdSubMotivo = Model.IdSubMotivo;
                proceso.Estatus = true;
                _con.SaveChanges();
                return Json(proceso);
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }

        // GET: Asistente/Delete/5
        public ActionResult Delete(int aIdProceso, int aIdCampania, int? aIdPerfil = null, int? aIdMotivo = null, int? aIdSubMotivos = null)
        {
            Procesos proceso = _con.Procesos.Where(x=>x.Id == aIdProceso).SingleOrDefault();
            if (proceso.Estatus)
            {
                proceso.Estatus = false;
                _con.SaveChanges();
            }            
            return Json(_con.Procesos.Where(x => x.Estatus && x.IdCampaña == aIdCampania && x.IdPerfil == aIdPerfil && x.IdMotivo == aIdMotivo && x.IdSubMotivo == aIdSubMotivos).ToList(), JsonRequestBehavior.AllowGet);
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

        #region EXAMPLE Upload FILE
        //public ActionResult UploadFileExample()
        //{
        //    try
        //    {
        //        if (Request.Files.Count > 0)
        //        {
        //            var root = "~/Content/Temp/";

        //            bool folderpath = Directory.Exists(HttpContext.Server.MapPath(root));
        //            if (!folderpath)
        //            {
        //                Directory.CreateDirectory(HttpContext.Server.MapPath(root));
        //            }
        //            //----
        //            List<int> NumSolucion_img = new List<int>();
        //            //----
        //            var lst_imgName = Request.Files.AllKeys;
        //            //----
        //            for (int j = 0; j < lst_imgName.Count(); j++)
        //            {
        //                int tmp = Int32.Parse(lst_imgName[j].Replace("photo_", ""));
        //                //tmp = tmp - 1;
        //                NumSolucion_img.Add(tmp);
        //            }
        //            var k_real = 0;
        //            //FILES
        //            for (int j = 0; j < NumSolucion_img.Count(); j++)
        //            {
        //                int position = NumSolucion_img[j];
        //                //for (int k = 0; k < Lst_IdSolution_Saved.Count(); k++)
        //                //{
        //                //if (k == position)
        //                //{
        //                //k_real = Lst_IdSolution_Saved[k];
        //                //
        //                var files = Request.Files[j];
        //                var fileName = Path.GetFileName(files.FileName);
        //                //
        //                FilesTickets file = new FilesTickets();
        //                //int tmp = Lst_IdSolution_Saved[k];
        //                using (ClaroContext conn = new ClaroContext())
        //                {
        //                    file.IdCrm = IdCRM;
        //                    file.IdBandeja = IdBandeja;
        //                    file.NameFile = fileName;
        //                    file.Ruta = @"\10.200.154.36\uFiles\Edenred\AsistenteExpress\" + fileName;
        //                    file.FechaCarga = Hoy;
        //                    //file.estatus = true;
        //                    file.typeFile = Request.Files[j].ContentType;
        //                    file.IdHis = IdHis;
        //                    file.IdHisBandeja = historico.Id;
        //                    conn.FilesTickets.Add(file);
        //                    var validaSave = conn.SaveChanges();
        //                    if (validaSave > 0)
        //                    {
        //                        //GUARDA EL ARCHIVO DE FORMA LOCAL
        //                        var path = Path.Combine(HttpContext.Server.MapPath(root), fileName);
        //                        files.SaveAs(path);
        //                        //GUARDA EN LA RUTA QUE SERA COMPARTIDA PARA AMBAS DIRECCIONES 35 Y 36
        //                        var fname = @"\\\\10.200.154.36\\uFiles\\Edenred\\AsistenteExpress\\" + fileName;
        //                        System.IO.File.Copy(path, fname, true);
        //                        System.IO.File.Delete(path);
        //                        data.Msj = "OK";
        //                        return Json(data, JsonRequestBehavior.AllowGet);

        //                    }

        //                }
        //                //}
        //                //}
        //            }
        //            data.Msj = "OK";
        //            return Json(data, JsonRequestBehavior.AllowGet);
        //            //
        //        }
        //        else
        //        {
        //            //return Json(new { success = false, message = "¡Favor de seleccionar un archivo!" }, JsonRequestBehavior.AllowGet);
        //            data.Msj = "OK";
        //            return Json(data, JsonRequestBehavior.AllowGet);
        //        }
        //        //

        //    }
        //    catch (Exception ex)
        //    {
        //        data.Msj = "ERROR";
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }

        //}
        #endregion

        public ActionResult CreateNotificacion() 
        {
            return View();
        }
    }
}
