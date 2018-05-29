using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Liberation
    {
        public int Id { get; set; }
        public Act Acta { get; set; }
        public string NroLiberacion { get; set; }
        public LiberationPlace LiberationPlace { get; set; }
        public DateTime FechaDeLiberacion { get; set; }
        public string NroJuzgado { get; set; }
        public DateTime FechaCarga { get; set; }
        public Usuario User { get; set; }
        public Person Person { get; set; }
        public string Convenio { get; set; }
        public Int32 Cuotas { get; set; }
        public decimal Acarreo { get; set; }
        public string NroRecibo { get; set; }
        public decimal Importe { get; set; }
        public decimal MontoEnCuotas { get; set; }
        public DateTime FechaEmisionRecibo { get; set; }
        public bool Enable { get; set; }
    }
}