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
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _service;

        public ExamsController(IExamService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromForm]Exam exam)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(exam);
            return Ok(exam);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var exams = _service.GetList();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var exam = _service.Get(e => e.Id == id);
            return Ok(exam);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromForm]Exam exam, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            exam.Id = id;
            _service.Update(exam);
            return Ok(exam);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var exam = _service.Get(d => d.Id == id);
            _service.Delete(exam);

            return Ok();
        }
    }
}