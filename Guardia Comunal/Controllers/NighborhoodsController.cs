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
    public class NighborhoodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Nighborhoods
        public ActionResult Index()
        {
            return View(db.Nighborhoods.ToList());
        }

        // GET: Nighborhoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nighborhood nighborhood = db.Nighborhoods.Find(id);
            if (nighborhood == null)
            {
                return HttpNotFound();
            }
            return View(nighborhood);
        }

        // GET: Nighborhoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nighborhoods/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Direccion,Altura,Partida,Barrio,Nombre,Zona,NombreCorto,Codigo,Habitantes2010,ProyecionHabitantes2016,ProyecionHabitantes2020,Detalle,Latitud,Longitud,Perimetro,Area")] Nighborhood nighborhood)
        {
            if (ModelState.IsValid)
            {
                db.Nighborhoods.Add(nighborhood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nighborhood);
        }

        // GET: Nighborhoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nighborhood nighborhood = db.Nighborhoods.Find(id);
            if (nighborhood == null)
            {
                return HttpNotFound();
            }
            return View(nighborhood);
        }

        // POST: Nighborhoods/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Direccion,Altura,Partida,Barrio,Nombre,Zona,NombreCorto,Codigo,Habitantes2010,ProyecionHabitantes2016,ProyecionHabitantes2020,Detalle,Latitud,Longitud,Perimetro,Area")] Nighborhood nighborhood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nighborhood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nighborhood);
        }

        // GET: Nighborhoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nighborhood nighborhood = db.Nighborhoods.Find(id);
            if (nighborhood == null)
            {
                return HttpNotFound();
            }
            return View(nighborhood);
        }

        // POST: Nighborhoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nighborhood nighborhood = db.Nighborhoods.Find(id);
            db.Nighborhoods.Remove(nighborhood);
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
