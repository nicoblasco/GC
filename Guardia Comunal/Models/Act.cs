using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Act
    {
        public int Id { get; set; }
        public string TipoDeActa { get; set; }
        public string NroActa { get; set; }
        public DateTime FechaInfraccion { get; set; }
        public Int32 Tanda { get; set; }
        public string Calle { get; set; }
        public string Altura { get; set; }
        public string EntreCalle { get; set; }
        public string Barrio { get; set; }
        public DateTime FechaEnvioAlJuzgado { get; set; }
        public string ActaAdjunta { get; set; }
        public DateTime FechaCarga { get; set; }
        public Usuario User { get; set; }
        public VehicleType VehicleType { get; set; }
        public Domain TipoDeDominio { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public string Color { get; set; }
        public string NroMotor { get; set; }
        public string NroChasis { get; set; }
        public string EstadoVehiculo { get; set; }
        public DateTime FechaEstado { get; set; }
        public string TipoAgente { get; set; }
        public Inspector Inspector { get; set; }
        public Police Police { get; set; }
        public bool VehiculoRetenido { get; set; }
        public bool LicenciaRetenida { get; set; }
        public bool TicketAlcoholemia { get; set; }
        public string ResultadoAlcoholemia { get; set; }
        public string TicketAlcoholemiaAdjunto { get; set; }
        public string Informe { get; set; }
        public string InformeAdjunto { get; set; }
        public List<Contravention> ListContravenciones { get; set; }
        public List<Observation> ListObservaciones { get; set; }
        public string Detalle { get; set; }
        public bool Enable { get; set; }



    }
}