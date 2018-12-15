using GuardiaComunal.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Guardia_Comunal.ViewModel
{
    public class LiberationViewModel
    {
        public int Id { get; set; }

        public int ActaId { get; set; }
        public virtual Act Acta { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
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

        //Validacion de Datos obligatorios
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public int DomainId { get; set; }        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string Dominio { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string NroMotor { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string NroChasis { get; set; }

        public bool Enable { get; set; }
    }
}