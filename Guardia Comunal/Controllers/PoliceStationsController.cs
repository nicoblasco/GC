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
    public class PoliceStationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PoliceStations
        public ActionResult Index()
        {
            return View(db.PoliceStations.ToList());
        }

        // GET: PoliceStations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceStation policeStation = db.PoliceStations.Find(id);
            if (policeStation == null)
            {
                return HttpNotFound();
            }
            return View(policeStation);
        }

        // GET: PoliceStations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoliceStations/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaAlta,Direccion,Altura,Partida,Barrio,Sede,Detalle,Latitud,Longitud,Enable")] PoliceStation policeStation)
        {
            if (ModelState.IsValid)
            {
                db.PoliceStations.Add(policeStation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(policeStation);
        }

        // GET: PoliceStations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceStation policeStation = db.PoliceStations.Find(id);
            if (policeStation == null)
            {
                return HttpNotFound();
            }
            return View(policeStation);
        }

        // POST: PoliceStations/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaAlta,Direccion,Altura,Partida,Barrio,Sede,Detalle,Latitud,Longitud,Enable")] PoliceStation policeStation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(policeStation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(policeStation);
        }

        // GET: PoliceStations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceStation policeStation = db.PoliceStations.Find(id);
            if (policeStation == null)
            {
                return HttpNotFound();
            }
            return View(policeStation);
        }

        // POST: PoliceStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PoliceStation policeStation = db.PoliceStations.Find(id);
            db.PoliceStations.Remove(policeStation);
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
