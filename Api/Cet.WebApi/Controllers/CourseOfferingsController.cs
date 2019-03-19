using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cet.BusinessLogic.Abstract;
using Cet.Entities.Concrete;
using Cet.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cet.WebApi.Controllers
{
    [Route("api/v0/[controller]")]
    [ApiController]
    public class CourseOfferingsController : ControllerBase
    {
        private readonly ICourseOfferingService _service;
        private readonly IMapper _mapper;

        public CourseOfferingsController(ICourseOfferingService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CourseOffering courseOffering)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(courseOffering);
            return Ok(courseOffering);
        }

        [HttpGet("{id}/exams")]
        public IActionResult GetExams(int id)
        {
            var courseOffering = _service.GetIncludedSingle(
                filter: co => co.Id == id,
                properties: co => co.Exams);

            var exams = _mapper.Map<List<ExamDto>>(courseOffering.Exams);

            return Ok(exams);
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

        [HttpPost("{id}")]
        public IActionResult Update([FromBody]CourseOffering courseOffering, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            courseOffering.Id = id;
            _service.Update(courseOffering);
            return Ok(courseOffering);
        }

        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var courseOffering = _service.Get(d => d.Id == id);
            _service.Delete(courseOffering);

            return Ok();
        }
    }
}