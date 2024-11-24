using ApplicationCore.UserEntites.Concrete;
using DataAccess.SeedData.IdentitySeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context.IdentityContext
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        static AppIdentityDbContext()
        {
            //Sizin yerel saatinizle uyumlu çalışarak, saat hatası vermeyi önler
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserSeedData());
            builder.ApplyConfiguration(new RoleSeedData());
            builder.ApplyConfiguration(new UserRoleSeedData());

            builder.Entity<AppRole>()
                .Property(x => x.ConcurrencyStamp)
                .IsConcurrencyToken(false);
            
            // builder.Entity<AppUser>()
            //     .Property(x => x.RowVersion)
            //     .IsRowVersion();
        }
    }
}
