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
using Guardia_Comunal.Helpers;
using Guardia_Comunal.Tags;

namespace Guardia_Comunal.Controllers
{
    [AutenticadoAttribute]
    public class LiberationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string ModuleDescription = "Actas";
        public string WindowDescription = "Liberacion";

        // GET: Liberations
        public ActionResult Index()
        {
            return View(db.Liberations.Where(x=>x.Enable==true).ToList());
        }

        [HttpPost]
        public JsonResult GetDatosDelInfractor(string dni)
        {
            //Act act = new Act();
            //InfractorViewModel infractor = new InfractorViewModel();
            //try
            //{
            //    act = db.Acts.Where(x => x.DNI == dni).FirstOrDefault();

            //    if (act != null)
            //    {
            //        infractor.Apellido = act.Apellido;
            //        infractor.Nombre = act.Nombre;
            //        infractor.NroLicencia = act.NroLicencia;
            //    }

            //    return Json(infractor, JsonRequestBehavior.AllowGet);

            Person person = new Person();
            try
            {
                person = db.People.Where(x => x.DNI == dni).FirstOrDefault();


                return Json(person, JsonRequestBehavior.AllowGet);
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
                list = db.Streets.ToList();
                var json = JsonConvert.SerializeObject(list.Select(item =>
                                  new { data = item.Id.ToString() , value = item.Descripcion }));

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
                ViewBag.PersonId = null;
            }
            else
            {
                ViewBag.PersonId = liberation.Person.Id;
            }

            ViewBag.listaLiberationPlace = new List<LiberationPlace>(db.LiberationPlaces.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaTipos = new List<VehicleType>(db.VehicleTypes.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaMarcas = new List<VehicleBrand>(db.VehicleBrands.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaModelos = new List<VehicleModel>(db.VehicleModels.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaDominios = new List<Domain>(db.Domains.ToList());
            ViewBag.ActaId = id;
            ViewBag.listaCalles = GetCalles();
            ViewBag.listaBarrios = GetBarrios();

            return View(liberation);
        }

        // POST: Liberations/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActaId,LiberationPlaceId,NroLiberacion,FechaDeLiberacion,NroJuzgado,FechaCarga,Convenio,Cuotas,Acarreo,NroRecibo,Importe,MontoEnCuotas,FechaEmisionRecibo,Enable,UsuarioId,Person,PersonId")] Liberation liberation)
        {
            ModelState.Remove("PersonId");

            if (ModelState.IsValid)
            {
                liberation.Enable = true;

                if (liberation.PersonId != 0)
                {
                    if (liberation.Person != null)
                    {
                        liberation.Person.Id = liberation.PersonId;
                        db.Entry(liberation.Person).State = EntityState.Modified;
                    }
                }

                Act act = db.Acts.Find(liberation.ActaId);

                liberation.Person.Nombre= liberation.Person.Nombre?.ToUpper();
                liberation.Person.Altura = liberation.Person.Altura?.ToUpper();
                liberation.Person.Apellido = liberation.Person.Apellido?.ToUpper();
                liberation.Person.EntreCalle = liberation.Person.EntreCalle?.ToUpper();
                liberation.Person.Partido = liberation.Person.Partido?.ToUpper();
                liberation.Person.Barrio = liberation.Person.Barrio?.ToUpper();
                liberation.Person.Calle = liberation.Person.Calle?.ToUpper();


                act.FechaEstado = DateTime.Now;
                act.EstadoVehiculo = "Liberado";
                db.Liberations.Add(liberation);
                db.Entry(act).State = EntityState.Modified;
                db.SaveChanges();

                TempData["message"] = "Los cambios se han grabado correctamente.";
                //Audito
                AuditHelper.Auditar("Alta", liberation.Id.ToString(), "Liberations", ModuleDescription, WindowDescription);

                return RedirectToAction("Index", "Acts");
            }

            return RedirectToAction("Index", "Acts");
        }

        // GET: Liberations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["message"] = "Lo sentimos! No se encontro la liberación";
                return RedirectToAction("Index", "Acts");
            }

            Act act = db.Acts.Find(id);

            Liberation liberation = db.Liberations.Where(x => x.ActaId == act.Id).FirstOrDefault();
            if (liberation == null)
            {
                TempData["message"] = "Lo sentimos! No se encontro la liberación";
                return RedirectToAction("Index", "Acts");
            }


            ViewBag.listaLiberationPlace = new List<LiberationPlace>(db.LiberationPlaces.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaTipos = new List<VehicleType>(db.VehicleTypes.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaMarcas = new List<VehicleBrand>(db.VehicleBrands.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaModelos = new List<VehicleModel>(db.VehicleModels.ToList().Where(x => x.Enable == true).ToList());
            ViewBag.listaDominios = new List<Domain>(db.Domains.ToList());
            ViewBag.ActaId = liberation.ActaId;
            ViewBag.PersonId = liberation.PersonId;
            ViewBag.Id = liberation.Id;
            ViewBag.listaCalles = GetCalles();
            ViewBag.listaBarrios = GetBarrios();

            return View(liberation);
        }

        // POST: Liberations/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ActaId,LiberationPlaceId,NroLiberacion,FechaDeLiberacion,NroJuzgado,FechaCarga,Convenio,Cuotas,Acarreo,NroRecibo,Importe,MontoEnCuotas,FechaEmisionRecibo,Enable,UsuarioId,Person,PersonId")] Liberation liberation)
        {
            if (ModelState.IsValid)
            {

                liberation.Person.Nombre = liberation.Person.Nombre?.ToUpper();
                liberation.Person.Altura = liberation.Person.Altura?.ToUpper();
                liberation.Person.Apellido = liberation.Person.Apellido?.ToUpper();
                liberation.Person.EntreCalle = liberation.Person.EntreCalle?.ToUpper();
                liberation.Person.Partido = liberation.Person.Partido?.ToUpper();
                liberation.Person.Barrio = liberation.Person.Barrio?.ToUpper();
                liberation.Person.Calle = liberation.Person.Calle?.ToUpper();

                liberation.Person.Id = liberation.PersonId;
                liberation.Acta = db.Acts.Find(liberation.ActaId);
                db.Entry(liberation).State = EntityState.Modified;
                db.Entry(liberation.Acta).State = EntityState.Modified;
                db.Entry(liberation.Person).State = EntityState.Modified;               

                db.SaveChanges();
                TempData["message"] = "Los cambios se han grabado correctamente.";
                //Audito
                AuditHelper.Auditar("Modificacion", liberation.Id.ToString(), "Liberations", ModuleDescription, WindowDescription);

                return RedirectToAction("Index", "Acts");
            }
            return RedirectToAction("Index", "Acts");
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

            //Audito
            AuditHelper.Auditar("Baja", liberation.Id.ToString(), "Liberations", ModuleDescription, WindowDescription);

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
