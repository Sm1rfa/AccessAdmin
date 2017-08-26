using System.Collections.Generic;
using AccessAdmin.Business.Interfaces;
using AccessAdmin.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace AccessAdmin.Service.Controllers
{
    [Route("api/roles")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository repository;

        public RoleController(IRoleRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet]
        public IEnumerable<Roles> GetAll()
        {
            return this.repository.GetAllRoles();
        }

        [HttpGet("{sysId}")]
        public IEnumerable<Roles> GetRoles(int sysId)
        {
            return this.repository.GetAllPerSystem(sysId);
        }

        [HttpGet("{sysId}/{roleId}")]
        public Roles GetRole(int sysId, int roleId)
        {
            return this.repository.GetSingleRolePerSystem(sysId, roleId);
        }

        [HttpPost]
        public void CreateRole([FromBody]Roles role)
        {
            this.repository.CreateRole(role);
        }

        [HttpPost("{sysId}/{roleId}")]
        public void AssignRole(int sysId, int roleId)
        {
            this.repository.AssignRoleToSystem(sysId, roleId);
        }

        [HttpPut("{sysId}/{roleId}/{roleId2}")]
        public void UpdateRole(int sysId, int roleId, int roleId2)
        {
            this.repository.UpdateRolePerSystem(sysId, roleId, roleId2);
        }

        [HttpDelete("{sysId}/{roleId}")]
        public void Delete(int sysId, int roleId)
        {
            this.repository.DeleteRolePerSystem(sysId, roleId);
        }
    }
}
