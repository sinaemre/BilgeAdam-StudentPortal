using FluentValidation;
using FluentValidation.AspNetCore;
using WEB.FluentValidation.AccountValidators;
using WEB.FluentValidation.ClassroomValidators;
using WEB.FluentValidation.CourseValidators;
using WEB.FluentValidation.CustomerManagerValidators;
using WEB.FluentValidation.RoleValidators;
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
            services.AddValidatorsFromAssemblyContaining<UpdateClassroomValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateCustomerManagerValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCustomerManagerValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateRoleValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateRoleValidator>();

            services.AddValidatorsFromAssemblyContaining<LoginValidator>();
            services.AddValidatorsFromAssemblyContaining<EditUserValidator>();
            services.AddValidatorsFromAssemblyContaining<ChangePasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<CreatePasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<ForgotPasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<ResetPasswordValidator>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
        }
    }
}
