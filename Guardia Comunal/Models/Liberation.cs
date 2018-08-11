using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Liberation
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public Act Acta { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string NroLiberacion { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public LiberationPlace LiberationPlace { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public DateTime FechaDeLiberacion { get; set; }
        public string NroJuzgado { get; set; }
        public DateTime FechaCarga { get; set; }
        public Usuario User { get; set; }
        public Person Person { get; set; }
        public string Convenio { get; set; }
        public Int32 Cuotas { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public decimal Acarreo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string NroRecibo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public decimal Importe { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public decimal MontoEnCuotas { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public DateTime FechaEmisionRecibo { get; set; }
        public bool Enable { get; set; }
    }
}