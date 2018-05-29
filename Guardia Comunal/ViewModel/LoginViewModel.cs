using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Guardia_Comunal.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string NombreDeUsuario { get; set; }

        [Required]
        public string Password { get; set; }
    }
}