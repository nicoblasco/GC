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
using Guardia_Comunal.ViewModel;

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

        [HttpPost]
        public JsonResult GetDatosDelInfractor(string dni)
        {
            Act act = new Act();
            InfractorViewModel infractor = new InfractorViewModel();
            try
            {
                act = db.Acts.Where(x => x.DNI == dni).FirstOrDefault();

                if (act != null)
                {
                    infractor.Apellido = act.Apellido;
                    infractor.Nombre = act.Nombre;
                    infractor.NroLicencia = act.NroLicencia;
                }

                return Json(infractor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw;
            }
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
                             where t.CodCalle > 0
                             group t by new { t.Id, t.Nombre }
                             into grp
                             select new
                             {
                                 data = grp.Key.Id,
                                 value = grp.Key.Nombre
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



        [HttpPost]
        public JsonResult GetMarcas(int id)
        {
            List<VehicleBrand> list = new List<VehicleBrand>();
            try
            {
                //Filtro los habilitados
                list = db.VehicleBrands.ToList().Where(x => x.Enable == true && (x.VehicleTypeId == id)).ToList();
                var json = JsonConvert.SerializeObject(list);

                return Json(list, JsonRequestBehavior.AllowGet);
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

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;

            }
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
        public ActionResult Create(int? id)
        {
            Liberation liberation = new Liberation();
            liberation.Acta = new Act();
            liberation.Acta = db.Acts.Find(id);


            liberation.Person = db.People.Where(x => x.DNI == liberation.Acta.DNI).FirstOrDefault();

            if (liberation.Person == null)
            {
                liberation.Person = new Person();
                liberation.Person.DNI = liberation.Acta.DNI;
                liberation.Person.Apellido = liberation.Acta.Apellido;
                liberation.Person.Nombre = liberation.Acta.Nombre;
            }

            ViewBag.listaLiberationPlace = new List<LiberationPlace>(db.LiberationPlaces.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaTipos = new List<VehicleType>(db.VehicleTypes.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaMarcas = new List<VehicleBrand>(db.VehicleBrands.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaModelos = new List<VehicleModel>(db.VehicleModels.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaDominios = new List<Domain>(db.Domains.ToList());
            ViewBag.listaCalles = GetCalles();
            ViewBag.listaBarrios = GetBarrios();

            return View(liberation);
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
                liberation.Enable = true;

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
