﻿using AutoMapper;
using Cet.Entities.Concrete;
using Cet.WebApi.Dtos;

namespace Cet.WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.User.Surname))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.User.PhotoUrl))
                .ForMember(dest => dest.StudentCourses, opt => opt.MapFrom(src => src.StudentCourseOfferings));

            CreateMap<StudentCourseOffering, StudentCourseDto>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src=> src.CourseOffering.Id))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseOffering.Course.Name))
                .ForMember(dest => dest.Exams, opt => opt.MapFrom(src => src.CourseOffering.Exams));

            CreateMap<Exam, ExamDto>()
                .ForMember(dest => dest.ExamId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.ExamStatusId, opt => opt.MapFrom(src => src.ExamStatusId));
        }
    }
}