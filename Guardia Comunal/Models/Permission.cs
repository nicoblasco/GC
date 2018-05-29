using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public Window Window { get; set; }
        public Rol Role { get; set; }
        public bool Consulta { get; set; }
        public bool AltaModificacion { get; set; }
        public bool Baja { get; set; }
        public DateTime Fecha { get; set; }
    }
}