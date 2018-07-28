using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace Homework.Controllers
{
    public class ExcutingTimeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Debug.WriteLine("[" + filterContext.ActionDescriptor.ActionName + "] OnActionExecuting Time: " + DateTime.Now);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Debug.WriteLine("[" + filterContext.RouteData.Values["action"] + "] OnResultExecuted Time: " + DateTime.Now);
        }
    }
}