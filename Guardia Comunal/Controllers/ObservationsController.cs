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
using Guardia_Comunal.ViewModel;

namespace Guardia_Comunal.Controllers
{
    [AutenticadoAttribute]
    public class ObservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string ModuleDescription = "AMB Maestros";
        public string WindowDescription = "Observaciones";

        // GET: Observations
        public ActionResult Index()
        {
            ViewBag.AltaModificacion = PermissionViewModel.TienePermisoAlta(WindowHelper.GetWindowId(ModuleDescription, WindowDescription));
            ViewBag.Baja = PermissionViewModel.TienePermisoBaja(WindowHelper.GetWindowId(ModuleDescription, WindowDescription));
            return View(db.Observations.ToList());
        }


        [HttpPost]
        public JsonResult GetObservaciones()
        {
            //List<Observation> list = new List<Observation>();
            try
            {
                var list = db.Observations.Select(c => new { c.Id, c.Descripcion });
              //  list = db.Observations.ToList().Where(x => x.Enable == true).ToList();
                                

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult GetObservacion(int id)
        {
            Observation observation = new Observation();
            try
            {

                observation = db.Observations.Find(id);
                var json = JsonConvert.SerializeObject(observation);

                return Json(observation, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult EditObservacion(Observation observation)
        {
            if (observation == null)
            {
                return Json(new { responseCode = "-10" });
            }

            //db.Entry(tipo).State = EntityState.Modified;

            //Solo actualizo el campo descripcion
            db.Observations.Attach(observation);
            db.Entry(observation).Property(x => x.Descripcion).IsModified = true;

            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Modificacion", observation.Id.ToString(), "Observation", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }

        [HttpGet]
        public JsonResult GetDuplicates(int id, string descripcion)
        {

            try
            {
                var result = from c in db.Observations
                             where c.Id != id
                             && c.Descripcion.ToUpper() == descripcion.ToUpper()
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


        public JsonResult CreateObservacion(Observation observation)
        {
            if (observation == null)
            {
                return Json(new { responseCode = "-10" });
            }

            observation.Enable = true;
            observation.FechaAlta = DateTime.Now;

            db.Observations.Add(observation);
            db.SaveChanges();

            AuditHelper.Auditar("Alta", observation.Id.ToString(), "Observation", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }


        public JsonResult DeleteObservacion(int id)
        {
            if (id == 0)
            {
                return Json(new { responseCode = "-10" });
            }

            Observation observation = db.Observations.Find(id);
            db.Observations.Remove(observation);
            db.SaveChanges();
            AuditHelper.Auditar("Baja", observation.Id.ToString(), "Observation", ModuleDescription, WindowDescription);
            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);


        }

        // GET: Observations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Observation observation = db.Observations.Find(id);
            if (observation == null)
            {
                return HttpNotFound();
            }
            return View(observation);
        }

        // GET: Observations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Observations/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,FechaAlta,Enable")] Observation observation)
        {
            if (ModelState.IsValid)
            {
                db.Observations.Add(observation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(observation);
        }

        // GET: Observations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Observation observation = db.Observations.Find(id);
            if (observation == null)
            {
                return HttpNotFound();
            }
            return View(observation);
        }

        // POST: Observations/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,FechaAlta,Enable")] Observation observation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(observation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(observation);
        }

        // GET: Observations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Observation observation = db.Observations.Find(id);
            if (observation == null)
            {
                return HttpNotFound();
            }
            return View(observation);
        }

        // POST: Observations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Observation observation = db.Observations.Find(id);
            db.Observations.Remove(observation);
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
