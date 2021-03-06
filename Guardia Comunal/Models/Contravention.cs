﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GuardiaComunal.Models
{
    [DataContract(IsReference = true)]
    public class Contravention
    {
        public int Id { get; set; }
        public string NroArticulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Enable { get; set; }

        [JsonIgnore]
        public virtual ICollection<Act> Acts { get; set; }
    }
}