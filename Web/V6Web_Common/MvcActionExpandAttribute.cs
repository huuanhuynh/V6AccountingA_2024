using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace V6Soft.Web.Common
{
    public class MvcActionExpandAttribute : ActionFilterAttribute
    {
        public MvcActionExpandAttribute()
        {
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            string actionName = actionContext.ActionDescriptor.ActionName;
            IHttpController controller = actionContext.ControllerContext.Controller;

            MethodInfo method = controller.GetType().GetMethod("Before" + actionName);
            if (null == method) { return; }

            method.Invoke(controller, null);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            
            HttpResponseMessage response = actionExecutedContext.Response;

            if (response == null || !response.IsSuccessStatusCode || response.Content == null) { return; }

            ObjectContent responseContent = response.Content as ObjectContent;
            if (responseContent == null) { return; }

            string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            IHttpController controller = actionExecutedContext.ActionContext.ControllerContext.Controller;

            MethodInfo method = controller.GetType().GetMethod("After" + actionName);
            if (null == method) { return; }

            object modifiedResponse = method.Invoke(controller, new object[] { responseContent.Value });
            if (null == modifiedResponse) { return; }

            responseContent.Value = modifiedResponse;
        }
    }
}
