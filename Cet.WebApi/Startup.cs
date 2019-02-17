using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.BusinessLogic.Abstract;
using Cet.BusinessLogic.Concrete;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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

            // Microsoft Injections
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:44320"));
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
