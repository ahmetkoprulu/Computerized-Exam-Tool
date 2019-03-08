﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockController : ControllerBase
    {
        public List<MockUserDto> Users = new List<MockUserDto>
        {
            new MockUserDto
            {
                Id = 2,
                Nick = "masterofdata",
                Name = "Ensar",
                Surname = "Köprülü",
                Mail = "2",
                Role = "student",
                Dept = "2",
                Uni = "istanbul_sehir_university",
                Token = "2"
            },
            new MockUserDto
            {
                Id = 2,
                Nick = "masterofdata",
                Name = "Ali",
                Surname = "Cakmak",
                Mail = "2",
                Role = "instructor",
                Dept = "2",
                Uni = "istanbul_sehir_university",
                Token = "2"
            }
        };

        [HttpGet("login")]
        public IActionResult Login()
        {
            MockUserDto user = new MockUserDto()
            {
                Id = 2,
                Nick = "masterofdata",
                Name = "Ensar",
                Surname = "Köprülü",
                Mail = "2",
                Role = "student",
                Dept = "2",
                Uni = "istanbul_sehir_university",
                Token = "2"
            };

            return Ok(user);
        }

        [HttpGet("login/instructor")]
        public IActionResult LoginInstructor()
        {
            MockUserDto user = new MockUserDto()
            {
                Id = 2,
                Nick = "masterofdata",
                Name = "Ali",
                Surname = "Cakmak",
                Mail = "2",
                Role = "instructor",
                Dept = "2",
                Uni = "istanbul_sehir_university",
                Token = "2"
            };

            return Ok(user);
        }

        [HttpGet("course/{id}")]
        public IActionResult GetLecturerCourse(string id)
        {
            var courses = new MockCoursesDto
            {
                Courses = new List<List<string>>
                {
                    new List<string>
                    {
                        "CS 350",
                        "Database Systems",
                    },
                    new List<string>
                    {
                        "CS 360",
                        "Computer Systems"
                    },
                    new List<string>
                    {
                        "CS 330",
                        "Operating Systems"
                    },
                    new List<string>
                    {
                        "Uni 124",
                        "Textual Analysis II"
                    }
                }
            };

            return Ok(courses);
        }

        [HttpGet("")]
        public IActionResult CheckConnection()
        {
            return Ok();
        }
    }

    public class MockUserDto
    {
        public string Nick { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Id { get; set; }
        public string Role { get; set; }
        public string Mail { get; set; }
        public string Dept { get; set; }
        public string Uni { get; set; }
        public string Token { get; set; }
    }

    public class MockCoursesDto
    {
        public List<List<string>> Courses { get; set; }
    }
}