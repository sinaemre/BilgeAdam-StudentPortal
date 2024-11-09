using FluentValidation;
using FluentValidation.AspNetCore;
using WEB.FluentValidation.ClassroomValidators;
using WEB.FluentValidation.CourseValidators;
using WEB.FluentValidation.StudentValidators;
using WEB.FluentValidation.TeacherValidators;

namespace WEB.ServiceExtensions
{
    public static class ValidatorServiceRegisteration
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateTeacherValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateTeacherValidator>();
            
            services.AddValidatorsFromAssemblyContaining<CreateCourseValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCourseValidator>();
            
            services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateStudentValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateClassroomValidator>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
        }
    }
}
