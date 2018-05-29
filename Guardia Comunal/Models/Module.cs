using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public bool Enable { get; set; }

        public List<Window> ListWindows { get; set; }
    }
}