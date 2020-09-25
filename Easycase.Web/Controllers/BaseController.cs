using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easycase.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            //Log the error!!

            //Redirect to action
            filterContext.Result = RedirectToAction("Error", "InternalError");

            // OR return specific view
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Error/InternalError.cshtml"
            };
        }
    }
}