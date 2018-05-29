using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Enable { get; set; }

    }
}