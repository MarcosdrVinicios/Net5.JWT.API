using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net5.JWT.Models;
using Net5.JWT.Models.GetDataDto;
using Net5.JWT.Repositories;
using Net5.JWT.Services;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Net5.JWT.Controllers
{
    [Route("api/shelters")]
    [ApiController]
    public class ShelterController : Controller
    {
        private readonly IRepository Repository;
        private readonly IWebHostEnvironment hostEnvironment;

        public ShelterController(IRepository repository, IWebHostEnvironment hostEnvironment)
        {
            Repository = repository;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        [Authorize(Roles = "2")]
        public IActionResult Post_Shelter(Shelter shelter)
        {
            if (shelter._extraStuff != null)
            {
                shelter._extraStuff = null;
                return Conflict(shelter);
            }

            Repository.AddShelter(shelter);
            return Ok(shelter);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get_Shelters()
        {
            var shelters = Repository.AllShelters();
            return Ok(shelters);
        }

        [HttpGet("names")]
        [AllowAnonymous]
        public IActionResult Get_Shelters_Names()
        {
            var shelters = Repository.GetSheltersNameAndId();
            return Ok(shelters);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get_Shelter(int id)
        {
            var shelter = Repository.GetShelter(id);
            if (shelter == null) 
            {
                return NotFound();
            }
            return Ok(shelter);
        }

        [HttpPut]
        [Authorize(Roles = "2")]
        public IActionResult Put_Shelter(Shelter shelter)
        {
            if (shelter._extraStuff != null)
            {
                shelter._extraStuff = null;
                return Conflict(shelter);
            }

            if (!Repository.EditShelter(shelter))
            {
                return NotFound(new {error = $"Shelter with id = '{shelter.Id}' does not exist"});
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public IActionResult Delete_Shelter(int id)
        {
            if (!Repository.DeleteShelter(id))
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
