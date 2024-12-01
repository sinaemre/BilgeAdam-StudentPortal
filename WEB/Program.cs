using ApplicationCore.UserEntites.Concrete;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataAccess.Context.ApplicationContext;
using DataAccess.Context.IdentityContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WEB.Autofac;
using WEB.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new AutofacModule());
            });


builder.Services.AddValidators();
// Add services to the container.
builder.Services.AddControllersWithViews();

var entityDbConnection = builder.Configuration.GetConnectionString("EntityDbConnection");
var identityDbConnection = builder.Configuration.GetConnectionString("IdentityDbConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(entityDbConnection);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseNpgsql(identityDbConnection);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.SignIn.RequireConfirmedPhoneNumber = false;
    x.SignIn.RequireConfirmedEmail = false;
    x.SignIn.RequireConfirmedAccount = false;
    x.User.RequireUniqueEmail = true;
    x.Password.RequiredLength = 3;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
    x.Password.RequireLowercase = false;
    x.Lockout.MaxFailedAccessAttempts = 5; //Þifreyi 5 kere yanlýþ girerse
    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //5 dakika boyunca hesabýný kitle
})
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(10);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
