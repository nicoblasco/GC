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
    public class LiberationPlacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string ModuleDescription = "AMB Maestros";
        public string WindowDescription = "Lugar de Liberacion";


        // GET: LiberationPlaces
        public ActionResult Index()
        {
            return View(db.LiberationPlaces.ToList());
        }

        [HttpPost]
        public JsonResult GetLiberationPlaces()
        {
            List<LiberationPlace> list = new List<LiberationPlace>();
            try
            {
                list = db.LiberationPlaces.ToList().Where(x => x.Enable == true).ToList();
                var json = JsonConvert.SerializeObject(list);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult GetLiberationPlace(int id)
        {
            LiberationPlace liberationPlace = new LiberationPlace();
            try
            {
                liberationPlace = db.LiberationPlaces.Find(id);
                var json = JsonConvert.SerializeObject(liberationPlace);

                return Json(liberationPlace, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult EditLiberationPlace(LiberationPlace liberationPlace )
        {
            if (liberationPlace == null)
            {
                return Json(new { responseCode = "-10" });
            }

            //db.Entry(tipo).State = EntityState.Modified;

            //Solo actualizo el campo descripcion
            db.LiberationPlaces.Attach(liberationPlace);
            db.Entry(liberationPlace).Property(x => x.Descripcion).IsModified = true;

            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Modificacion", liberationPlace.Id.ToString(), "LiberationPlace", ModuleDescription, WindowDescription);

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
                var result = from c in db.LiberationPlaces
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


        public JsonResult CreateLiberationPlace(LiberationPlace liberationPlace)
        {
            if (liberationPlace == null)
            {
                return Json(new { responseCode = "-10" });
            }

            liberationPlace.Enable = true;
            liberationPlace.FechaAlta = DateTime.Now;

            db.LiberationPlaces.Add(liberationPlace);
            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Alta", liberationPlace.Id.ToString(), "LiberationPlace", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }


        public JsonResult DeleteliberationPlace(int id)
        {
            if (id == 0)
            {
                return Json(new { responseCode = "-10" });
            }

            LiberationPlace liberationPlace= db.LiberationPlaces.Find(id);
            db.LiberationPlaces.Remove(liberationPlace);
            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Baja", liberationPlace.Id.ToString(), "LiberationPlace", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);


        }


        // GET: LiberationPlaces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiberationPlace liberationPlace = db.LiberationPlaces.Find(id);
            if (liberationPlace == null)
            {
                return HttpNotFound();
            }
            return View(liberationPlace);
        }

        // GET: LiberationPlaces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LiberationPlaces/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,FechaAlta,Enable")] LiberationPlace liberationPlace)
        {
            if (ModelState.IsValid)
            {
                db.LiberationPlaces.Add(liberationPlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(liberationPlace);
        }

        // GET: LiberationPlaces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiberationPlace liberationPlace = db.LiberationPlaces.Find(id);
            if (liberationPlace == null)
            {
                return HttpNotFound();
            }
            return View(liberationPlace);
        }

        // POST: LiberationPlaces/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,FechaAlta,Enable")] LiberationPlace liberationPlace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liberationPlace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(liberationPlace);
        }

        // GET: LiberationPlaces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiberationPlace liberationPlace = db.LiberationPlaces.Find(id);
            if (liberationPlace == null)
            {
                return HttpNotFound();
            }
            return View(liberationPlace);
        }

        // POST: LiberationPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LiberationPlace liberationPlace = db.LiberationPlaces.Find(id);
            db.LiberationPlaces.Remove(liberationPlace);
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
