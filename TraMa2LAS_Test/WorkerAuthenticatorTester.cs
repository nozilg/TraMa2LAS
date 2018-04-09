using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cizeta.TraMa2LAS;

namespace TraMa2LAS_Test
{
    [TestClass]
    public class WorkerAuthenticatorTester
    {
        [TestMethod]
        public void CheckWorkerAuthenticationByConnectionString()
        {
            string stationName = "ST10";
            string badgeCode = "12345";
            string connectionString = @"Data Source=.\SQLEXPRESS12;Initial Catalog=TraMa4_Test;Integrated Security=True";
            WorkerAuthenticator wa = new WorkerAuthenticator(connectionString);
            WorkerLoginResult ret = wa.LoginByBadgeCode(badgeCode, stationName);
            WorkerLoginResult expected = WorkerLoginResult.Ok;
            WorkerLoginResult actual = ret;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckWorkerAuthenticationByConfigFile()
        {
            string stationName = "ST10";
            string badgeCode = "gothamcity";
            WorkerAuthenticator wa = new WorkerAuthenticator();
            WorkerLoginResult ret = wa.LoginByBadgeCode(badgeCode, stationName);
            WorkerLoginResult expected = WorkerLoginResult.Ok;
            WorkerLoginResult actual = ret;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckWorkerAuthenticationWithWorkerName()
        {
            string stationName = "ST10";
            string badgeCode = "gothamcity";
            WorkerAuthenticator wa = new WorkerAuthenticator();
            WorkerLoginResult ret = wa.LoginByBadgeCode(badgeCode, stationName);
            if (ret == WorkerLoginResult.Ok)
            {
                string expected = "Batman";
                string actual = wa.CurrentWorker.Name;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.Fail("Authentication failed");
            }
        }
    }
}
