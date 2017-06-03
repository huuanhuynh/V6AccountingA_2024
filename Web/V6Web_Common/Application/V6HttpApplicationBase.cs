using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using V6Soft.Common.Logging;
using V6Soft.Services.Common.Infrastructure;
using V6Soft.Web.Common.Constants;


namespace V6Soft.Web.Common.Application
{
    /// <summary>
    /// Provides basic behaviours for V6 Http Applications.<br/>
    /// A Http Application is used to host one or more App Engines.<br/>
    /// V6 Http Application includes routing, controllers, web views... while
    ///     V6 Winform Application consists of UI forms, event handlers...
    /// </summary>
    public abstract class V6HttpApplicationBase : System.Web.HttpApplication
    {
        private static V6AppEngineBase s_AppEngine;
        private static ILogger s_Logger;

        protected V6AppEngineBase WebApplication
        {
            get { return s_AppEngine; }
        }

        protected ILogger Logger
        {
            get { return s_Logger; }
        }

        protected void Application_Start()
        {
            // Must use this to debug Http Application
            // System.Diagnostics.Debugger.Launch();

            s_AppEngine = CreateAppEngine(null);
            s_Logger = AppContext.Logger;

            Logger.LogTraceEntry("V6HttpApplicationBase Application_Start");

            TaskScheduler.UnobservedTaskException += OnTaskSchedulerUnobservedTaskException;
            s_AppEngine
                .Start()
                .ContinueWith((antecedent) =>
                {
                    // Http Application is considered "started" only when App Engine has started.
                    OnApplicationStart();
                })
                .Wait();

            Logger.LogTraceExit("V6HttpApplicationBase Application_Start");
        }
        
        protected virtual void OnApplicationStart() { }

        protected virtual void OnApplicationStop()
        {
            s_AppEngine.Stop();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                LogException(exception, Request.RequestContext.HttpContext);
                Server.ClearError();
                //Response.Redirect(exception.ToRedirectUrl() + (Request.RequestContext.HttpContext.Request.IsAjaxRequest() ? "?ajax=1" : ""));
            }
        }

        protected abstract V6AppEngineBase CreateAppEngine(string serviceInstanceId);

        
        private void LogException(Exception exception, HttpContextBase context)
        {
            HttpRequestBase Request = context.Request;
            string exceptionMessage = new StringBuilder("An error occured in V6 Accounting Web, request coming from IP: ")
                       .Append(Request.ServerVariables[Names.ServerVariables.RemoteIP]).Append(Environment.NewLine)
                       .Append("Url: " + Request.Url)
                       .ToString();
            // AlertsManager.Instance.SendAlertError(exceptionMessage, exception); // TODO: cause an exception, check configuration service
            Logger.LogException(exceptionMessage, exception);
        }

        private void OnTaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            if (!e.Observed)
            {
                e.SetObserved();
            }

            e.Exception.Handle((ex) =>
            {
                Logger.LogException("TASK ERROR", ex);
                return true;
            });
        }
    
    }
}
