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

namespace Guardia_Comunal.Controllers
{
    public class LiberationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Liberations
        public ActionResult Index()
        {
            return View(db.Liberations.ToList());
        }

        // GET: Liberations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liberation liberation = db.Liberations.Find(id);
            if (liberation == null)
            {
                return HttpNotFound();
            }
            return View(liberation);
        }

        // GET: Liberations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Liberations/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NroLiberacion,FechaDeLiberacion,NroJuzgado,FechaCarga,Convenio,Cuotas,Acarreo,NroRecibo,Importe,MontoEnCuotas,FechaEmisionRecibo,Enable")] Liberation liberation)
        {
            if (ModelState.IsValid)
            {
                db.Liberations.Add(liberation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(liberation);
        }

        // GET: Liberations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liberation liberation = db.Liberations.Find(id);
            if (liberation == null)
            {
                return HttpNotFound();
            }
            return View(liberation);
        }

        // POST: Liberations/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NroLiberacion,FechaDeLiberacion,NroJuzgado,FechaCarga,Convenio,Cuotas,Acarreo,NroRecibo,Importe,MontoEnCuotas,FechaEmisionRecibo,Enable")] Liberation liberation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liberation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(liberation);
        }

        // GET: Liberations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liberation liberation = db.Liberations.Find(id);
            if (liberation == null)
            {
                return HttpNotFound();
            }
            return View(liberation);
        }

        // POST: Liberations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Liberation liberation = db.Liberations.Find(id);
            db.Liberations.Remove(liberation);
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
