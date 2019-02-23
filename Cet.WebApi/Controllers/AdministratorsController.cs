using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
    public class AdministratorsController : ControllerBase
    {
        private readonly IAdministratorService _service;
        private readonly IConfiguration _configuration;

        public AdministratorsController(IAdministratorService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }
    
        [HttpPut("{id}")]
        public IActionResult Update([FromBody]Administrator admin, int id)
        {
            admin.Id = id;
            _service.Update(admin);

            return Ok(admin);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var admin = _service.GetIncludedSingle(a => a.Id == id);
            _service.Delete(admin);

            return StatusCode(204);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]AdministratorRegisterDto admin)
        {
            if (_service.IsUserExist(admin.UserName))
                ModelState.AddModelError("UserName", "Username already taken");

            if (!ModelState.IsValid)
                return BadRequest();

            var adminToCreate = new Administrator()
            {
                User = new User()
                {
                    Name = admin.Name,
                    Surname = admin.Surname,
                    UserName = admin.UserName,
                    Email = admin.Email
                }
            };

            var createdTeacher = _service.Register(adminToCreate, admin.Password);
            // 201: Created Status
            return StatusCode(201, createdTeacher);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginDto userDto)
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
                    new Claim(ClaimTypes.Name, user.User.UserName),
                    new Claim(ClaimTypes.Role, Role.Admin)
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