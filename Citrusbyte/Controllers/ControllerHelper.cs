using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Citrusbyte.Controllers
{
    internal static class ControllerHelper
    {
        #region Static Fields and Constants

        private static string _goodConnectionName;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Determines which connection string is active
        /// </summary>
        /// <returns>The name of the active connection string.</returns>
        public static string GetActiveConnectionString()
        {
            if (!string.IsNullOrEmpty(_goodConnectionName))
            {
                return _goodConnectionName;
            }

            var connectionStrings = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>();
            foreach (var cs in connectionStrings)
            {
                // short circuit the empty or null strings
                if (string.IsNullOrEmpty(cs.ConnectionString))
                {
                    continue;
                }

                using (var conn = new SqlConnection(cs.ConnectionString))
                {
                    try
                    {
                        // Can't use OpenAsync here because the calling framework code requires an instantiated ApplicationDbContext
                        conn.Open();
                        conn.Close();
                        _goodConnectionName = cs.Name;
                        return cs.Name;
                    }
                    catch (SqlException)
                    {
                        // the only way, in C#, to test a connection string is to try it.
                        // if it fails to open, a SqlException will be throw.
                        // this is expected if the connection string was invalid, so just swallow this exception.
                    }
                    catch (InvalidOperationException)
                    {
                        // can be thrown if the connection string is poorly formatted
                    }
                }
            }

            throw new Exception("There were no connection string the opened the database.");
        }

        #endregion
    }
}