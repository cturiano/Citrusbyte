using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Citrusbyte
{
    /// <inheritdoc />
    /// <summary>
    ///     MVC Application
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        #region Protected Methods

        /// <summary>
        ///     Called when the application starts.  Registers all the importants stuff
        /// </summary>
        protected void Application_Start()
        {
            // try to help performance by only adding the rendering engines we're using
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine {FileExtensions = new[] {"cshtml"}});

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        #endregion
    }
}