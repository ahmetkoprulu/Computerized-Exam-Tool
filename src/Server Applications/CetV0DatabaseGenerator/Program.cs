using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Cet.BusinessLogic.Concrete;
using Cet.DataAccess.Concrete.EntityFramework;
using Cet.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CetV0DatabaseGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
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
           */
            /*
             var studentManager = new StudentManager(new StudentRepository());
             var students = new List<Student>();
             students.Add(new Student()
             {
                 User = new User() { Name="İlayda", Surname="Turan", Email="ahmetkoprulu@std.sehir.edu.tr", UserName="123123"},
                 DepartmentId = 1
             });
             students.Add(new Student()
             {
                 User = new User() { Name = "İlayda", Surname = "Turan", Email = "ahmetkoprulu@std.sehir.edu.tr", UserName = "123123" },
                 DepartmentId = 1
             });
             students.Add(new Student()
             {
                 User = new User() { Name = "İlayda", Surname = "Turan", Email = "ahmetkoprulu@std.sehir.edu.tr", UserName = "123123" },
                 DepartmentId = 1
             });
             students.Add(new Student()
             {
                 User = new User() { Name = "İlayda", Surname = "Turan", Email = "ahmetkoprulu@std.sehir.edu.tr", UserName = "123123" },
                 DepartmentId = 1
             });

             foreach (var student in students)
             {   
                 var pass = RandomString(10);
                 student.StudentCourseOfferings.Add(new StudentCourseOffering() { StudentId = student.Id, CourseOfferingId = 2, RegistrationDate = DateTime.Now });
                 studentManager.Register(student, pass);
                 try
                 {
                     MailMessage mail = new MailMessage();
                     SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                     SmtpServer.UseDefaultCredentials = false;
                     SmtpServer.Credentials = new System.Net.NetworkCredential("qwer1x1@gmail.com", "ekokobaba1");
                     SmtpServer.EnableSsl = true;
                     SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                     mail.From = new MailAddress("qwer1x1@gmail.com", "Comp Exam");
                     Console.WriteLine(student.User.Email);
                     mail.To.Add(new MailAddress(student.User.Email));
                     mail.Subject = "Registration";
                     mail.Body = "Your Password Is " + pass;
                     ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                     SmtpServer.Send(mail);
                     Console.WriteLine("mail Send");
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.ToString());
                 }
                 Console.WriteLine(pass);
             }
             Console.WriteLine(RandomString(10));
             Console.ReadKey();
             */

            using (var context = new ApplicationDbContext())
            {
                var students = context.Users.FromSql(@"SELECT Users.Id, Users.Name, Users.Surname, Users.UserName, Users.Email, Users.PasswordHash, Users.PasswordSalt, PhotoId, PhotoUrl
                                                          FROM Users, Students, StudentCourseOfferings 
                                                          WHERE Users.Id = Students.Id 
                                                          AND Students.Id = StudentId 
                                                          AND CourseOfferingId=2")
                                                          .ToList<User>();
                
                foreach (var student in students)
                {
                    Console.WriteLine(student.Name);
                }
            }

            Console.ReadLine();
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
