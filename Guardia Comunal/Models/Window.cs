using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Window
    {
        public int Id { get; set; }
        public Module Module { get; set; }
        public string Descripcion { get; set; }
        public bool Enable { get; set; }
        public string Url { get; set; }
        public string Orden { get; set; }




    }
}