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
    public class InspectorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string ModuleDescription = "AMB Maestros";
        public string WindowDescription = "Inspectores";

        // GET: Inspectors
        public ActionResult Index()
        {
            return View(db.Inspectors.ToList());
        }

        [HttpPost]
        public JsonResult GetInspectores()
        {
            List<Inspector> list = new List<Inspector>();
            try
            {
                list = db.Inspectors.ToList().Where(x => x.Enable == true).ToList();
                var json = JsonConvert.SerializeObject(list);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult GetInspector(int id)
        {
            Inspector inspector = new Inspector();
            try
            {
                inspector = db.Inspectors.Find(id);
                var json = JsonConvert.SerializeObject(inspector);

                return Json(inspector, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult EditInspector(Inspector inspector)
        {
            if (inspector == null)
            {
                return Json(new { responseCode = "-10" });
            }

            //db.Entry(tipo).State = EntityState.Modified;

            //Solo actualizo el campo descripcion
            db.Inspectors.Attach(inspector);
            db.Entry(inspector).Property(x => x.DNI).IsModified = true;
            db.Entry(inspector).Property(x => x.Apellido).IsModified = true;
            db.Entry(inspector).Property(x => x.Nombre).IsModified = true;

            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Modificacion", inspector.Id.ToString(), "Inspector", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }

        [HttpGet]
        public JsonResult GetDuplicates(int id, string dni)
        {

            try
            {
                var result = from c in db.Inspectors
                             where c.Id != id
                             && c.DNI.ToUpper() == dni.ToUpper()
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


        public JsonResult CreateInspector(Inspector inspector)
        {
            if (inspector == null)
            {
                return Json(new { responseCode = "-10" });
            }

            inspector.Enable = true;
            inspector.FechaAlta = DateTime.Now;

            db.Inspectors.Add(inspector);
            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Alta", inspector.Id.ToString(), "Inspector", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }


        public JsonResult DeleteInspector(int id)
        {
            if (id == 0)
            {
                return Json(new { responseCode = "-10" });
            }

            Inspector inspector = db.Inspectors.Find(id);
            db.Inspectors.Remove(inspector);
            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Baja", inspector.Id.ToString(), "Inspector", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);


        }


        // GET: Inspectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspector inspector = db.Inspectors.Find(id);
            if (inspector == null)
            {
                return HttpNotFound();
            }
            return View(inspector);
        }

        // GET: Inspectors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inspectors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DNI,Nombre,Apellido,FechaAlta,Enable")] Inspector inspector)
        {
            if (ModelState.IsValid)
            {
                db.Inspectors.Add(inspector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inspector);
        }

        // GET: Inspectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspector inspector = db.Inspectors.Find(id);
            if (inspector == null)
            {
                return HttpNotFound();
            }
            return View(inspector);
        }

        // POST: Inspectors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DNI,Nombre,Apellido,FechaAlta,Enable")] Inspector inspector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inspector);
        }

        // GET: Inspectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspector inspector = db.Inspectors.Find(id);
            if (inspector == null)
            {
                return HttpNotFound();
            }
            return View(inspector);
        }

        // POST: Inspectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inspector inspector = db.Inspectors.Find(id);
            db.Inspectors.Remove(inspector);
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
