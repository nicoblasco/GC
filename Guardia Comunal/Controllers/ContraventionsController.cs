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
    public class ContraventionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string ModuleDescription = "AMB Maestros";
        public string WindowDescription = "Contravenciones";

        // GET: Contraventions
        public ActionResult Index()
        {
            return View(db.Contraventions.ToList());
        }



        [HttpPost]
        public JsonResult GetContravenciones()
        {
            //List<Contravention> list = new List<Contravention>();
            try
            {
                //list = db.Contraventions.ToList().Where(x => x.Enable == true).ToList();
                //var json = JsonConvert.SerializeObject(list);
                var list = db.Contraventions.Where(c => c.Enable==true).Select(c => new { c.Id, c.NroArticulo, c.Descripcion });

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult GetContravencion(int id)
        {
            Contravention contravention = new Contravention();
            try
            {
                contravention = db.Contraventions.Find(id);
                var json = JsonConvert.SerializeObject(contravention);

                return Json(contravention, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult EditContravencion(Contravention contravention)
        {
            if (contravention == null)
            {
                return Json(new { responseCode = "-10" });
            }

            //db.Entry(tipo).State = EntityState.Modified;

            //Solo actualizo el campo descripcion
            db.Contraventions.Attach(contravention);
            db.Entry(contravention).Property(x => x.NroArticulo).IsModified = true;
            db.Entry(contravention).Property(x => x.Descripcion).IsModified = true;

            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Modificacion", contravention.Id.ToString(), "Contravention", ModuleDescription, WindowDescription);


            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }

        [HttpGet]
        public JsonResult GetDuplicates(int id, string nroarticulo)
        {

            try
            {
                var result = from c in db.Contraventions
                             where c.Id != id
                             && c.NroArticulo.ToUpper() == nroarticulo.ToUpper()
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


        public JsonResult CreateContravencion(Contravention contravention)
        {
            if (contravention == null)
            {
                return Json(new { responseCode = "-10" });
            }

            contravention.Enable = true;
            contravention.FechaAlta = DateTime.Now;

            db.Contraventions.Add(contravention);

            db.SaveChanges();


            //Audito
            AuditHelper.Auditar("Alta", contravention.Id.ToString(), "Contravention", ModuleDescription, WindowDescription);


            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }


        public JsonResult DeleteContravencion(int id)
        {
            if (id == 0)
            {
                return Json(new { responseCode = "-10" });
            }

            Contravention contravention = db.Contraventions.Find(id);
            db.Contraventions.Remove(contravention);
            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Baja", id.ToString(), "Contravention", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);


        }

        // GET: Contraventions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contravention contravention = db.Contraventions.Find(id);
            if (contravention == null)
            {
                return HttpNotFound();
            }
            return View(contravention);
        }

        // GET: Contraventions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contraventions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NroArticulo,Descripcion,FechaAlta,Enable")] Contravention contravention)
        {
            if (ModelState.IsValid)
            {
                db.Contraventions.Add(contravention);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contravention);
        }

        // GET: Contraventions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contravention contravention = db.Contraventions.Find(id);
            if (contravention == null)
            {
                return HttpNotFound();
            }
            return View(contravention);
        }

        // POST: Contraventions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NroArticulo,Descripcion,FechaAlta,Enable")] Contravention contravention)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contravention).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contravention);
        }

        // GET: Contraventions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contravention contravention = db.Contraventions.Find(id);
            if (contravention == null)
            {
                return HttpNotFound();
            }
            return View(contravention);
        }

        // POST: Contraventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contravention contravention = db.Contraventions.Find(id);
            db.Contraventions.Remove(contravention);
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
