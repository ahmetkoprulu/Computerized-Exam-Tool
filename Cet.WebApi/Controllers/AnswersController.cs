using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cet.BusinessLogic.Abstract;
using Cet.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerService _service;

        public AnswersController(IAnswerService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromForm]Answer answer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(answer);
            return Ok(answer);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var answers = _service.GetList();
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var answer = _service.Get(a => a.Id == id);
            return Ok(answer);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromForm]Answer answer, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            answer.Id = id;
            _service.Update(answer);
            return Ok(answer);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var answer = _service.Get(a => a.Id == id);
            _service.Delete(answer);

            return Ok();
        }
    }
}