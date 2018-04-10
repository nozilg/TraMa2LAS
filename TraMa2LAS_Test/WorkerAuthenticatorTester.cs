using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cizeta.TraMa2LAS;
using Cizeta.TraMaAuth;

namespace TraMa2LAS_Test
{
    [TestClass]
    public class WorkerAuthenticatorTester
    {

        [TestMethod]
        public void CheckWorkerAuthenticationByConfigFile()
        {
            string stationName = "ST10";
            string badgeCode = "gothamcity";
            Authenticator a = new Authenticator(AuthenticationMode.BadgeCode);
            LoginResult ret = a.LoginByBadgeCode(badgeCode, stationName);
            LoginResult expected = LoginResult.Ok;
            LoginResult actual = ret;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckWorkerAuthenticationByConnectionString()
        {
            string stationName = "ST10";
            string badgeCode = "12345";
            string connectionString = @"Data Source=.\SQLEXPRESS12;Initial Catalog=TraMa4_Test;Integrated Security=True";
            Authenticator a = new Authenticator(AuthenticationMode.BadgeCode, connectionString);
            LoginResult ret = a.LoginByBadgeCode(badgeCode, stationName);
            LoginResult expected = LoginResult.Ok;
            LoginResult actual = ret;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckWorkerAuthenticationWithWorkerName()
        {
            string stationName = "ST10";
            string badgeCode = "gothamcity";
            Authenticator a = new Authenticator(AuthenticationMode.BadgeCode);
            LoginResult ret = a.LoginByBadgeCode(badgeCode, stationName);
            if (ret == LoginResult.Ok)
            {
                string expected = "Batman";
                string actual = a.CurrentWorker.Name;
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.Fail("Authentication failed");
            }
        }

    }
}
