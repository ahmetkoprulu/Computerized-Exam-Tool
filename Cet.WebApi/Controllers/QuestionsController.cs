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
    [Route("api/v0/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _service;

        public QuestionsController(IQuestionService service)
        {
            _service = service;
        }

        [HttpGet("{id}/answers")]
        public IActionResult GetAnswers(int id)
        {
            var question = _service.GetIncludedSingle(
                filter: q => q.Id == id,
                properties: q => q.Answers);

            if (question == null)
                return NotFound();

            return Ok(question);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Question question)
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
        public IActionResult Update([FromBody]Question question, int id)
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