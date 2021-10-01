using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net5.JWT.Models;
using Net5.JWT.Models.GetDataDto;
using Net5.JWT.Repositories;
using Net5.JWT.Services;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Net5.JWT.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalController : Controller
    {
        private readonly IRepository Repository;
        private readonly IWebHostEnvironment hostEnvironment;

        public AnimalController(IRepository repository, IWebHostEnvironment hostEnvironment)
        {
            Repository = repository;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpPost]     
        [Authorize(Roles = "1,2")]
        public IActionResult Post_Animal(AnimalDto dto)
        {

            if (dto._extraStuff != null) 
            {
                dto._extraStuff = null;
                return Conflict(dto);
            }

            Found_Animal animal = new Found_Animal
            {
                UserRefId = dto.UserId,
                Title = dto.Title,
                Address = dto.Address,
                Description = dto.Description,
                ImagePath = dto.ImageUrl,
                ShelterRefId = dto.ShelterId == -1 ? null : dto.ShelterId
            };
            if (!Repository.AddAnimal(animal))
            {
                return StatusCode(400, $"This userId = '{dto.UserId}' is not exist");
            }
            
            return Created("success", dto);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get_Animals()
        {
            var animals = Repository.AllAnimals();
            return Ok(animals);
        }

        [HttpGet("{id}/byshelter")]
        [AllowAnonymous]
        public IActionResult Get_Animals_By_Shelter(int id)
        {

            var animals = Repository.GetAnimalsByShelter(id);
            if (animals.Count == 0)
            {
                return NotFound();
            }
            return Ok(animals);
        }

        [HttpGet("byuser")]
        [Authorize(Roles = "1,2")]
        public IActionResult Get_Animals_ByUser()
        {
            var animals = Repository.GetAnimalsByUser(UserId());
            if (animals.Count == 0)
            {
                return NotFound();
            }
            return Ok(animals);
        }

        [HttpGet("athome")]
        [AllowAnonymous]
        public IActionResult Get_Animals_At_Home()
        {
            var animals = Repository.GetAnimalsAtHome();
            if (animals.Count == 0)
            {
                return NotFound();
            }
            return Ok(animals);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get_Animal(int id)
        {
            var animal = Repository.GetAnimal(id);
            if (animal == null)
            {
                return NotFound();
            }
            return Ok(animal);
        }

        [HttpPut]
        [Authorize(Roles = "1,2")]
        public IActionResult Put_Animal(EditAnimalDto dto)
        {
            if (dto._extraStuff != null)
            {
                dto._extraStuff = null;
                return Conflict(dto);
            }

            int user = UserId();
            if (Repository.GetById(user).RoleRefId == 2 || Repository.CheckFound(UserId(), dto.Id))
            {
                ;
                if (!Repository.EditAnimal(dto))
                {
                    return NotFound(new { error = $"Shelter with id = '{dto.Id}' does not exist" });
                }
                return Ok();
            }
            return StatusCode(403, "User access denied");
        }

        [HttpGet("byuser/{id}")]
        [Authorize(Roles = "1,2")]
        public IActionResult Get_Animal_byuser(int id)
        {
            int user = UserId();
            if (Repository.GetById(user).RoleRefId == 2 || Repository.CheckFound(user, id)) {
                var animal = Repository.GetAnimal(id);
                if(animal == null) 
                {
                    return NotFound();
                }
                return Ok(animal);
            }
            return StatusCode(403, "User access denied");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1,2")]
        public IActionResult Delete_Animal(int id)
        {
            int user = UserId();
            if (Repository.GetById(user).RoleRefId == 2 || Repository.CheckFound(UserId(), id))
            {
                if (!Repository.DeleteAnimal(id))
                {
                    return NotFound();
                }
                return Ok();
            }
            return StatusCode(403, "User access denied");
        }

        [HttpDelete]
        [Authorize(Roles = "1,2")]
        public IActionResult Delete()
        {
            return StatusCode(405);
        }

        public int UserId()
        {
            var jwt = Request.Cookies["jwt"];
            var token = TokenService.Verify(jwt);

            IEnumerator enumerator = token.Claims.GetEnumerator();
            enumerator.MoveNext();
            Claim cl = (Claim)enumerator.Current;
            return int.Parse(cl.Value);
        }
    }
}
