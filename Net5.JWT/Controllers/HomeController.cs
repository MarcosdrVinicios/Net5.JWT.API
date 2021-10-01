using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net5.JWT.Models;
using Net5.JWT.Models.GetDataDto;
using Net5.JWT.Repositories;
using Net5.JWT.Services;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Net5.JWT.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRepository Repository;
        private readonly IWebHostEnvironment hostEnvironment;

        public HomeController(IRepository repository, IWebHostEnvironment hostEnvironment)
        {
            Repository = repository;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterDto dto)
        {
            if (dto._extraStuff != null)
            {
                dto._extraStuff = null;
                return Conflict(dto);
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            user.RoleRefId = 0;

            User us = Repository.Create(user);

            if (us == null) {
                return Conflict();
            }

            return Created("success", new { 
                username = us.Username,
                email = us.Email
            });
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login(UserDto model)
        {

            if (model._extraStuff != null)
            {
                model._extraStuff = null;
                return Conflict(model);
            }

            var user = Repository.Get(model.Email);

            if (user == null) {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = TokenService.CreateToken(user);
            user.Password = "";
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new 
            { 
                username = user.Username,    
                jwt = jwt
            });
        }

        [HttpPost("logout")]
        [Authorize(Roles = "0,1,2")]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwt");

            if (Request.Cookies["jwt"] == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "success"
            });
        }


        [HttpGet("user")]
        [AllowAnonymous]
        public IActionResult Current_User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = TokenService.Verify(jwt);

                IEnumerator enumerator = token.Claims.GetEnumerator();
                enumerator.MoveNext();
                Claim cl = (Claim)enumerator.Current;
                int userId = int.Parse(cl.Value);
                var user = Repository.GetById(userId);

                return Ok(new { 
                    username = user.Username,
                    role = user.RoleRefId,
                    id = user.Id,
                    jwt = jwt
                });
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Authorize(Roles = "1,2")]
        public IActionResult Users()
        {
            var users = Repository.GetUsers();
            return Ok(users);
        }

        [HttpGet("roles")]
        [Authorize(Roles = "2")]
        public IActionResult Roles()
        {
            var roles = Repository.GetRoles();
            return Ok(roles);
        }

        [HttpPatch("roles/{id}")]
        [Authorize(Roles = "2")]
        public IActionResult Patch_Role(int id, ChangeRoleDto role)
        {
            if (role._extraStuff != null)
            {
                role._extraStuff = null;
                return Conflict(role);
            }
            if (!Repository.ChangeRole(id, role))
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public IActionResult Delete_User(int id)
        {
            if (!Repository.DeleteUser(id))
            {
                return NotFound();
            }
            
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "1,2")]
        public IActionResult Delete()
        {
            return StatusCode(405);
        }

    }
}
