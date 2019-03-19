using System;
using System.Collections.Generic;
using Cet.BusinessLogic.Concrete;
using Cet.DataAccess.Concrete.EntityFramework;
using Cet.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CetV0DatabaseGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                var adminManager = new AdministratorManager(new AdministratorRepository());
                var studentManager = new StudentManager(new StudentRepository());
                var instructorManager = new InstructorManager(new InstructorRepository());
                var departmentRepository = new DepartmentRepository();
                var courseRepository = new CourseRepository();
                var courseOfferingRepository = new CourseOfferingRepository();
                var studentCourseOfferingRepository = new StudentCourseOfferingRepository();

                var admin = new Administrator
                {
                    User = new User
                    {
                        Name = "Admin",
                        Surname = "Admin",
                        UserName = "admin",
                        Email = "admin@sehir.edu.tr",
                        PhotoUrl = @"https://res.cloudinary.com/ahmetkoprulu/image/upload/v1551362706/ProfilePhotos/mehmetb.png"
                    }
                };
                adminManager.Register(admin, "admin");

                var department = new Department { Name = "Computer Engineering" };
                departmentRepository.Add(department);
                
                var course = new Course
                {
                    Name = "Introduction To Programming II",
                    Code = "ENGR 102",
                    DepartmentId = 1
                };
                var course2 = new Course
                {
                    Name = "Computer Systems",
                    Code = "CS 340",
                    DepartmentId = 1
                };
                courseRepository.Add(course);
                courseRepository.Add(course2);

                var instructor = new Instructor
                {
                    DepartmentId = department.Id,
                    User = new User
                    {
                        Name = "Ali",
                        Surname = "Çakmak",
                        UserName = "masterofdata",
                        Email = "alicakmak@gmail.com",
                        PhotoUrl = @"https://res.cloudinary.com/ahmetkoprulu/image/upload/v1551362706/ProfilePhotos/mehmetb.png"
                    },
                };
                var instructor2 = new Instructor
                {
                    DepartmentId = 1,
                    User = new User
                    {
                        Name = "Ahmet",
                        Surname = "Bulut",
                        UserName = "masterofspark",
                        Email = "ahmetbulut@sehir.edu.tr",
                        PhotoUrl = @"https://res.cloudinary.com/ahmetkoprulu/image/upload/v1551362706/ProfilePhotos/mehmetb.png"
                    }
                };
                instructorManager.Register(instructor, "123456");
                instructorManager.Register(instructor2, "123456");

                var courseOffering = new CourseOffering
                {
                    Semester = "Spring",
                    Year = "2019",
                    IsActive = true,
                    CourseId = course.Id,
                    InstructorId = instructor.Id
                };
                var courseOffering2 = new CourseOffering
                {
                    Semester = "Spring",
                    Year = "2019",
                    IsActive = true,
                    CourseId = course2.Id,
                    InstructorId = instructor2.Id
                };
                courseOfferingRepository.Add(courseOffering);
                courseOfferingRepository.Add(courseOffering2);

                var student = new Student
                {
                    DepartmentId = department.Id,
                    User = new User
                    {
                        Name = "İlayda",
                        Surname = "Turan",
                        UserName = "215241187",
                        Email = "ilaydaturan@std.sehir.edu.tr",
                        PhotoUrl = @"https://res.cloudinary.com/ahmetkoprulu/image/upload/v1551362706/ProfilePhotos/mehmetb.png"
                    }
                };
                studentManager.Register(student, "123456");

                var studentCourseOffering = new StudentCourseOffering
                {
                    StudentId = student.Id,
                    CourseOfferingId = courseOffering.Id
                };
                var studentCourseOffering2 = new StudentCourseOffering
                {
                    StudentId = student.Id,
                    CourseOfferingId = courseOffering2.Id
                };
                studentCourseOfferingRepository.Add(studentCourseOffering);
                studentCourseOfferingRepository.Add(studentCourseOffering2);

                Console.Write("CompExamV0 Database Created And Admin Inserted.");
                Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            Console.ReadKey();
        }
    }
}
