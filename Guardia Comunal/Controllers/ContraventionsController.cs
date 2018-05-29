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
    public class ContraventionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contraventions
        public ActionResult Index()
        {
            return View(db.Contraventions.ToList());
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
