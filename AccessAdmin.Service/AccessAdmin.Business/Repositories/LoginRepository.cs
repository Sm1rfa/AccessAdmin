using System;
using System.Data;
using System.Data.SqlClient;
using AccessAdmin.Business.Interfaces;
using AccessAdmin.Domain.Constants;
using AccessAdmin.Domain.Model;

namespace AccessAdmin.Business.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        public int AuthenticateUser(string email, string password)
        {
            var count = 0;
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.AuthenticateUserQuery, con);
                    cmd.Parameters.Add("@mail", SqlDbType.NVarChar).Value = email;
                    cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = password;

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            count = (int)reader[0];
                        }
                    }

                    reader.Dispose();
                    cmd.Dispose();
                }

                return count;
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }

        public bool IsUserAdmin(string email)
        {
            var isAdmin = false;
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.IsUserAdminQuery, con);
                    cmd.Parameters.Add("@mail", SqlDbType.NVarChar).Value = email;

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            isAdmin = (bool)reader[0];
                        }
                    }

                    reader.Dispose();
                    cmd.Dispose();
                }

                return isAdmin;
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }
    }
}
