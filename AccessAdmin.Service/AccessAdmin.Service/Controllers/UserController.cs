using AccessAdmin.Business.Interfaces;
using AccessAdmin.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace AccessAdmin.Service.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private IUserRepository repository;

        public UserController(IUserRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet("{email}")]
        public User GetUserByEmail(string email)
        {
            return this.repository.GetUserByEmail(email);
        }
    }
}
