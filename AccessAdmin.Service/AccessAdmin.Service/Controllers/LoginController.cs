using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessAdmin.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccessAdmin.Service.Controllers
{
    [Route("api/login")]
    public class LoginController : Controller
    {
        private readonly ILoginRepository repository;

        public LoginController(ILoginRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet("{email}/{password}")]
        public int GetLogin(string email, string password)
        {
            return this.repository.AuthenticateUser(email, password);
        }

        [HttpGet("{email}")]
        public bool IsUserAdmin(string email)
        {
            return this.repository.IsUserAdmin(email);
        }
    }
}
