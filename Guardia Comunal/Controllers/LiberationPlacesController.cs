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
    public class LiberationPlacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LiberationPlaces
        public ActionResult Index()
        {
            return View(db.LiberationPlaces.ToList());
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
