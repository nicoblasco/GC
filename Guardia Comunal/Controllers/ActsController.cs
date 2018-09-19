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
using System.IO;
using Guardia_Comunal.ViewModel;
using Guardia_Comunal.Helpers;

namespace Guardia_Comunal.Controllers
{
    [AutenticadoAttribute]
    public class ActsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string ModuleDescription = "Actas";
        public string WindowDescription = "Altas";

        // GET: Acts
        public ActionResult Index()
        {
          //   List<Act> list = db.Acts.Where(x=>x.Enable==true).ToList();
            ViewBag.AltaModificacion = PermissionViewModel.TienePermisoAlta(WindowHelper.GetWindowId("Actas", "Altas"));
            ViewBag.Baja = PermissionViewModel.TienePermisoBaja(WindowHelper.GetWindowId("Actas", "Baja"));
            ViewBag.Liberacion = PermissionViewModel.TienePermisoAlta(WindowHelper.GetWindowId("Actas", "Liberacion"));
            return View();
        }

        //public ActionResult Search(string NroActa)
        //{
        //    //  List<Act> list = db.Acts.Where(x => x.Enable == true && x.NroActa==NroActa).ToList();

        //    List<Act> list = db.Acts.Where(x => x.Enable == true && x.NroActa == NroActa).ToList();

        //    ViewBag.AltaModificacion = PermissionViewModel.TienePermisoAlta(WindowHelper.GetWindowId("Actas", "Altas"));
        //    ViewBag.Baja = PermissionViewModel.TienePermisoBaja(WindowHelper.GetWindowId("Actas", "Baja"));
        //    ViewBag.Liberacion = PermissionViewModel.TienePermisoAlta(WindowHelper.GetWindowId("Actas", "Liberacion"));
        //    ViewBag.NroActa = NroActa;
        //    return View(list);
        //}

        public ActionResult SearchNroActa(string NroActa)
        {
            List<ActIndexViewModel> acts = new List<ActIndexViewModel>();
            acts = ArmarConsulta(NroActa, null, null, null, null, null, null, null);
            ViewBag.AltaModificacion = PermissionViewModel.TienePermisoAlta(WindowHelper.GetWindowId("Actas", "Altas"));
            ViewBag.Baja = PermissionViewModel.TienePermisoBaja(WindowHelper.GetWindowId("Actas", "Baja"));
            ViewBag.Liberacion = PermissionViewModel.TienePermisoAlta(WindowHelper.GetWindowId("Actas", "Liberacion"));
            ViewBag.NroActa = NroActa;
            return View("Index",acts);
        }
        

