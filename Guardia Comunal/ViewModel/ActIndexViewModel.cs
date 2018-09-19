using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guardia_Comunal.ViewModel
{
    public class ActIndexViewModel
    {
        public int Id { get; set; }
        public string NroActa { get; set; }
        public string NroLiberacion { get; set; }

        public DateTime FechaCarga { get; set; }

        public DateTime FechaInfraccion { get; set; }
        public string Dominio { get; set; }

        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Estado { get; set; }
    }
}