using System.Web.Http;
using Citrusbyte.Models;

namespace Citrusbyte.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Base class for Web Api controllers.  Provides common functionality such as assess to the DB and a means of
    ///     determining connection string.
    /// </summary>
    public abstract class WebApiControllerBase : ApiController
    {
        #region Fields

        protected readonly ApplicationDbContext DB;

        #endregion

        #region Constructors

        /// <inheritdoc />
        protected WebApiControllerBase() => DB = new ApplicationDbContext(ControllerHelper.GetActiveConnectionString());

        #endregion

        #region Protected Methods

        /// <inheritdoc />
        /// <summary>
        ///     Disposes of the <see cref="T:System.Data.Entity.DbContext" />
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