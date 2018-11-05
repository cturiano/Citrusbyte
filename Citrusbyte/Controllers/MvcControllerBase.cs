using System.Web.Mvc;
using Citrusbyte.Models;

namespace Citrusbyte.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Base class for MVC controllers.  Provides common functionality such as assess to the DB and a means of determining
    ///     connection string.
    /// </summary>
    public abstract class MvcControllerBase : Controller
    {
        #region Fields

        protected ApplicationDbContext DB;

        #endregion

        #region Constructors

        /// <inheritdoc />
        protected MvcControllerBase() => DB = new ApplicationDbContext(ControllerHelper.GetActiveConnectionString());

        #endregion

        #region Protected Methods

        /// <inheritdoc />
        /// <summary>
        ///     Disposes of the <see cref="T:System.Data.Entity.DbContext" /> object
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}