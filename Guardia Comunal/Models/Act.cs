
using ExpressiveAnnotations.Attributes;
using Guardia_Comunal.Validations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace GuardiaComunal.Models
{
    public class Act
    {

        // -----------Datos del Acta
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string TipoDeActa { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string NroActa { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public DateTime FechaInfraccion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public Int32 Tanda { get; set; }

        //La descripcion de la calle la guardo por si no la encuentra en el maestro

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string Calle { get; set; }
        //Este es el codigo de geolocalizacion
        public int? StreetId { get; set; }
        public virtual Street Street { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string Altura { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string EntreCalle { get; set; }

        //La descripcion del barrio la guardo por si no la encuentra en el maestro
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string Barrio { get; set; }

        public int? NighborhoodId { get; set; }
        public virtual Nighborhood Nighborhood { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
      //  [DateGreaterThan("FechaInfraccion", "Estimated end date must be greater than the start date of the project")]
        public DateTime FechaEnvioAlJuzgado { get; set; }
        public string ActaAdjunta { get; set; }

        [NotMapped]
        public virtual bool ActaAdjuntaBorrada { get; set; }
        public DateTime FechaCarga { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        // -----------Datos del Vehiculo

        [RequiredIf("TipoDeActa == 'Documentada'", ErrorMessage = "Requerido")]
        public string DNI { get; set; }
        [RequiredIf("TipoDeActa == 'Documentada'", ErrorMessage = "Requerido")]
        public string Nombre { get; set; }
        [RequiredIf("TipoDeActa == 'Documentada'", ErrorMessage = "Requerido")]
        public string Apellido { get; set; }

        [RequiredIf("LicenciaRetenida == true", ErrorMessage = "Requerido")]
        public string NroLicencia { get; set; }

        // -----------Datos del Vehiculo
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public int VehicleTypeId { get; set; }
        public virtual VehicleType VehicleType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public int DomainId { get; set; }

        public virtual Domain Domain { get; set; }

        public string Dominio { get; set; }
        public int? VehicleBrandId { get; set; }
        public virtual VehicleBrand VehicleBrand { get; set; }

        public int? VehicleModelId { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }
        public string Color { get; set; }
        public string NroMotor { get; set; }
        public string NroChasis { get; set; }
        public string EstadoVehiculo { get; set; }
        public DateTime FechaEstado { get; set; }


        // -----------Datos del Acta
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string TipoAgente { get; set; }
        public int? InspectorId { get; set; }
        public virtual Inspector Inspector { get; set; }
        public int? PoliceId  { get; set; }
        public virtual Police Police { get; set; }
        public bool VehiculoRetenido { get; set; }
        public bool LicenciaRetenida { get; set; }
        public bool TicketAlcoholemia { get; set; }

        [RequiredIf("TicketAlcoholemia == true", ErrorMessage = "Requerido")]
        public string ResultadoAlcoholemia { get; set; }
       
        public string TicketAlcoholemiaAdjunto { get; set; }
        [NotMapped]
        public virtual bool TicketAlcoholemiaAdjuntoBorrado { get; set; }
        
        public string Informe { get; set; }
        public string InformeAdjunto { get; set; }

        [NotMapped]
        public virtual bool InformeAdjuntoBorrado { get; set; }

        [ScriptIgnore]
        public virtual ICollection<Contravention> Contraventions { get; set; }

        public virtual int[] SelectedContraventions { get; set; }

        public virtual ICollection<Observation> Observations { get; set; }
        public virtual int[] SelectedObservations { get; set; }
        public string Detalle { get; set; }
        public bool Enable { get; set; }


    }
}