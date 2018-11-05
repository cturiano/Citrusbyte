using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Citrusbyte.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Citrusbyte.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// <inheritdoc />
    public class ApplicationUser : IdentityUser
    {
        #region Public Methods

        /// <summary>
        ///     Creates a user from the given <see cref="UserManager{ApplicationUser}" />
        /// </summary>
        /// <param name="manager">The <see cref="UserManager{ApplicationUser}" /> to use to create the user</param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        #endregion
    }

    /// <inheritdoc />
    /// <summary>
    ///     The Entity Framework database context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructors

        /// <inheritdoc />
        /// <summary>
        ///     Default constructor
        /// </summary>
        public ApplicationDbContext() : base("DefaultConnection", false)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Parameterized constructor
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        public ApplicationDbContext(string connectionString) : base(connectionString, false)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     All the devices in the database
        /// </summary>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        ///     All the readings in the database
        /// </summary>
        public DbSet<SensorReading> SensorReadings { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Creates an ApplicationDBContext asyncronously
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create() => new ApplicationDbContext(ControllerHelper.GetActiveConnectionString());

        #endregion
    }
}