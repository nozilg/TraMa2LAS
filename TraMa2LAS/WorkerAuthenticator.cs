using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cizeta.TraMa2LAS
{
    public class WorkerAuthenticator
    {
        public Worker CurrentWorker;
        public string ExceptionMessage;
        private string ConnectionString;

        public WorkerAuthenticator()
        {
            this.ConnectionString = GetConnectionString();
            this.CurrentWorker = new Worker();
            this.ExceptionMessage = string.Empty;
        }

        public WorkerAuthenticator(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.CurrentWorker = new Worker();
            this.ExceptionMessage = string.Empty;
        }

        private string GetConnectionString()
        {
            ConfigurationHandler ch = new ConfigurationHandler();
            return ch.GetConnectionString();
        }

        //public WorkerLoginResult Login(string loginName, string password, string badgeCode, string stationName)
        //{
        //    WorkerLoginResult ret = WorkerLoginResult.Failed;
        //    return (ret);
        //}

        public WorkerLoginResult LoginByBadgeCode(string workerBadgeCode, string stationName)
        {
            WorkerLoginResult ret;
            try
            {
                CurrentWorker = GetWorker(workerBadgeCode);
                if (CurrentWorker != null)
                {
                    CurrentWorker.StationsAccess = GetStationsAccess(CurrentWorker.LoginName);
                    if(CurrentWorker.IsEnabledOnStation(stationName))
                    {
                        ret = WorkerLoginResult.Ok;
                    }
                    else
                    {
                        ret = WorkerLoginResult.NotEnabled;
                    }
                }
                else
                {
                    ret = WorkerLoginResult.Failed;
                } 
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
                ret = WorkerLoginResult.Failed;
            }
            return (ret);
        }

        private Worker GetWorker(string workerBadgeCode)
        {
            Worker w = new Worker();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetWorkerByBadgeCodeForLAS";
                    SqlParameter sqlParamBadgeCode = new SqlParameter();
                    sqlParamBadgeCode = command.Parameters.Add("@BadgeCode", SqlDbType.NVarChar);
                    sqlParamBadgeCode.Value = workerBadgeCode;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        w = new Worker(
                            reader["Name"].ToString(),
                            reader["LoginName"].ToString(),
                            reader["RoleName"].ToString(),
                            reader["Code"].ToString(),
                            (int)reader["AccessLevel"]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return w;
        }

        private Dictionary<string, bool> GetStationsAccess(string workerLoginName)
        {
            Dictionary<string, bool> d = new Dictionary<string, bool>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetWorkerStationsByLoginNameForLAS";
                    SqlParameter sqlParamBadgeCode = new SqlParameter();
                    sqlParamBadgeCode = command.Parameters.Add("@LoginName", SqlDbType.NVarChar);
                    sqlParamBadgeCode.Value = workerLoginName;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        d.Add(
                            reader["StationName"].ToString(), 
                            (bool)reader["Enabled"]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (d);
        }
    }

    //public enum AuthenticationMode
    //{
    //    Any = 0,
    //    UserPassword = 1,
    //    BadgeCode = 2,
    //}

    public enum WorkerLoginResult
    {
        Ok,
        Failed,
        NotEnabled,
    }

}
