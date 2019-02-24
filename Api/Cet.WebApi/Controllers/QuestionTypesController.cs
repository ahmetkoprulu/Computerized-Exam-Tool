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
    public class QuestionTypesController : ControllerBase
    {
        private readonly IQuestionTypeService _service;

        public QuestionTypesController(IQuestionTypeService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody]QuestionType questionType)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(questionType);
            return Ok(questionType);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var questionTypes = _service.GetList();
            return Ok(questionTypes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var questionType = _service.Get(d => d.Id == id);
            return Ok(questionType);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody]QuestionType questionType, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            questionType.Id = id;
            _service.Update(questionType);
            return Ok(questionType);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var questionType = _service.Get(d => d.Id == id);
            _service.Delete(questionType);

            return Ok();
        }
    }
}