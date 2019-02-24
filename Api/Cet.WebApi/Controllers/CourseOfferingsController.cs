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
    public class CourseOfferingsController : ControllerBase
    {
        private readonly ICourseOfferingService _service;

        public CourseOfferingsController(ICourseOfferingService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CourseOffering courseOffering)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(courseOffering);
            return Ok(courseOffering);
        }

        [HttpGet]
        // [Authorize(Roles = Role.Admin+", "+Role.Instructor)]
        public IActionResult GetAll()
        {
            var courseOfferings = _service.GetList();
            return Ok(courseOfferings);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var courseOffering = _service.Get(c => c.Id == id);
            return Ok(courseOffering);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody]CourseOffering courseOffering, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            courseOffering.Id = id;
            _service.Update(courseOffering);
            return Ok(courseOffering);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var courseOffering = _service.Get(d => d.Id == id);
            _service.Delete(courseOffering);

            return Ok();
        }
    }
}