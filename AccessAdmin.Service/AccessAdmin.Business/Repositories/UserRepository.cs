using System;
using System.Data;
using System.Data.SqlClient;
using AccessAdmin.Business.Interfaces;
using AccessAdmin.Domain.Constants;
using AccessAdmin.Domain.Model;
using NLog;

namespace AccessAdmin.Business.Repositories
{
    public class UserRepository : IUserRepository
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public User GetUserByEmail(string email)
        {
            var user = new User();
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.GetUserByEmailQuery, con);
                    cmd.Parameters.Add("@mail", SqlDbType.NVarChar).Value = email;

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            user.UserId = (int)reader[0];
                            user.Email = reader[1].ToString();
                            user.FirstName = reader[2].ToString();
                            user.LastName = reader[3].ToString();
                        }
                    }

                    reader.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in GetUserByEmail - {e}");
            }

            return user;
        }
    }
}
