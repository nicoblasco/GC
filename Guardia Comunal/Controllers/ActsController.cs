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
using Guardia_Comunal.Tags;

namespace Guardia_Comunal.Controllers
{
    [AutenticadoAttribute]
    public class ActsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Acts
        public ActionResult Index()
        {
            return View(db.Acts.ToList());
        }

        // GET: Acts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act act = db.Acts.Find(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        // GET: Acts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TipoDeActa,NroActa,FechaInfraccion,Tanda,Calle,Altura,EntreCalle,Barrio,FechaEnvioAlJuzgado,ActaAdjunta,FechaCarga,Color,NroMotor,NroChasis,EstadoVehiculo,FechaEstado,TipoAgente,VehiculoRetenido,LicenciaRetenida,TicketAlcoholemia,ResultadoAlcoholemia,TicketAlcoholemiaAdjunto,Informe,InformeAdjunto,Detalle,Enable")] Act act)
        {
            if (ModelState.IsValid)
            {
                db.Acts.Add(act);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(act);
        }

        // GET: Acts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act act = db.Acts.Find(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        // POST: Acts/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TipoDeActa,NroActa,FechaInfraccion,Tanda,Calle,Altura,EntreCalle,Barrio,FechaEnvioAlJuzgado,ActaAdjunta,FechaCarga,Color,NroMotor,NroChasis,EstadoVehiculo,FechaEstado,TipoAgente,VehiculoRetenido,LicenciaRetenida,TicketAlcoholemia,ResultadoAlcoholemia,TicketAlcoholemiaAdjunto,Informe,InformeAdjunto,Detalle,Enable")] Act act)
        {
            if (ModelState.IsValid)
            {
                db.Entry(act).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(act);
        }

        // GET: Acts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act act = db.Acts.Find(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        // POST: Acts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Act act = db.Acts.Find(id);
            db.Acts.Remove(act);
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
