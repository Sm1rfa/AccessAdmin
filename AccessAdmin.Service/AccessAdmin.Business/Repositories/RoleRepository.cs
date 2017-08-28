using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AccessAdmin.Business.Interfaces;
using AccessAdmin.Domain.Constants;
using AccessAdmin.Domain.Model;
using NLog;

namespace AccessAdmin.Business.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public List<Roles> GetAllRoles()
        {
            var list = new List<Roles>();
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.GetAllRoles, con);

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new Roles
                            {
                                RoleId = (int)reader[0],
                                RoleName = reader[1].ToString(),
                                IsDeleted = (bool)reader[2]
                            });
                        }
                    }

                    reader.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in GetAllRoles - {e}");
            }

            return list;
        }

        public List<Roles> GetAllPerSystem(int sysId)
        {
            var list = new List<Roles>();
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.GetAllRolesPerSystemQuery, con);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = sysId;

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new Roles
                            {
                                RoleId = (int)reader[0],
                                RoleName = reader[1].ToString(),
                                IsDeleted = (bool)reader[2]
                            });
                        }
                    }

                    reader.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in GetAllRoles - {e}");
            }

            return list;
        }

        public Roles GetSingleRolePerSystem(int sysId, int roleId)
        {
            var role = new Roles();
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.GetSingleRolePerSystemQuery, con);
                    cmd.Parameters.Add("@sysId", SqlDbType.Int).Value = sysId;
                    cmd.Parameters.Add("@roleId", SqlDbType.Int).Value = roleId;

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            role.RoleId = (int)reader[0];
                            role.RoleName = reader[1].ToString();
                            role.IsDeleted = (bool)reader[2];
                        }
                    }

                    reader.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in GetSingleRolePerSystem - {e}");
            }

            return role;
        }

        public void CreateRole(Roles role)
        {
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.CreateRoleQuery, con);
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = role.RoleName;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in CreateRole - {e}");
            }
        }

        public void AssignRoleToSystem(int sysId, int roleId)
        {
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.AssignRoleToSystemQuery, con);
                    cmd.Parameters.Add("@sysId", SqlDbType.Int).Value = sysId;
                    cmd.Parameters.Add("@roleId", SqlDbType.Int).Value = roleId;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in AssignRoleToSystem - {e}");
            }
        }

        public void UpdateRolePerSystem(int sysId, int roleId, int roleId2)
        {
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.UpdateRolePerSystemQuery, con);
                    cmd.Parameters.Add("@sysId", SqlDbType.Int).Value = sysId;
                    cmd.Parameters.Add("@roleId", SqlDbType.Int).Value = roleId;
                    cmd.Parameters.Add("@roleId2", SqlDbType.Int).Value = roleId2;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in UpdateRolePerSystem - {e}");
            }
        }

        public void DeleteRolePerSystem(int sysId, int roleId)
        {
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.DeleteRolePerSystemQuery, con);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = sysId;
                    cmd.Parameters.Add("@roleId", SqlDbType.Int).Value = roleId;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in DeleteRolePerSystem - {e}");
            }
        }
    }
}
