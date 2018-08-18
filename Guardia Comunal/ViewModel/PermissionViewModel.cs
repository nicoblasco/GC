using Guardia_Comunal.Helpers;
using Guardia_Comunal.Models;
using GuardiaComunal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guardia_Comunal.ViewModel
{
    public class PermissionViewModel
    {
        //public static bool TienePermisoAlta(int WindowId, PermissionTypes permissionTypes )
        //{
        //    //foreach (var item in PermissionViewModel.GetPermisos())
        //    //{

        //    //}

        //    List<Permission> permission = new List<Permission>();
        //    permission = GetPermisos();

        //    switch (permissionTypes)
        //    {
        //        case PermissionTypes.Consulta:
        //            permission.Where(x => x.WindowId == WindowId && x.Consulta == true);
        //            break;               
        //    }            
        //}

        public static List<Permission> GetPermisos()
        {
            Usuario usuario = new Usuario();
            usuario.Obtener(SessionHelper.GetUser());
            return new Permission().Obtener(usuario.RolId);
        }
    }
}