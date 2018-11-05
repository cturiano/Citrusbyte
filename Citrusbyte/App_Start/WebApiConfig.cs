using System.Web.Http;

namespace Citrusbyte
{
    /// <summary>
    /// Config for Asp.NET Web API
    /// </summary>
    public class WebApiConfig
    {
        /// <summary>
        /// Register the routing
        /// </summary>
        /// <param name="configuration"></param>
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{action}/{id}", new {id = RouteParameter.Optional, action = RouteParameter.Optional});
        }
    }
}