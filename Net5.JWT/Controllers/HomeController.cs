using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net5.JWT.Models;
using Net5.JWT.Repositories;
using Net5.JWT.Services;
using System;
using System.Threading.Tasks;

namespace Net5.JWT.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "User or password invalid" });

            var token = TokenService.CreateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous()
        {
            return "You are Anonymous";
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Authenticated - {0}", User.Identity.Name);

        [HttpGet]
        [Route("tester")]
        [Authorize(Roles = "tester")]
        public string Tester()
        {
            return "You are a Tester";
        }

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Employee";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Manager";

    }
}
