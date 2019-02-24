using System.Text;
using Cet.BusinessLogic.Abstract;
using Cet.BusinessLogic.Concrete;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace Cet.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Service and Repository Dependency Injection
            services.AddScoped<IAdministratorService, AdministratorManager>();
            services.AddScoped<IAdministratorRepository, AdministratorRepository>();

            services.AddScoped<IInstructorService, InstructorManager>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();

            services.AddScoped<IStudentService, StudentManager>();
            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddScoped<IDepartmentService, DepartmentManager>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<ICourseService, CourseManager>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            services.AddScoped<IExamService, ExamManager>();
            services.AddScoped<IExamRepository, ExamRepository>();

            services.AddScoped<IQuestionService, QuestionManager>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();

            services.AddScoped<IAnswerService, AnswerManager>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();

            services.AddScoped<ICourseOfferingService, CourseOfferingManager>();
            services.AddScoped<ICourseOfferingRepository, CourseOfferingRepository>();

            services.AddScoped<IExamStatusService, ExamStatusManager>();
            services.AddScoped<IExamRepository, ExamRepository>();

            services.AddScoped<IQuestionTypeService, QuestionTypeManager>();
            services.AddScoped<IQuestionTypeRepository, QuestionTypeRepository>();

            services.AddScoped<IStudentCourseOfferingService, StudentCourseOfferingManager>();
            services.AddScoped<IStudentCourseOfferingsRepository, StudentCourseOfferingRepository>();

            // Microsoft Injections
            services.Configure<IISOptions>(opt =>
            {
                opt.AutomaticAuthentication = true;

            });

            services.AddCors();
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            //app.UseCors(builder => builder.WithOrigins("http://localhost:44320"));

        }
    }
}
