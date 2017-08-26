using System.Collections.Generic;
using AccessAdmin.Business.Interfaces;
using AccessAdmin.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace AccessAdmin.Service.Controllers
{
    [Route("api/systems")]
    public class SystemsController : Controller
    {
        private readonly ISystemRepository repository;

        public SystemsController(ISystemRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet]
        public IEnumerable<Systems> Get()
        {
            return this.repository.GetAllSystems();
        }
        
        [HttpGet("{id}")]
        public Systems Get(int id)
        {
            return this.repository.GetSystem(id);
        }
        
        [HttpPost]
        public void CreateSystem([FromBody]Systems sys)
        {
            this.repository.CreateSystem(sys);
        }
        
        [HttpPut("{id}")]
        public void UpdateSystem(int id, [FromBody]Systems sys)
        {
            sys.SystemId = id;
            this.repository.UpdateSystem(sys);
        }
        
        [HttpDelete("{id}")]
        public void DeleteSystem(int id)
        {
            this.repository.DeleteSystem(id);
        }
    }
}
