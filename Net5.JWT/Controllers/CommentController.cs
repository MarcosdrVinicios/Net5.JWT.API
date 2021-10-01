using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net5.JWT.Models;
using Net5.JWT.Models.GetDataDto;
using Net5.JWT.Repositories;
using Net5.JWT.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Net5.JWT.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly IRepository Repository;
        private readonly IWebHostEnvironment hostEnvironment;

        public CommentsController(IRepository repository, IWebHostEnvironment hostEnvironment)
        {
            Repository = repository;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        [Authorize(Roles = "0,1,2")]
        public IActionResult Post_Comment(GetCommentDto dto)
        {
            if (Request.Cookies["jwt"] == null)
            {
                return Forbid();
            }
            if (dto._extraStuff != null)
            {
                dto._extraStuff = null;
                return Conflict(dto);
            }

            var comment = Repository.AddComment(dto, UserId());

            if (comment == null)
            {
                return NotFound($"This animalId = '{dto.AnimalId}' is not exist");
            }
            return Ok(comment);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get_Comments(int id)
        {
            var comments = Repository.AllComments(id);
            if (comments.Count == 0) 
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            return StatusCode(405);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "2")]
        public IActionResult Put_Comment(int id, GetEditCommentDto dto)
        {
            if (dto._extraStuff != null)
            {
                dto._extraStuff = null;
                return Conflict(dto);
            }
            if (!Repository.EditComment(id, dto))
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "1,2")]
        public IActionResult Put()
        {
            return StatusCode(405);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public IActionResult Delete_Comment(int id)
        {
            if (!Repository.DeleteComment(id))
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
