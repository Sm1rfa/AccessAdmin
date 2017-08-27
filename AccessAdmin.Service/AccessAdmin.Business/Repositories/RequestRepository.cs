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
    public class RequestRepository : IRequestRepository
    {
        private Logger logger = LogManager.GetCurrentClassLogger();


        public List<Requests> GetAllPendingRequests()
        {
            var requests = new List<Requests>();
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.GetPendingRequests, con);

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            requests.Add(new Requests
                            {
                                RequestId = (int)reader[0],
                                RequestReason = reader[1].ToString(),
                                DecisionReason = reader[2].ToString(),
                                UserId = new User { UserId = (int)reader[3] },
                                SystemId = new Systems{ SystemId = (int)reader[4], SystemName = reader[5].ToString() },
                                RoleId = new Roles{ RoleId = (int)reader[6], RoleName = reader[7].ToString() },
                                RequestStateId = new RequestStates { RequestId  = (int)reader[8], RequestName = reader[9].ToString() }
                            });
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

            return requests;
        }

        public void PublishRequest(Requests request)
        {
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.MakeRequest, con);
                    cmd.Parameters.Add("@reqReason", SqlDbType.NVarChar).Value = request.RequestReason;
                    cmd.Parameters.Add("@userId", SqlDbType.NVarChar).Value = request.UserId.UserId;
                    cmd.Parameters.Add("@sysId", SqlDbType.NVarChar).Value = request.SystemId.SystemId;
                    cmd.Parameters.Add("@roleId", SqlDbType.NVarChar).Value = request.RoleId.RoleId;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in PublishRequest - {e}");
            }
        }

        public void SolveRequest(Requests request)
        {
            try
            {
                using (var con = Connection.GetConnection())
                {
                    var cmd = new SqlCommand(Queries.SolveRequest, con);
                    cmd.Parameters.Add("@reason", SqlDbType.NVarChar).Value = request.DecisionReason;
                    cmd.Parameters.Add("@reqId", SqlDbType.NVarChar).Value = request.RequestId;
                    cmd.Parameters.Add("@stateId", SqlDbType.NVarChar).Value = request.RequestStateId.RequestId;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in SolveRequest - {e}");
            }
        }
    }
}
