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
using Newtonsoft.Json;

namespace Guardia_Comunal.Controllers
{
    [AutenticadoAttribute]
    public class ActsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Acts
        public ActionResult Index()
        {
            List<Act> list = db.Acts.ToList();
            List<Street> lCalles = new List<Street>();
           // List<Nighborhood> lBarrios = new List<Nighborhood>();
            List<VehicleType> lTipos = new List<VehicleType>();
            List<VehicleBrand> lMarcas = new List<VehicleBrand>();
            List<VehicleModel> lModelos = new List<VehicleModel>();
            List<Domain> lDominios = new List<Domain>();
            List<Police> lPolicias = new List<Police>();
            List<Inspector> lIspectores = new List<Inspector>();
            List<Contravention> lContravenciones = new List<Contravention>();
            List<Observation> lObservaciones = new List<Observation>();

            lCalles = db.Streets.ToList();
          //  lBarrios = db.Nighborhoods.ToList();
            lTipos = db.VehicleTypes.ToList();
            lMarcas = db.VehicleBrands.ToList();
            lModelos = db.VehicleModels.ToList();
            lDominios = db.Domains.ToList();
            lPolicias = db.Police.ToList();
            lIspectores = db.Inspectors.ToList();
            lContravenciones = db.Contraventions.ToList();
            lObservaciones = db.Observations.ToList();

            ViewBag.listaCalles = lCalles;
            ViewBag.listaBarrios = GetBarrios();
            ViewBag.listaTipos = lTipos;
            ViewBag.listaMarcas = lMarcas;
            ViewBag.listaModelos = lModelos;
            ViewBag.listaDominios = lDominios;
            ViewBag.listaPolicias = lPolicias;
            ViewBag.listaInspectores = lIspectores;
            ViewBag.listaObservaciones = lObservaciones;
            ViewBag.listaContravenciones = lContravenciones;


            return View(list);
        }

        public ActionResult Search()
        {
            return View(db.Acts.ToList());
        }

        [HttpGet]
        public JsonResult GetBarrios()
        {
            List<Nighborhood> list = new List<Nighborhood>();
            try
            {
                //Filtro los habilitados
                list = db.Nighborhoods.ToList();
                var json = JsonConvert.SerializeObject(list.Select(item =>
                                  new { data = item.Codigo.ToString(), value = item.Nombre }));

                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;

            }
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
