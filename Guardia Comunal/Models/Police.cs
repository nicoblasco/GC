using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Police
    {
        public int Id { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public int PoliceStationId { get; set; }
        public virtual PoliceStation PoliceStation { get; set; }

        public DateTime FechaAlta { get; set; }
        public bool Enable { get; set; }
    }
}