using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _service;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public InstructorsController(IInstructorService service, IConfiguration configuration, IMapper mapper)
        {
            _service = service;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]InstructorRegisterDto instructor)
        {
            if (_service.IsUserExist(instructor.UserName))
                ModelState.AddModelError("UserName", "Username already taken");

            if (!ModelState.IsValid)
                return BadRequest();

            var instructorToCreate = new Instructor()
            {
                DepartmentId = instructor.DepartmentId,
                User = new User()
                {
                    Name = instructor.Name,
                    Surname = instructor.Surname,
                    UserName = instructor.UserName,
                    Email = instructor.Email
                }
            };
            _service.Register(instructorToCreate, instructor.Password);
            // 201: Created Status
            return StatusCode(201);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var instructor = _service.Login(loginDto.UserName, loginDto.Password);

            if (instructor == null)
                return Unauthorized();

//            var instructorDto = new InstructorDto
  //          {
    //            Id = instructor.Id,
      //          Name = instructor.User.Name,
        //        Surname = instructor.User.Surname,
          //      Username = instructor.User.UserName,
            //    Email = instructor.User.Email,
              //  DepartmentName = instructor.Department.Name,
                //Token = CreateToken(instructor)
            //};

            var instructorDto = _mapper.Map<MemberDto>(instructor);
            instructorDto.Role = "instructor";
            instructorDto.Token = CreateToken(instructor);

            return Ok(instructorDto);
        }

        [HttpGet("{id}/courseofferings")]
        public IActionResult GetExams(int id)
        {
            var instructor = _service.GetIncludedSingle(filter: i => i.Id == id, properties: i => i.CourseOfferings);
            var courses = _mapper.Map<CourseOfferingDto>(instructor.CourseOfferings);

            return Ok(courses);
        }

        public string CreateToken(Instructor instructor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, instructor.User.Id.ToString()),
                    new Claim(ClaimTypes.Name, instructor.User.UserName),
                    new Claim(ClaimTypes.Role, Role.Instructor)
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