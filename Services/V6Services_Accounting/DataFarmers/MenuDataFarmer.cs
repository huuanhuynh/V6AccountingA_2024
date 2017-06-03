using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Dapper;

using V6Soft.Models.Core;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Services.Accounting.Constants;
using V6Soft.Common.Utils;


namespace V6Soft.Services.Accounting.DataFarmers
{
    /// <summary>
    ///     Implements <see cref="IMenuDataFarmer"/>
    /// </summary>
    public class MenuDataFarmer : IMenuDataFarmer
    {
        private string m_ConnectionString;


        /// <summary>
        ///     Initializes a new instance of MenuDataFarmer class
        ///     with specified <paramref name="connectionString"/>
        /// </summary>
        /// <param name="connectionString">Must not be null</param>
        public MenuDataFarmer(string connectionString)
        {
            Guard.ArgumentNotNullOrEmpty(connectionString, "connectionString");
            m_ConnectionString = connectionString;
        }

        /// <summary>
        ///     See <see cref="IMenuDataFarmer.GetMenuItems"/>
        /// </summary>
        public DisposableEnumerable<MenuItem> GetMenuItems()
        {
            SqlConnection connection = CreateConnection();
            IEnumerable<MenuItem> menuItems;

            menuItems = connection.Query<MenuItem>(StoreProcedures.GetMenuItems,
                commandType: CommandType.StoredProcedure, buffered: false);
            return MakeDisposable(connection, menuItems);
        }

        /// <summary>
        ///     See <see cref="IMenuDataFarmer.GetChildren"/>
        /// </summary>
        public DisposableEnumerable<MenuItem> GetChildren(int oid)
        {
            SqlConnection connection = CreateConnection();
            IEnumerable<MenuItem> menuItems;

            menuItems = connection.Query<MenuItem>(StoreProcedures.GetMenuChildren,
                new { OID = oid }, commandType: CommandType.StoredProcedure,
                buffered: false);
            return MakeDisposable(connection, menuItems);
        }
        

        #region Private methods

        private DisposableEnumerable<MenuItem> MakeDisposable(SqlConnection connection, IEnumerable<MenuItem> menuItems)
        {
            var disposableResult = new DisposableEnumerable<MenuItem>(menuItems);
            disposableResult.Disposed += (object sender, System.EventArgs e) =>
            {
                connection.Dispose();
            };

            return disposableResult;
        }

        private SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(m_ConnectionString);
            connection.Open();
            return connection;
        }

        #endregion
    }
}
