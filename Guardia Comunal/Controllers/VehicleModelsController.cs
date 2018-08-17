using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuardiaComunal.Models;
using Guardia_Comunal.Models;
using Newtonsoft.Json;
using Guardia_Comunal.Helpers;

namespace Guardia_Comunal.Controllers
{
    public class VehicleModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string ModuleDescription = "Vehiculos";
        public string WindowDescription = "Modelos";

        // GET: VehicleModels
        public ActionResult Index()
        {
            List<VehicleModel> list = db.VehicleModels.ToList();
            List<VehicleType> lTipos = new List<VehicleType>();
            List<VehicleBrand> lMarcas = new List<VehicleBrand>();
            lTipos = db.VehicleTypes.ToList().Where(x => x.Enable == true).ToList();
            lMarcas = db.VehicleBrands.ToList().Where(x => x.Enable == true).ToList();
            ViewBag.listaTipos = lTipos;
            ViewBag.listaMarcas = lMarcas;

            return View(list);
        }


        [HttpPost]
        public JsonResult GetMarcas(int id)
        {
            List<VehicleBrand> list = new List<VehicleBrand>();
            try
            {
                //Filtro los habilitados
                list = db.VehicleBrands.ToList().Where(x => x.Enable == true && (x.VehicleTypeId==id )).ToList();
                var json = JsonConvert.SerializeObject(list);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;

            }
        }

        [HttpPost]
        public JsonResult GetModelos()
        {
            List<VehicleModel> list = new List<VehicleModel>();
            try
            {
                //Filtro los habilitados
                list = db.VehicleModels.ToList().Where(x => x.Enable == true).ToList();
                var json = JsonConvert.SerializeObject(list);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;

            }
        }


        [HttpPost]
        public JsonResult GetModelo(int id)
        {
            VehicleModel modelo = new VehicleModel();
            try
            {
                modelo = db.VehicleModels.Find(id);
                var json = JsonConvert.SerializeObject(modelo);

                return Json(modelo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public JsonResult GetDuplicates(int id, int tipo, int marca, string modelo)
        {

            try
            {
                var result = from c in db.VehicleModels
                             where (c.Id != id)
                             && (c.VehicleBrand.VehicleTypeId == tipo)
                             && (c.VehicleBrandId == marca)
                             && (c.Enable == true)
                             && (c.Descripcion.ToUpper() == modelo.ToUpper())
                             select c;

                var responseObject = new
                {
                    responseCode = result.Count()
                };

                return Json(responseObject, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult CreateModelo(VehicleModel modelo)
        {
            if (modelo == null)
            {
                return Json(new { responseCode = "-10" });
            }

            modelo.Enable = true;
            modelo.FechaAlta = DateTime.Now;
            db.VehicleModels.Add(modelo);

            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Alta", modelo.Id.ToString(), "VehicleModel", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }


        public JsonResult EditModelo(VehicleModel modelo)
        {
            if (modelo == null)
            {
                return Json(new { responseCode = "-10" });
            }

            db.VehicleModels.Attach(modelo);
            db.Entry(modelo).Property(x => x.Descripcion).IsModified = true;
            db.Entry(modelo).Property(x => x.VehicleBrandId).IsModified = true;
            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Modificacion", modelo.Id.ToString(), "VehicleModel", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }


        public JsonResult DeleteModelo(int id)
        {
            if (id == 0)
            {
                return Json(new { responseCode = "-10" });
            }

            VehicleModel modelo = db.VehicleModels.Find(id);
            db.VehicleModels.Remove(modelo);
            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Baja", modelo.Id.ToString(), "VehicleModel", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);


        }


        // GET: VehicleModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleModels/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,FechaAlta,Enable")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                db.VehicleModels.Add(vehicleModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModels/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,FechaAlta,Enable")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            db.VehicleModels.Remove(vehicleModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
