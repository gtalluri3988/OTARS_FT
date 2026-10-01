using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTAoarsFamily.Services.Shared;
using MTAoarsFamily.Utilities.Extensions;

namespace MTAoarsFamily.Shell.Controllers {
    
    public class BaseController : Controller {

        protected override void OnResultExecuted(ResultExecutedContext filterContext) {
            if (!Request.IsAjaxRequest()) {
                var json = ModelState.GetErrorsJson();
                filterContext.HttpContext.Response.Write("<script type='text/Javascript'>$(document).ready(function(){onResultSuccess(" + json + ");});</script>");
            }
            base.OnResultExecuted(filterContext);
        }


        public void Check(ServiceContext context) {
            if (context.IsSuccess) return;
            foreach (var msg in context.ValidationMessages) {
                ModelState.AddModelError(msg.ErrorKey, msg.ErrorMessage);
            }
        }



    }
}
