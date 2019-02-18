using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cet.BusinessLogic.Abstract;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;
using Cet.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = Role.Admin+", "+Role.Instructor)]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet("{id}/courses")]
        public IActionResult GetCourses(int id)
        {
            var department = _service.GetIncludedSingle(
                filter: d => d.Id == id, 
                properties: d => d.Courses);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [HttpGet("{id}/instructors")]
        public IActionResult GetInstructors(int id)
        {
            var department = _service.GetIncludedSingle(
                filter: d => d.Id == id,
                properties: d => d.Instructors);

            if (department == null)
                return NotFound();

            return Ok(new JsonResult(department).Value);
        }

        [HttpGet("{id}/students")]
        public IActionResult GetStudents(int id)
        {
            var department = _service.GetIncludedSingle(
                filter: d => d.Id == id, 
                properties: d => d.Students);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [HttpGet("{id}/members")]
        public IActionResult GetMembers(int id)
        {
            var department = _service.GetIncludedSingle(
                d => d.Id == id, 
                d => d.Students, d => d.Instructors);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [HttpPost]
        public IActionResult Create([FromForm]Department department)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(department);
            return Ok(department);
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

            var department = _service.Get(d => d.Id == id);
            return Ok(department);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromForm]Department department, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            department.Id = id;
            _service.Update(department);
            return Ok(department);
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