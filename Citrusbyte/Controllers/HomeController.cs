using System.Web.Mvc;

namespace Citrusbyte.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Class for actions related to the first page of the application
    /// </summary>
    public class HomeController : Controller
    {
        #region Public Methods

        /// <summary>
        ///     Gets the index view
        /// </summary>
        /// <returns></returns>
        [OverrideAuthorization]
        public ActionResult Index() => View();

        #endregion
    }
}