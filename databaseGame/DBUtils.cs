using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace databaseGame
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource =...

            string database = "eric test";
            string username = ...
            string password = "...

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}
