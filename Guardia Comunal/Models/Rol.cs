using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Rol
    {
        public int RolId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

       //[JsonIgnore] 
       //[IgnoreDataMember]
       // public virtual List<Usuario> Usuarios { get; set; }

    }
}