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
            string datasource = @"TESQL3";

            string database = "eric test";
            string username = "web.user";
            string password = "webuser02182000";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}