        private List<ActIndexViewModel> ArmarConsulta(string NroActa, string NroLiberacion, string Estado, string DNI, string Apellido, string Dominio, string FechaDesde, string FechaHasta)
        {
            List<ActIndexViewModel> acts = new List<ActIndexViewModel>();
            DateTime? dtFechaDesde = null;
            DateTime? dtFechaHasta = null;

            if (!String.IsNullOrEmpty(FechaDesde))
                dtFechaDesde = Convert.ToDateTime(FechaDesde);

            if (!String.IsNullOrEmpty(FechaHasta))
                dtFechaHasta = Convert.ToDateTime(FechaHasta);


            try
            {

                List<Liberation> liberationsList = new List<Liberation>();



                var lista = db.Acts
                .Where(x => x.Enable == true)
                .Where(x => !string.IsNullOrEmpty(NroActa) ? (x.NroActa == NroActa && x.NroActa != null) : true)
                .Where(x => !string.IsNullOrEmpty(Estado) ? (x.EstadoVehiculo == Estado && x.EstadoVehiculo != null) : true)
                .Where(x => !string.IsNullOrEmpty(DNI) ? (x.DNI == DNI && x.DNI != null) : true)
                .Where(x => !string.IsNullOrEmpty(Apellido) ? (x.Apellido == Apellido && x.Apellido != null) : true)
                .Where(x => !string.IsNullOrEmpty(Dominio) ? (x.Dominio == Dominio && x.Dominio != null) : true)
                .Where(x => !string.IsNullOrEmpty(FechaDesde) ? (x.FechaInfraccion >= dtFechaDesde && x.FechaInfraccion != null) : true)
                .Where(x => !string.IsNullOrEmpty(FechaHasta) ? (x.FechaInfraccion <= dtFechaHasta && x.FechaInfraccion != null) : true)
                .Where(x => !string.IsNullOrEmpty(NroLiberacion) ? (db.Liberations.Where(y => y.ActaId == x.Id && y.NroLiberacion == NroLiberacion).Any()) : true)
                .Select(c => new { c.Id, c.FechaCarga, c.FechaInfraccion, c.NroActa, c.Dominio, c.EstadoVehiculo, c.DNI, c.Nombre, c.Apellido }).OrderByDescending(x => x.FechaInfraccion).Take(1000);
                ;



                // List<ActIndexViewModel> acts = new List<ActIndexViewModel>();
                var liberations = db.Liberations.Where(x => x.Enable == true).ToList();

                foreach (var item in lista)
                {
                    ActIndexViewModel act = new ActIndexViewModel();
                    Liberation liberation = new Liberation();

                    //Busco si tiene un liberacion
                    liberation = liberations.Where(x => x.ActaId == item.Id).FirstOrDefault();

                    act.Id = item.Id;
                    act.NroActa = item.NroActa;
                    if (liberation != null)
                        act.NroLiberacion = liberation.NroLiberacion;

                    act.Apellido = item.Apellido;
                    act.DNI = item.DNI;
                    act.Dominio = item.Dominio;
                    act.Estado = item.EstadoVehiculo;
                    act.FechaCarga = item.FechaCarga;
                    act.FechaInfraccion = item.FechaInfraccion;

                    acts.Add(act);

                }

            }
            catch (Exception e)
            {

                throw;
            }

            return acts;
        }

        public JsonResult Search(string NroActa, string NroLiberacion, string Estado, string DNI, string Apellido, string Dominio, string FechaDesde, string FechaHasta )
        {

            List<ActIndexViewModel> acts = new List<ActIndexViewModel>();
            acts = ArmarConsulta(NroActa, NroLiberacion, Estado, DNI, Apellido, Dominio, FechaDesde, FechaHasta);

            ViewBag.AltaModificacion = PermissionViewModel.TienePermisoAlta(WindowHelper.GetWindowId("Actas", "Altas"));
            ViewBag.Baja = PermissionViewModel.TienePermisoBaja(WindowHelper.GetWindowId("Actas", "Baja"));
            ViewBag.Liberacion = PermissionViewModel.TienePermisoAlta(WindowHelper.GetWindowId("Actas", "Liberacion"));
          //  ViewBag.NroActa = NroActa;

            return Json(acts, JsonRequestBehavior.AllowGet);

        }



