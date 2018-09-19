using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string DNI { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string Apellido { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requerido")]
        public string Nombre { get; set; }
        public string Calle { get; set; }
        public int? StreetId { get; set; }
        public virtual Street Street { get; set; }
        public string Altura { get; set; }
        public string Barrio { get; set; }
        public int? NighborhoodId { get; set; }
        public virtual Nighborhood Nighborhood { get; set; }
        public string EntreCalle { get; set; }
        public string Partido { get; set; }
    }
}