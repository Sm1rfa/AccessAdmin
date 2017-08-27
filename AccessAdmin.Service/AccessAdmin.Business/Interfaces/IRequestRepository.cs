using System.Collections.Generic;
using AccessAdmin.Domain.Model;

namespace AccessAdmin.Business.Interfaces
{
    public interface IRequestRepository
    {
        List<Requests> GetAllPendingRequests();
        void PublishRequest(Requests request);
        void SolveRequest(Requests request);
    }
}
