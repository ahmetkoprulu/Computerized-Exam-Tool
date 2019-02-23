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
    public class StudentCourseOfferingsController : ControllerBase
    {
        private readonly IStudentCourseOfferingService _service;

        public StudentCourseOfferingsController(IStudentCourseOfferingService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody]StudentCourseOffering studentCourseOffering)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(studentCourseOffering);
            return Ok(studentCourseOffering);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var studentCourseOfferings = _service.GetList();
            return Ok(studentCourseOfferings);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var studentCourseOffering = _service.Get(d => d.Id == id);
            return Ok(studentCourseOffering);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody]StudentCourseOffering studentCourseOffering, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            studentCourseOffering.Id = id;
            _service.Update(studentCourseOffering);
            return Ok(studentCourseOffering);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var studentCourseOffering = _service.Get(d => d.Id == id);
            _service.Delete(studentCourseOffering);

            return Ok();
        }
    }
}