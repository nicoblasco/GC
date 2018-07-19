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
            //List<Street> lCalles = new List<Street>();
           // List<Nighborhood> lBarrios = new List<Nighborhood>();
          //  List<VehicleType> lTipos = new List<VehicleType>();
           // List<VehicleBrand> lMarcas = new List<VehicleBrand>();
            //List<VehicleModel> lModelos = new List<VehicleModel>();
            //List<Domain> lDominios = new List<Domain>();
            List<Police> lPolicias = new List<Police>();
            List<Inspector> lIspectores = new List<Inspector>();
            List<Contravention> lContravenciones = new List<Contravention>();
            List<Observation> lObservaciones = new List<Observation>();

            //lCalles = db.Streets.ToList();
          //  lBarrios = db.Nighborhoods.ToList();
            //lTipos = db.VehicleTypes.ToList();
            //lMarcas = db.VehicleBrands.ToList();
            //lModelos = db.VehicleModels.ToList();
            //lDominios = db.Domains.ToList();
            lPolicias = db.Police.ToList();
            lIspectores = db.Inspectors.ToList();
            lContravenciones = db.Contraventions.ToList();
            lObservaciones = db.Observations.ToList();

            ViewBag.listaCalles = GetCalles();
            ViewBag.listaBarrios = GetBarrios();
            ViewBag.listaTipos = GetTipoVehiculos();
            //ViewBag.listaMarcas = lMarcas;
            //ViewBag.listaModelos = lModelos;
            ViewBag.listaDominios = GetTipoDominios() ;
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


        [HttpGet]
        public JsonResult GetCalles()
        {
            List<Street> list = new List<Street>();
            try
            {
                //Filtro los habilitados
               // list = db.Streets.ToList();
                var query = (from t in db.Streets
                             where t.CodCalle>0
                             group t by new { t.CodCalle, t.Nombre }
                             into grp
                             select new
                             {
                                 data=grp.Key.CodCalle,
                                 value=grp.Key.Nombre
                             }).ToList();


                var json = JsonConvert.SerializeObject(query);
                //var json = JsonConvert.SerializeObject(list.GroupBy(x => new { x.CodCalle, x.Nombre }).Select(item =>
                //  new { data = item.CodCalle.ToString(), value = item.Nombre }));


                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;

            }
        }

        [HttpGet]
        public JsonResult GetTipoVehiculos()
        {
            List<VehicleType> list = new List<VehicleType>();
            try
            {
                //Filtro los habilitados
                list = db.VehicleTypes.ToList();
                var json = JsonConvert.SerializeObject(list.Select(item =>
                                  new { id = item.Id, text = item.Descripcion }));

                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;

            }
        }

        [HttpGet]
        public JsonResult GetTipoDominios()
        {
            List<Domain> list = new List<Domain>();
            try
            {
                //Filtro los habilitados
                list = db.Domains.ToList();
                var json = JsonConvert.SerializeObject(list.Select(item =>
                                  new { id = item.Id, text = item.Descripcion }));

                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;

            }
        }


        [HttpPost]
        public JsonResult GetMarcas(int id)
        {
            List<VehicleBrand> list = new List<VehicleBrand>();
            try
            {
                //Filtro los habilitados
                //list = db.VehicleBrands.ToList().Where(x => x.Enable == true && (x.VehicleTypeId == id)).ToList();
                //var json = JsonConvert.SerializeObject(list);

                var query = (from t in db.VehicleBrands
                             where t.Enable == true
                             where t.VehicleTypeId == id
                             group t by new { t.Id, t.Descripcion }
                             into grp
                             select new
                             {
                                 id = grp.Key.Id,
                                 text = grp.Key.Descripcion
                             }).ToList();


                var json = JsonConvert.SerializeObject(query);

                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;

            }
        }

        [HttpPost]
        public JsonResult GetModelos(int id)
        {
            List<VehicleModel> list = new List<VehicleModel>();
            try
            {
                //Filtro los habilitados
                list = db.VehicleModels.ToList().Where(x => x.Enable == true && (x.VehicleBrandId == id)).ToList();
                var json = JsonConvert.SerializeObject(list);

                return Json(list, JsonRequestBehavior.AllowGet);
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
