using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PERFECT.CLICK.API.Filters
{
    public class LogFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public LogFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("Log Filter");
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("Entering", filterContext.RouteData, filterContext.ActionArguments);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("Exiting", filterContext.RouteData);
        }

        private void Log(String method, RouteData routeData, IDictionary<String, object> arguments = null)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            StringBuilder message = new StringBuilder();
            message.Append(String.Format($"{method} : Controller:{controllerName} ; action:{actionName} ; "));
            if (arguments != null && arguments.Count > 0)
            {
                message.Append("Arguments = ");
                foreach (var keyValue in arguments)
                    message.Append($"{keyValue.Key} : {keyValue.Value.ToString()} ; ");
            }
            _logger.LogInformation(message.ToString());
        }
    }
}
