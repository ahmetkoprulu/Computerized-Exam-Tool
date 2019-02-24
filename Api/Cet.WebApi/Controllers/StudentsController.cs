using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cet.BusinessLogic.Abstract;
using Cet.Entities.Concrete;
using Cet.WebApi.Dtos;
using Cet.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cet.WebApi.Controllers
{
    [Route("api/v0/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly IConfiguration _configuration;

        public StudentsController(IStudentService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpGet("{id}/exams")]
        public IActionResult GetExams(int id)
        {
            var student = _service.GetStudentWithExams(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpGet("{id}/courses")]
        public IActionResult GetStudentCourses(int id)
        {
            var student = _service.GetStudentWithCourses(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]StudentRegisterDto student)
        {
            if (_service.IsUserExist(student.UserName))
                ModelState.AddModelError("UserName", "Username already taken");

            if (!ModelState.IsValid)
                return BadRequest();

            var studentToCreate = new Student()
            {
                DepartmentId = student.DepartmentId,
                User = new User()
                {
                    Name = student.Name,
                    Surname = student.Surname,
                    UserName = student.UserName,
                    Email = student.Email
                }
            };
            _service.Register(studentToCreate, student.Password);
            // 201: Created Status
            return StatusCode(201);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var student = _service.Login(loginDto.UserName, loginDto.Password);

            if (student == null)
                return Unauthorized();

            var studentDto = new StudentDto
            {
                Id = student.Id,
                Name = student.User.Name,
                Surname = student.User.Surname,
                Username = student.User.UserName,
                Email = student.User.Email,
                DepartmentName = student.Department.Name,
                Token = CreateToken(student)
            };

            return Ok(studentDto);
        }

        public string CreateToken(Student student)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, student.User.Id.ToString()),
                    new Claim(ClaimTypes.Name, student.User.UserName),
                    new Claim(ClaimTypes.Role, Role.Student)
                }),

                Expires = DateTime.Now.AddDays(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}