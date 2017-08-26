using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AccessAdmin.Business.Interfaces;
using AccessAdmin.Domain.Constants;
using AccessAdmin.Domain.Model;

namespace AccessAdmin.Business.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        public List<Systems> GetAllSystems()
        {
            var list = new List<Systems>();
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.GetAllSystemsQuery, con);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new Systems
                            {
                                SystemId = (int)reader[0],
                                SystemName = reader[1].ToString(),
                                IsDeleted = (bool)reader[2]
                            });
                        }
                    }

                    reader.Dispose();
                    cmd.Dispose();
                }

                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }

        public Systems GetSystem(int id)
        {
            var sys = new Systems();
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.GetSingleSystemQuery, con);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            sys.SystemId = (int) reader[0];
                            sys.SystemName = reader[1].ToString();
                            sys.IsDeleted = (bool) reader[2];
                        }
                    }

                    reader.Dispose();
                    cmd.Dispose();
                }

                return sys;
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }

        public void CreateSystem(Systems sys)
        {
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.CreateSystemQuery, con);
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = sys.SystemName;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }

        public void UpdateSystem(Systems sys)
        {
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.UpdateSystemQuery, con);
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = sys.SystemName;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = sys.SystemId;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }

        public void DeleteSystem(int id)
        {
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.DeleteSystemQuery, con);
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }
    }
}
