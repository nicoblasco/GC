using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Liberation
    {
        public int Id { get; set; }
        public int ActaId { get; set; }
        public virtual Act Acta { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string NroLiberacion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public int LiberationPlaceId { get; set; }
        public virtual LiberationPlace LiberationPlace { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public DateTime FechaDeLiberacion { get; set; }
        public string NroJuzgado { get; set; }
        public DateTime FechaCarga { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public string Convenio { get; set; }
        public Int32 Cuotas { get; set; }
        [DataType(DataType.Currency)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public decimal Acarreo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string NroRecibo { get; set; }
        [DataType(DataType.Currency)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public decimal Importe { get; set; }
        [DataType(DataType.Currency)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public decimal MontoEnCuotas { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public DateTime FechaEmisionRecibo { get; set; }
        public bool Enable { get; set; }
    }
}