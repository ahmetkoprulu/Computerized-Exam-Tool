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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromForm]Course course)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(course);
            return Ok(course);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var departments = _service.GetList();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var course = _service.Get(c => c.Id == id);
            return Ok(course);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromForm]Course course, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            course.Id = id;
            _service.Update(course);
            return Ok(course);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var department = _service.Get(d => d.Id == id);
            _service.Delete(department);

            return Ok();
        }
    }
}