using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class PoliceStation
    {
        public int Id { get; set; }
        public DateTime FechaAlta { get; set; }

        public string Direccion { get; set; }
        public string Altura { get; set; }
        public string Partida { get; set; }
        public string Barrio { get; set; }
        public string Sede { get; set; }

        public string Detalle { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public bool Enable { get; set; }
    }
}