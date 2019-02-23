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
    public class ExamStatusesController : ControllerBase
    {
        private readonly IExamStatusService _service;

        public ExamStatusesController(IExamStatusService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody]ExamStatus examStatus)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(examStatus);
            return Ok(examStatus);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var examStatuses = _service.GetList();
            return Ok(examStatuses);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var examStatus = _service.Get(d => d.Id == id);
            return Ok(examStatus);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody]ExamStatus examStatus, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            examStatus.Id = id;
            _service.Update(examStatus);
            return Ok(examStatus);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var examStatus = _service.Get(d => d.Id == id);
            _service.Delete(examStatus);

            return Ok();
        }
    }
}