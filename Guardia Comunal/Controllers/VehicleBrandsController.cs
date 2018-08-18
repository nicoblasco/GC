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
using Guardia_Comunal.Tags;

namespace Guardia_Comunal.Controllers
{
    [AutenticadoAttribute]
    public class VehicleBrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string ModuleDescription = "Vehiculos";
        public string WindowDescription = "Marcas";


        // GET: VehicleBrands
        public ActionResult Index()
        {
            List<VehicleBrand> list = db.VehicleBrands.ToList();
            List<VehicleType> lTipos = new List<VehicleType>();
            lTipos = db.VehicleTypes.ToList().Where(x => x.Enable == true).ToList();
            ViewBag.listaTipos = lTipos;

            return View(list);
        }


        [HttpPost]
        public JsonResult GetMarcas()
        {
            List<VehicleBrand> list = new List<VehicleBrand>();
            try
            {
                //Filtro los habilitados
                list = db.VehicleBrands.ToList().Where(x=> x.Enable==true).ToList();
                var json = JsonConvert.SerializeObject(list);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;

            }
        }


        [HttpPost]
        public JsonResult GetMarca(int id)
        {
            VehicleBrand marca = new VehicleBrand();
            try
            {
                marca = db.VehicleBrands.Find(id);
                var json = JsonConvert.SerializeObject(marca);

                return Json(marca, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public JsonResult GetDuplicates(int id, int tipo, string marca)
        {

            try
            {
                var result = from c in db.VehicleBrands
                             where (c.Id != id)
                             && (c.VehicleType.Id == tipo) 
                             && (c.Enable == true)
                             && (c.Descripcion.ToUpper() == marca.ToUpper())
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

        // GET: VehicleBrands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
            if (vehicleBrand == null)
            {
                return HttpNotFound();
            }
            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Create
        public ActionResult Create()
        {
            return View();
        }


        public JsonResult CreateMarca(VehicleBrand marca)
        {
            if (marca == null)
            {
                return Json(new { responseCode = "-10" });
            }

            marca.Enable = true;
            marca.FechaAlta = DateTime.Now;
            db.VehicleBrands.Add(marca);

            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Alta", marca.Id.ToString(), "VehicleBrand", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }


        public JsonResult EditMarca(VehicleBrand marca)
        {
            if (marca == null)
            {
                return Json(new { responseCode = "-10" });
            }

            db.VehicleBrands.Attach(marca);
            db.Entry(marca).Property(x => x.Descripcion).IsModified = true;
            db.Entry(marca).Property(x => x.VehicleTypeId).IsModified = true;
            db.SaveChanges();
            //Audito
            AuditHelper.Auditar("Modificacion", marca.Id.ToString(), "VehicleBrand", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }
        // POST: VehicleBrands/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,FechaAlta,Enable")] VehicleBrand vehicleBrand)
        {
            if (ModelState.IsValid)
            {
                db.VehicleBrands.Add(vehicleBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
            if (vehicleBrand == null)
            {
                return HttpNotFound();
            }
            return View(vehicleBrand);
        }

        // POST: VehicleBrands/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,FechaAlta,Enable")] VehicleBrand vehicleBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleBrand);
        }


        public JsonResult DeleteMarca(int id)
        {
            if (id == 0)
            {
                return Json(new { responseCode = "-10" });
            }

            VehicleBrand marca = db.VehicleBrands.Find(id);
            db.VehicleBrands.Remove(marca);
            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Baja", marca.Id.ToString(), "VehicleBrand", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);


        }
        // GET: VehicleBrands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
            if (vehicleBrand == null)
            {
                return HttpNotFound();
            }
            return View(vehicleBrand);
        }

        // POST: VehicleBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
            db.VehicleBrands.Remove(vehicleBrand);
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
