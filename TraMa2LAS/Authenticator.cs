using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cizeta.TraMaAuth;

namespace Cizeta.TraMa2LAS
{
    public class Authenticator
    {

        #region Properties

        public Worker CurrentWorker
        {
            get { return (Auth.CurrentWorker); }
        }

        #endregion

        #region Public members

        #endregion

        #region Private members

        private WorkerAuthenticator Auth;
        private WorkerLoginResult Result;

        #endregion

        #region Constructors

        public Authenticator(AuthenticationMode authMode)
        {
            this.Auth = new WorkerAuthenticator(authMode, this.GetConnectionString());
            this.Result = WorkerLoginResult.Failed;
        }

        public Authenticator(AuthenticationMode authMode, string connectionString)
        {
            this.Auth = new WorkerAuthenticator(authMode, connectionString);
            this.Result = WorkerLoginResult.Failed;
        }

        #endregion

        #region Public methods

        public LoginResult LoginByBadgeCode(string workerBadgeCode, string stationName)
        {
            try
            {
                Result = Auth.Login(string.Empty, string.Empty, workerBadgeCode, stationName);
                if (!(string.IsNullOrEmpty(Auth.ExceptionMessage)))
                {
                    throw new Exception(string.Format("Authenticator exception: {0}", Auth.ExceptionMessage));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}{1}{2}", ex.Message, Environment.NewLine, Auth.ExceptionMessage));
            }
            return (GetLoginResult(Result));
        }

        public LoginResult LoginByPassword(string workerLoginName, string workerPassword, string stationName)
        {
            try
            {
                Result = Auth.Login(workerLoginName, workerPassword, string.Empty, stationName);
                if (!(string.IsNullOrEmpty(Auth.ExceptionMessage)))
                {
                    throw new Exception(string.Format("Authenticator exception: {0}", Auth.ExceptionMessage));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}{1}{2}", ex.Message, Environment.NewLine, Auth.ExceptionMessage));
            }
            return (GetLoginResult(Result));
        }

        public void Logout(string stationName)
        {
            Auth = new WorkerAuthenticator();
            Auth.Logout(stationName);
        }

        #endregion

        #region Private methods

        private string GetConnectionString()
        {
            ConfigurationHandler ch = new ConfigurationHandler();
            return ch.GetConnectionString();
        }

        private LoginResult GetLoginResult(WorkerLoginResult res)
        {
            switch (res)
            {
                case WorkerLoginResult.Ok:
                    return (LoginResult.Ok);
                case WorkerLoginResult.Failed:
                    return (LoginResult.Failed);
                case WorkerLoginResult.NotEnabled:
                    return (LoginResult.NotEnabled);
                default:
                    return (LoginResult.Failed);
            }
        }

        #endregion

    }

}
