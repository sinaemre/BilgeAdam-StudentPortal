using Autofac;
using AutoMapper;
using Business.AutoMapper;
using Business.Manager.Concrete;
using Business.Manager.Interface;
using DataAccess.Services.Concrete;
using DataAccess.Services.Interface;
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

            var mappingconfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TeacherMapping());
                mc.AddProfile(new CourseMapping());
                mc.AddProfile(new ClassroomMapping());
                mc.AddProfile(new StudentMapping());

                mc.AddProfile(new TeacherBusinessMapping());
                mc.AddProfile(new CourseBusinessMapping());
                mc.AddProfile(new ClassroomBusinessMapping());
                mc.AddProfile(new StudentBusinessMapping());
            });

            IMapper mapper = mappingconfig.CreateMapper();
            builder.RegisterInstance<IMapper>(mapper);

        }
    }
}
