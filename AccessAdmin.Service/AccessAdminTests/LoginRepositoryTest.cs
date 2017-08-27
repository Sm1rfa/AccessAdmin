using AccessAdmin.Business.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AccessAdminTests
{
    [TestClass]
    public class LoginRepositoryTest
    {
        [TestMethod]
        public void Will_not_authenticate()
        {
            var email = "harvy@network.org";
            var pass = "harv88burnstein";

            var mock = new Mock<ILoginRepository>();
            mock.Setup(r => r.AuthenticateUser(email, pass)).Returns(0);

            ILoginRepository rr = mock.Object;
            var result = rr.AuthenticateUser(email, pass);

            Assert.AreNotEqual(1, result);
        }

        [TestMethod]
        public void Will_authenticate_user()
        {
            var email = "stoyan@varna.net";
            var pass = "1234";

            var mock = new Mock<ILoginRepository>();
            mock.Setup(r => r.AuthenticateUser(email, pass)).Returns(1);

            ILoginRepository rr = mock.Object;
            var result = rr.AuthenticateUser(email, pass);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Will_check_if_user_is_admin()
        {
            var email = "stoyan@varna.net";
            var email2 = "pep@cph.net";

            var mock = new Mock<ILoginRepository>();
            mock.Setup(r => r.IsUserAdmin(email)).Returns(true);
            mock.Setup(r => r.IsUserAdmin(email2)).Returns(false);

            ILoginRepository rr = mock.Object;
            var result = rr.IsUserAdmin(email);
            var result2 = rr.IsUserAdmin(email2);

            Assert.IsTrue(result);
            Assert.AreNotEqual(result, result2);
        }
    }
}
