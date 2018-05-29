﻿using System;
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
    public class VehicleBrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VehicleBrands
        public ActionResult Index()
        {
            return View(db.VehicleBrands.ToList());
        }

        // GET: VehicleBrands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
            if (vehicleBrand == null)
            {
                return HttpNotFound();
            }
            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleBrands/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,FechaAlta,Enable")] VehicleBrand vehicleBrand)
        {
            if (ModelState.IsValid)
            {
                db.VehicleBrands.Add(vehicleBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
            if (vehicleBrand == null)
            {
                return HttpNotFound();
            }
            return View(vehicleBrand);
        }

        // POST: VehicleBrands/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,FechaAlta,Enable")] VehicleBrand vehicleBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
            if (vehicleBrand == null)
            {
                return HttpNotFound();
            }
            return View(vehicleBrand);
        }

        // POST: VehicleBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleBrand vehicleBrand = db.VehicleBrands.Find(id);
            db.VehicleBrands.Remove(vehicleBrand);
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
