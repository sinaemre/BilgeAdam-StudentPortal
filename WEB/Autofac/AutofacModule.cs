using Autofac;
using AutoMapper;
using Business.AutoMapper;
using Business.Manager.Concrete;
using Business.Manager.Interface;
using DataAccess.Services.Concrete;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Identity.UI.Services;
using WEB.AutoMapper;

namespace WEB.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(BaseRepository<>).Assembly)
                .AsClosedTypesOf(typeof(IBaseRepository<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(typeof(BaseManager<,>).Assembly)
                .AsClosedTypesOf(typeof(IBaseManager<,>))
                .InstancePerLifetimeScope();

            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();
            builder.RegisterType<RoleManager>().As<IRoleManager>().InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();

            var mappingconfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TeacherMapping());
                mc.AddProfile(new CourseMapping());
                mc.AddProfile(new ClassroomMapping());
                mc.AddProfile(new StudentMapping());
                mc.AddProfile(new CustomerManagerMapping());
                mc.AddProfile(new RoleMapping());
                mc.AddProfile(new UserMapping());
                mc.AddProfile(new AccountMapping());

                mc.AddProfile(new TeacherBusinessMapping());
                mc.AddProfile(new CourseBusinessMapping());
                mc.AddProfile(new ClassroomBusinessMapping());
                mc.AddProfile(new StudentBusinessMapping());
                mc.AddProfile(new CustomerManagerBusinessMapping());
                mc.AddProfile(new RoleBusinessMapping());
                mc.AddProfile(new UserBusinessMapping());
                mc.AddProfile(new AccountBusinessMapping());
            });

            IMapper mapper = mappingconfig.CreateMapper();
            builder.RegisterInstance<IMapper>(mapper);

            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
        }
    }
}
