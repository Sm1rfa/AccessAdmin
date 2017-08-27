using System.Collections.Generic;
using AccessAdmin.Business.Interfaces;
using AccessAdmin.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AccessAdmin.Service.Controllers
{
    [Route("api/requests")]
    public class RequestController : Controller
    {
        private IRequestRepository repository;

        public RequestController(IRequestRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet]
        public IEnumerable<Requests> GetAllPending()
        {
            return this.repository.GetAllPendingRequests();
        }

        [HttpPost]
        [Route("makerequest")]
        public void MakeReuqest([FromBody]Requests request)
        {
            this.repository.PublishRequest(request);
        }

        [HttpPost]
        [Route("solverequest")]
        public void SolveReuqest([FromBody] Requests request)
        {
            this.repository.SolveRequest(request);
        }
    }
}
