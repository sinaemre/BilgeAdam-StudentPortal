using Autofac;
using AutoMapper;
using Business.Manager.Concrete;
using Business.Manager.Interface;
using DataAccess.Services.Concrete;
using DataAccess.Services.Interface;

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

            });

            IMapper mapper = mappingconfig.CreateMapper();
            builder.RegisterInstance<IMapper>(mapper);

        }
    }
}
