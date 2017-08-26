using System.Collections.Generic;
using AccessAdmin.Domain.Model;

namespace AccessAdmin.Business.Interfaces
{
    public interface IRoleRepository
    {
        List<Roles> GetAllRoles();
        List<Roles> GetAllPerSystem(int sysId);
        Roles GetSingleRolePerSystem(int sysId, int roleId);
        void CreateRole(Roles role);
        void AssignRoleToSystem(int sysId, int roleId);
        void UpdateRolePerSystem(int sysId, int roleId, int roleId2);
        void DeleteRolePerSystem(int sysId, int roleId);
    }
}
