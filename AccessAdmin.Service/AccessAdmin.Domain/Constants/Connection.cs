using System.Data.SqlClient;

namespace AccessAdmin.Domain.Constants
{
    public static class Connection
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(Queries.ConnectionString);
        }
    }
}
