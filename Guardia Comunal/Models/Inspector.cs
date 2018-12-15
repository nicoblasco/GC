using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Inspector
    {
        public int Id { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Legajo { get; set; }

        public DateTime FechaAlta { get; set; }
        public bool Enable { get; set; }


        [NotMapped]
        public string FullName => Apellido + " " + Nombre;

        [NotMapped]
        public string FullNameLeg => Legajo + " - " + Apellido + " " + Nombre;

    }
}