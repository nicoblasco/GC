using Guardia_Comunal.Helpers;
using Guardia_Comunal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardiaComunal.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Nombreusuario { get; set; }

        public string Contraseña { get; set; }

        public bool Enable { get; set; }

        public Rol Role { get; set; }

        public ResponseModel Autenticarse()
        {
            var rm = new ResponseModel();

            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var usuario = ctx.Usuarios.Where(x => x.Nombreusuario == this.Nombreusuario && x.Contraseña == this.Contraseña).SingleOrDefault();
                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(usuario.Id.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Acceso denegado al sistema");
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }

    }
}