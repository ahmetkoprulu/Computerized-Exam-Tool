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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _service;

        public QuestionsController(IQuestionService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromForm]Question question)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(question);
            return Ok(question);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var questions = _service.GetList();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var question = _service.Get(q => q.Id == id);
            return Ok(question);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromForm]Question question, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            question.Id = id;
            _service.Update(question);
            return Ok(question);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var question = _service.Get(q => q.Id == id);
            _service.Delete(question);

            return Ok();
        }
    }
}