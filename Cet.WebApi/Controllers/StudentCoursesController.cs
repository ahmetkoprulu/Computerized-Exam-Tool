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
    public class StudentCoursesController : ControllerBase
    {
        private readonly IStudentCourseService _service;

        public StudentCoursesController(IStudentCourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var studentCourses = _service.GetList();
            return Ok(studentCourses);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var studentCourse = _service.Get(s => s.Id == id);

            if (studentCourse == null)
                return NotFound();

            return Ok(studentCourse);
        }

        [HttpPost]
        public IActionResult Create([FromForm]StudentCourse studentCourse)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(studentCourse);
            return Ok(studentCourse);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromForm]StudentCourse studentCourse, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            studentCourse.Id = id;
            _service.Update(studentCourse);

            return Ok(studentCourse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var studentCourse = _service.Get(d => d.Id == id);
            _service.Delete(studentCourse);

            return Ok();
        }
    }
}