using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class VehicleBrand
    {
        public int Id { get; set; }

        public int VehicleTypeId { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Enable { get; set; }
    }
}