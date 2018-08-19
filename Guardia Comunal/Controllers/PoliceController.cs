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
    public class PoliceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string ModuleDescription = "AMB Maestros";
        public string WindowDescription = "Policias";

        // GET: Police
        public ActionResult Index()
        {
            ViewBag.AltaModificacion = PermissionViewModel.TienePermisoAlta(WindowHelper.GetWindowId(ModuleDescription, WindowDescription));
            ViewBag.Baja = PermissionViewModel.TienePermisoBaja(WindowHelper.GetWindowId(ModuleDescription, WindowDescription));
            List<Police> list = db.Police.ToList();
            List<PoliceStation> lComisarias = new List<PoliceStation>();
            lComisarias = db.PoliceStations.ToList().Where(x => x.Enable == true).ToList();
            ViewBag.listaComisarias = lComisarias;

            return View(list);
        }

        [HttpPost]
        public JsonResult GetPolicias()
        {
            List<Police> list = new List<Police>();
            try
            {
                list = db.Police.ToList().Where(x => x.Enable == true).ToList();
                var json = JsonConvert.SerializeObject(list);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult GetPolicia(int id)
        {
            Police police = new Police();
            try
            {
                police = db.Police.Find(id);
                var json = JsonConvert.SerializeObject(police);

                return Json(police, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult EditPolicia(Police police)
        {
            if (police == null)
            {
                return Json(new { responseCode = "-10" });
            }


            //Solo actualizo el campo descripcion
            db.Police.Attach(police);
            db.Entry(police).Property(x => x.PoliceStationId).IsModified = true;
            db.Entry(police).Property(x => x.Nombre).IsModified = true;
            db.Entry(police).Property(x => x.Apellido).IsModified = true;
            db.Entry(police).Property(x => x.DNI).IsModified = true;

            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Modificacion", police.Id.ToString(), "Police", ModuleDescription, WindowDescription);

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
                var result = from c in db.Police
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


        public JsonResult CreatePolicia(Police police)
        {
            if (police == null)
            {
                return Json(new { responseCode = "-10" });
            }

            police.Enable = true;
            police.FechaAlta = DateTime.Now;

            db.Police.Add(police);
            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Alta", police.Id.ToString(), "Police", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);
        }


        public JsonResult DeletePolicia(int id)
        {
            if (id == 0)
            {
                return Json(new { responseCode = "-10" });
            }

            Police police = db.Police.Find(id);
            db.Police.Remove(police);
            db.SaveChanges();

            //Audito
            AuditHelper.Auditar("Baja", police.Id.ToString(), "Police", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);


        }

        // GET: Police/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Police police = db.Police.Find(id);
            if (police == null)
            {
                return HttpNotFound();
            }
            return View(police);
        }

        // GET: Police/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Police/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DNI,Nombre,Apellido,FechaAlta,Enable")] Police police)
        {
            if (ModelState.IsValid)
            {
                db.Police.Add(police);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(police);
        }

        // GET: Police/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Police police = db.Police.Find(id);
            if (police == null)
            {
                return HttpNotFound();
            }
            return View(police);
        }

        // POST: Police/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DNI,Nombre,Apellido,FechaAlta,Enable")] Police police)
        {
            if (ModelState.IsValid)
            {
                db.Entry(police).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(police);
        }

        // GET: Police/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Police police = db.Police.Find(id);
            if (police == null)
            {
                return HttpNotFound();
            }
            return View(police);
        }

        // POST: Police/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Police police = db.Police.Find(id);
            db.Police.Remove(police);
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
