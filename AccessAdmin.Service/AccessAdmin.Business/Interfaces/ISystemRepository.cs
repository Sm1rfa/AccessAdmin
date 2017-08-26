using System.Collections.Generic;
using AccessAdmin.Domain.Model;

namespace AccessAdmin.Business.Interfaces
{
    public interface ISystemRepository
    {
        List<Systems> GetAllSystems();
        Systems GetSystem(int id);
        void CreateSystem(Systems sys);
        void UpdateSystem(Systems sys);
        void DeleteSystem(int id);
    }
}
