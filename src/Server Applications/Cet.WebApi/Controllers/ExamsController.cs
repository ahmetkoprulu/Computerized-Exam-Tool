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
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _service;
        private readonly IMapper _mapper;

        public ExamsController(IExamService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}/questions")]
        public IActionResult GetQuestions(int id)
        {
            var exam = _service.GetIncludedSingle(
                filter: e => e.Id == id,
                properties: e => e.Questions);

            if (exam == null)
                return NotFound();

            return Ok(exam);
        }

        [HttpGet("{id}/course")]
        public IActionResult GetCourse(int id)
        {
            var exam = _service.GetIncludedSingle(
                filter: e => e.Id == id, 
                properties: e => e.CourseOffering);

            if (exam == null)
                return NotFound();

            return Ok(exam);
        }

        [HttpGet("{id}/active")]
        public IActionResult GetActiveExams(int id)
        {
            return Ok(_service.GetActiveExams(id));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]Exam exam)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.Add(exam);
            return Ok(exam);
        }

        [HttpPost]
        public IActionResult Insert([FromBody]Exam exam)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_service.AddExam(exam));
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

        [HttpPost("{id}")]
        public IActionResult Update([FromBody]ExamDto examDto, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var exam = _mapper.Map<Exam>(examDto);
            exam.Id = id;
            _service.Update(exam);
            return Ok(exam);
        }

        [HttpGet("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var exam = _service.Get(d => d.Id == id);
            _service.Delete(exam);

            return Ok();
        }
    }
}