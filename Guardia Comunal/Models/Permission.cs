﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Permission
    {
        public int Id { get; set; }

        public int WindowId { get; set; }
        public virtual Window Window { get; set; }

        public int RolId { get; set; }
        public virtual Rol Role { get; set; }
        public bool Consulta { get; set; }
        public bool AltaModificacion { get; set; }
        public bool Baja { get; set; }
        public DateTime Fecha { get; set; }
    }
}