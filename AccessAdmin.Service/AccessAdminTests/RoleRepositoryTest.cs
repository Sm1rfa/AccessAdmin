using System.Collections.Generic;
using AccessAdmin.Business.Interfaces;
using AccessAdmin.Business.Repositories;
using AccessAdmin.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AccessAdminTests
{
    [TestClass]
    public class RoleRepositoryTest
    {
        [TestMethod]
        public void Will_get_all_roles()
        {
            var list = new List<Roles>
            {
                new Roles{ RoleId = 1, RoleName = "Read", IsDeleted = false},
                new Roles{ RoleId = 2, RoleName = "Read/Write", IsDeleted = false}
            };

            var mock = new Mock<IRoleRepository>();
            mock.Setup(r => r.GetAllRoles()).Returns(list);

            IRoleRepository rr = mock.Object;
            var result = rr.GetAllRoles();

            Assert.AreNotEqual(0, result.Count);
        }
    }
}
