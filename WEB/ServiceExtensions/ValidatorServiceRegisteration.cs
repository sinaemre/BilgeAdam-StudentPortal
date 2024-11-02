using FluentValidation;
using FluentValidation.AspNetCore;
using WEB.FluentValidation.TeacherValidators;

namespace WEB.ServiceExtensions
{
    public static class ValidatorServiceRegisteration
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateTeacherValidator>();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
        }
    }
}