        private List<ActIndexViewModel> GetActIndexFiltrado()
        {
            try
            {


                var lista = db.Acts.Where(x => x.Enable == true).Select(c => new { c.Id, c.FechaCarga, c.FechaInfraccion, c.NroActa, c.Dominio, c.EstadoVehiculo, c.DNI, c.Nombre, c.Apellido }).OrderByDescending(x => x.FechaInfraccion).OrderByDescending(x=>x.FechaCarga).Take(1000);
                ;


                List<ActIndexViewModel> acts = new List<ActIndexViewModel>();
                var liberations = db.Liberations.Where(x => x.Enable == true).ToList();

                foreach (var item in lista)
                {
                    ActIndexViewModel act = new ActIndexViewModel();
                    Liberation liberation = new Liberation();

                    //Busco si tiene un liberacion
                    liberation = liberations.Where(x => x.ActaId == item.Id).FirstOrDefault();

                    act.Id = item.Id;
                    act.NroActa = item.NroActa;
                    if (liberation != null)
                        act.NroLiberacion = liberation.NroLiberacion;

                    act.Apellido = item.Apellido;
                    act.DNI = item.DNI;
                    act.Dominio = item.Dominio;
                    act.Estado = item.EstadoVehiculo;
                    act.FechaCarga = item.FechaCarga;
                    act.FechaInfraccion = item.FechaInfraccion;

                    acts.Add(act);

                }

                return acts;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        [HttpPost]
        public JsonResult GetActs()
        {
            try
            {

                List<ActIndexViewModel> acts = new List<ActIndexViewModel>();
                acts = GetActIndexFiltrado();
                 return Json(acts, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw;
            }
        }


        [HttpGet]
        public JsonResult GetDuplicates(int id, string nroActa)
        {

            try
            {
                var result = from c in db.Acts
                             where c.Id != id
                             && c.NroActa.ToUpper() == nroActa.ToUpper()
                             && c.Enable==true
                             select c;

                var responseObject = new
                {
                    responseCode = result.Count()
                };

                return Json(responseObject, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult GetDatosDelInfractor(string dni)
        {
            Act act = new Act();
            InfractorViewModel infractor = new InfractorViewModel();
            try
            {
                act = db.Acts.Where(x => x.DNI == dni).FirstOrDefault();
                
                if (act!=null)
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
                list = db.Streets.ToList();
                var json = JsonConvert.SerializeObject(list.Select(item =>
                                  new { data = item.Id.ToString(), value = item.Descripcion }));

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
            if (!PermissionViewModel.TienePermisoAcesso(WindowHelper.GetWindowId("Actas", "Altas")))
            {
                return RedirectToAction("Index", "Acts");
            }   

            List<VehicleType> lTipos = new List<VehicleType>();
            List<VehicleBrand> lMarcas = new List<VehicleBrand>();
            List<VehicleModel> lModelos = new List<VehicleModel>();
            List<Domain> lDominios = new List<Domain>();
            List<Police> lPolicias = new List<Police>();
            List<Inspector> lIspectores = new List<Inspector>();
            List<Contravention> lContravenciones = new List<Contravention>();
            List<Observation> lObservaciones = new List<Observation>();

            //lCalles = db.Streets.ToList();
            //  lBarrios = db.Nighborhoods.ToList();
            lTipos = db.VehicleTypes.ToList().Where(x => x.Enable == true).ToList();
            lMarcas = db.VehicleBrands.ToList().Where(x => x.Enable == true).ToList();
            lModelos = db.VehicleModels.ToList().Where(x => x.Enable == true).ToList();
            lDominios = db.Domains.ToList();
            lPolicias = db.Police.ToList().Where(x => x.Enable == true).ToList();
            lIspectores = db.Inspectors.ToList().Where(x => x.Enable == true).ToList();
            lContravenciones = db.Contraventions.ToList().Where(x => x.Enable == true).ToList();
            lObservaciones = db.Observations.ToList().Where(x => x.Enable == true).ToList();

            ViewBag.listaCalles = GetCalles();
            ViewBag.listaBarrios = GetBarrios();
            ViewBag.listaTipos = lTipos;
            ViewBag.listaMarcas = lMarcas;
            ViewBag.listaModelos = lModelos;
            ViewBag.listaDominios = lDominios;
            ViewBag.listaPolicias = lPolicias;
            ViewBag.listaInspectores = lIspectores;
            ViewBag.listaObservaciones = lObservaciones;
            ViewBag.listaContravenciones = lContravenciones;
            return View();
        }

        // POST: Acts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TipoDeActa,NroActa,FechaInfraccion,Tanda,Calle,StreetId,Altura,EntreCalle,Barrio,NighborhoodId,FechaEnvioAlJuzgado,FechaCarga,UsuarioId,VehicleTypeId,VehicleBrandId,VehicleModelId,Color,NroMotor,NroChasis,EstadoVehiculo,FechaEstado,TipoAgente,InspectorId,PoliceId,VehiculoRetenido,LicenciaRetenida,TicketAlcoholemia,ResultadoAlcoholemia,TicketAlcoholemiaAdjunto,Informe,InformeAdjunto,Detalle,Enable,DNI,Nombre,Apellido,NroLicencia,DomainId,Dominio,SelectedContraventions,SelectedObservations")] Act act, HttpPostedFileBase fileUploadActa, HttpPostedFileBase fileUploadTicketAlcoholemiaAdjunto, HttpPostedFileBase fileInformeAdjunto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    act.Enable = true;
                    act.Calle = act.Calle?.ToUpper();
                    act.Barrio = act.Barrio?.ToUpper();
                    act.Altura = act.Altura?.ToUpper();
                    act.Color = act.Color?.ToUpper();
                    act.Dominio = act.Dominio?.ToUpper();
                    act.Nombre = act.Nombre?.ToUpper();
                    act.Apellido = act.Apellido?.ToUpper();
                    act.EntreCalle = act.EntreCalle?.ToUpper();
                    act.NroActa = act.NroActa?.ToUpper();
                    act.NroChasis = act.NroChasis?.ToUpper();
                    act.NroLicencia = act.NroLicencia?.ToUpper();
                    act.NroMotor = act.NroMotor?.ToUpper();
                    act.Contraventions = new List<Contravention>();
                    act.Observations = new List<Observation>();

                    if (act.SelectedContraventions != null)
                    {
                        foreach (int contraventionId in act.SelectedContraventions)
                        {
                            act.Contraventions.Add(db.Contraventions.Find(contraventionId));
                        }
                    }

                    if (act.SelectedObservations != null)
                    {
                        foreach (int observationId in act.SelectedObservations)
                        {
                            act.Observations.Add(db.Observations.Find(observationId));

                        }
                    }
                    //Guardo los archivos
                    if (fileUploadActa != null)
                        act.ActaAdjunta= SaveFile(fileUploadActa, act, "Acta");
                    if (fileUploadTicketAlcoholemiaAdjunto != null)
                        act.TicketAlcoholemiaAdjunto = SaveFile(fileUploadTicketAlcoholemiaAdjunto, act, "Ticket");
                    if (fileUploadTicketAlcoholemiaAdjunto != null)
                        act.InformeAdjunto= SaveFile(fileInformeAdjunto, act, "Informe");


                    db.Acts.Add(act);

                    db.SaveChanges();

                    TempData["message"] = "Los cambios se han grabado correctamente.";
                    //Audito
                    AuditHelper.Auditar("Alta", act.Id.ToString(), "Acts", ModuleDescription, WindowDescription);

                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(404, ex.Message.Replace("\r\n", ""));
                    //return View(act);
                }
                return RedirectToAction("Index", "Acts");
            }
            return View(act);
            //return View(act);
        }

        private string SaveFile(HttpPostedFileBase fileUpload, Act act, string folder)
        {

                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads/"+ folder + "/" + act.NroActa + "/"), fileName);
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                file.Directory.Create(); // If the directory already exists, this method does nothing.
                fileUpload.SaveAs(path);
                return  path;
        }

        // GET: Acts/Edit/5
        public ActionResult Edit(int? id)
        {

            int i = 0;
            List<VehicleType> lTipos = new List<VehicleType>();
            List<VehicleBrand> lMarcas = new List<VehicleBrand>();
            List<VehicleModel> lModelos = new List<VehicleModel>();
            List<Domain> lDominios = new List<Domain>();
            List<Police> lPolicias = new List<Police>();
            List<Inspector> lIspectores = new List<Inspector>();
            List<Contravention> lContravenciones = new List<Contravention>();
            List<Observation> lObservaciones = new List<Observation>();


            //lCalles = db.Streets.ToList();
            //  lBarrios = db.Nighborhoods.ToList();
            lTipos = db.VehicleTypes.ToList().Where(x => x.Enable == true).ToList();
            lMarcas = db.VehicleBrands.ToList().Where(x => x.Enable == true).ToList();
            lModelos = db.VehicleModels.ToList().Where(x => x.Enable == true).ToList();
            lDominios = db.Domains.ToList();
            lPolicias = db.Police.ToList().Where(x => x.Enable == true).ToList();
            lIspectores = db.Inspectors.ToList().Where(x => x.Enable == true).ToList();
            lContravenciones = db.Contraventions.ToList().Where(x => x.Enable == true).ToList();
            lObservaciones = db.Observations.ToList().Where(x => x.Enable == true).ToList();

            ViewBag.listaCalles = GetCalles();
            ViewBag.listaBarrios = GetBarrios();
            ViewBag.listaTipos = lTipos;
            ViewBag.listaMarcas = lMarcas;
            ViewBag.listaModelos = lModelos;
            ViewBag.listaDominios = lDominios;
            ViewBag.listaPolicias = lPolicias;
            ViewBag.listaInspectores = lIspectores;
            ViewBag.listaObservaciones = lObservaciones;
            ViewBag.listaContravenciones = lContravenciones;
           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act act = db.Acts.Find(id);            
//            act.File = new    //File(act.ActaAdjunta, MimeMapping.GetMimeMapping( Path.GetFileName(act.ActaAdjunta)), Path.GetFileName(act.ActaAdjunta));
            act.SelectedContraventions = new int[act.Contraventions.Count];
            act.SelectedObservations = new int[act.Observations.Count];

            //Inicializo en falso
            act.ActaAdjuntaBorrada = false;
            act.TicketAlcoholemiaAdjuntoBorrado = false;
            act.InformeAdjuntoBorrado = false;

            foreach (var item in act.Contraventions)
            {
               act.SelectedContraventions[i] = item.Id;
                i++;
            }

            i = 0;
            foreach (var item in act.Observations)
            {
                act.SelectedObservations[i] = item.Id;
                i++;
            }




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
        public ActionResult Edit([Bind(Include = "Id,TipoDeActa,NroActa,FechaInfraccion,Tanda,Calle,StreetId,Altura,EntreCalle,Barrio,NighborhoodId,FechaEnvioAlJuzgado,ActaAdjunta,FechaCarga,UsuarioId,VehicleTypeId,VehicleBrandId,VehicleModelId,Color,NroMotor,NroChasis,EstadoVehiculo,FechaEstado,TipoAgente,InspectorId,PoliceId,VehiculoRetenido,LicenciaRetenida,TicketAlcoholemia,ResultadoAlcoholemia,TicketAlcoholemiaAdjunto,Informe,InformeAdjunto,Detalle,Enable,DNI,Nombre,Apellido,NroLicencia,DomainId,Dominio,Contraventions,Observations,SelectedContraventions,SelectedObservations,ActaAdjuntaBorrada,TicketAlcoholemiaAdjuntoBorrado,InformeAdjuntoBorrado")] Act act, HttpPostedFileBase fileUploadActa, HttpPostedFileBase fileUploadTicketAlcoholemiaAdjunto, HttpPostedFileBase fileInformeAdjunto)
        {
            if (ModelState.IsValid)
            {
                act.Calle = act.Calle?.ToUpper();
                act.Barrio = act.Barrio?.ToUpper();
                act.Altura = act.Altura?.ToUpper();
                act.Color = act.Color?.ToUpper();
                act.Dominio = act.Dominio?.ToUpper();
                act.Nombre = act.Nombre?.ToUpper();
                act.Apellido = act.Apellido?.ToUpper();
                act.EntreCalle = act.EntreCalle?.ToUpper();
                act.NroActa = act.NroActa?.ToUpper();
                act.NroChasis = act.NroChasis?.ToUpper();
                act.NroLicencia = act.NroLicencia?.ToUpper();
                act.NroMotor = act.NroMotor?.ToUpper();

                act.Contraventions = new List<Contravention>();
                act.Observations = new List<Observation>();

                if (act.SelectedContraventions != null)
                {
                    foreach (int contraventionId in act.SelectedContraventions)
                    {
                        act.Contraventions.Add(db.Contraventions.Find(contraventionId));
                    }
                }
                if (act.SelectedObservations != null)
                {
                    foreach (int observationId in act.SelectedObservations)
                    {
                        act.Observations.Add(db.Observations.Find(observationId));

                    }
                }

                //Guardo los archivos
                if (fileUploadActa == null)
                {
                    //puede venir nulo porque no hizo ningun cambio o porque lo borro

                    //Si la borro
                    if (act.ActaAdjuntaBorrada)
                    {
                        //fisicamente
                        var file = Path.Combine(act.ActaAdjunta);
                        if (System.IO.File.Exists(file))
                            System.IO.File.Delete(file);
                        //logicamente
                        act.ActaAdjunta = null;                        

                    }
                    //Si no la borro, lo dejo como esta
                }
                else
                {
                    //Borro la anterior por si la reemplazo
                    if (!String.IsNullOrEmpty( act.ActaAdjunta))
                    {
                        //fisicamente
                        var file = Path.Combine(act.ActaAdjunta);
                        if (System.IO.File.Exists(file))
                            System.IO.File.Delete(file);
                    }

                    if (fileUploadActa.ContentLength > 0)
                    {
                        act.ActaAdjunta = SaveFile(fileUploadActa, act, "Acta");
                    }
                    else
                    {
                        act.ActaAdjunta = null;
                    }
                }


                if (fileUploadTicketAlcoholemiaAdjunto == null)
                {
                    //puede venir nulo porque no hizo ningun cambio o porque lo borro

                    //Si la borro
                    if (act.TicketAlcoholemiaAdjuntoBorrado)
                    {
                        //fisicamente
                        var file = Path.Combine(act.TicketAlcoholemiaAdjunto);
                        if (System.IO.File.Exists(file))
                            System.IO.File.Delete(file);
                        //logicamente
                        act.TicketAlcoholemiaAdjunto = null;

                    }
                    //Si no la borro, lo dejo como esta
                }
                else
                {
                    //Borro la anterior por si la reemplazo
                    if (!String.IsNullOrEmpty(act.TicketAlcoholemiaAdjunto))
                    {
                        //fisicamente
                        var file = Path.Combine(fileUploadTicketAlcoholemiaAdjunto.FileName);
                        if (System.IO.File.Exists(file))
                            System.IO.File.Delete(file);
                    }

                    if (fileUploadTicketAlcoholemiaAdjunto.ContentLength > 0)
                    {
                      act.TicketAlcoholemiaAdjunto=  SaveFile(fileUploadTicketAlcoholemiaAdjunto, act, "Ticket");
                    }
                    else
                    {
                        act.TicketAlcoholemiaAdjunto = null;
                    }
                }

                //Guardo los archivos
                if (fileInformeAdjunto == null)
                {
                    //puede venir nulo porque no hizo ningun cambio o porque lo borro

                    //Si la borro
                    if (act.InformeAdjuntoBorrado)
                    {
                        //fisicamente
                        var file = Path.Combine(act.InformeAdjunto);
                        if (System.IO.File.Exists(file))
                            System.IO.File.Delete(file);
                        //logicamente
                        act.InformeAdjunto = null;

                    }
                    //Si no la borro, lo dejo como esta
                }
                else
                {
                    //Borro la anterior por si la reemplazo
                    if (!String.IsNullOrEmpty(act.ActaAdjunta))
                    {
                        //fisicamente
                        var file = Path.Combine(act.ActaAdjunta);
                        if (System.IO.File.Exists(file))
                            System.IO.File.Delete(file);
                    }

                    if (fileInformeAdjunto.ContentLength > 0)
                    {
                        act.InformeAdjunto = SaveFile(fileInformeAdjunto, act, "Informe");
                    }
                    else
                    {
                        act.ActaAdjunta = null;
                    }
                }



                db.Entry(act).State = EntityState.Modified;
                db.SaveChanges();

                TempData["message"] = "Los cambios se han grabado correctamente.";

                AuditHelper.Auditar("Modificacion", act.Id.ToString(), "Acts", ModuleDescription, WindowDescription);

                return RedirectToAction("Index", "Acts");
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

        public JsonResult DeleteAct(int id)
        {
            if (id == 0)
            {
                return Json(new { responseCode = "-10" });
            }

            Act act = db.Acts.Find(id);
            act.Enable = false;
            db.Entry(act).State = EntityState.Modified;
            db.SaveChanges();

            AuditHelper.Auditar("Baja", act.Id.ToString(), "Acts", ModuleDescription, WindowDescription);

            var responseObject = new
            {
                responseCode = 0
            };

            return Json(responseObject);


        }

        // POST: Acts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Act act = db.Acts.Find(id);
            db.Acts.Remove(act);
            db.SaveChanges();

            AuditHelper.Auditar("Baja", act.Id.ToString(), "Acts", ModuleDescription, WindowDescription);

            return RedirectToAction("Index");
        }


        public FileResult Download(string filePath)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@filePath);
            string fileName = Path.GetFileName(filePath);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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
