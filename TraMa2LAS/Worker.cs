using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cizeta.TraMa2LAS
{
    public class Worker
    {
        public string Name;
        public string LoginName;
        public string RoleName;
        public string Code;
        public int AccessLevel;
        public Dictionary<string, bool> StationsAccess;

        public Worker()
        {
            this.Name = string.Empty;
            this.LoginName = string.Empty;
            this.RoleName = string.Empty;
            this.Code = string.Empty;
            this.AccessLevel = 0;
            this.StationsAccess = new Dictionary<string, bool>();
        }

        public Worker(string workerName, 
            string workerLoginName, 
            string workerRoleName, 
            string workerCode, 
            int workerAccessLevel)
        {
            this.Name = workerName;
            this.LoginName = workerLoginName;
            this.RoleName = workerRoleName;
            this.Code = workerCode;
            this.AccessLevel = workerAccessLevel;
            this.StationsAccess = new Dictionary<string, bool>();
        }

        public bool IsEnabledOnStation(string stationName)
        {
            return (StationsAccess[stationName]);
        }

    }
}
