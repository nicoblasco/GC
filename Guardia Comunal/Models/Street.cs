using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Street
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Direccion { get; set; }
        public string Altura { get; set; }
        public string Partida { get; set; }

        public string Barrio { get; set; }
        public string Nombre { get; set; }

        public int NighborhoodId { get; set; }

        public string BarrioConId { get; set; }
        public bool Luz { get; set; }
        public bool Agua { get; set; }
        public bool Cloaca { get; set; }
        public bool Gas { get; set; }
        public string Pavimento { get; set; }
        public string TipoPavimento { get; set; }
        public bool Cord { get; set; }
        public bool RutaProvincial { get; set; }
        public string CodCalleTexto { get; set; }
        public string ImpDesde { get; set; }
        public string ParDesde { get; set; }
        public string ImpHasta { get; set; }
        public string ParHasta { get; set; }
        public Int32 CodCalle { get; set; }
        public string UltimoPavimento { get; set; }
        public string Detalle { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Perimetro { get; set; }

    }
}