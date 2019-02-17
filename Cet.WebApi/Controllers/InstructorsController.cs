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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _service;
        private readonly IConfiguration _configuration;

        public InstructorsController(IInstructorService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpGet("trial")]
        public IActionResult Trial()
        {
            return Ok("sa");
        }

        [HttpPost("register")]
        public IActionResult Register([FromForm]InstructorRegisterDto instructor)
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

            var createdTeacher = _service.Register(instructorToCreate, instructor.Password);
            // 201: Created Status
            return StatusCode(201, createdTeacher);
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginDto userDto)
        {
            var user = _service.Login(userDto.UserName, userDto.Password);

            if (user == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.User.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.User.UserName)
                }),

                Expires = DateTime.Now.AddDays(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }
    }
}