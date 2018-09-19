using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Guardia_Comunal.Helpers
{
    public static class BootstrapExtensions
    {

        public static MvcForm BeginHorizontalForm(this HtmlHelper helper)
        {
            return helper.BeginForm(null, null, FormMethod.Post, new { @class = "form-horizontal" });
        }
    }
}