using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public Usuario User { get; set; }
        public Window Window { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }



    }
}