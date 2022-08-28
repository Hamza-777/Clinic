using ClinicBack;

namespace ClinicTest
{
    [TestClass]
    public class UnitTest1
    {
        ClinicServer server = new ClinicServer();

        [TestMethod]
        public void LoginTestSuccess()
        {
            bool loginSuccess = server.LoginUser("kevin123", "kevin@123");

            Assert.IsTrue(loginSuccess);
        }

        [TestMethod]
        public void LoginTestFail()
        {
            bool loginFail = server.LoginUser("kevin123", "kevin123");

            Assert.IsFalse(loginFail);
        }

        [TestMethod]
        public void AddPatientSuccess()
        {
            bool addPatientSuccess = server.RegisterNewPatient("Hrishi", "Dude", "M", 21, Convert.ToDateTime("08/11/2000"));

            Assert.IsTrue(addPatientSuccess);
        }
    }
}