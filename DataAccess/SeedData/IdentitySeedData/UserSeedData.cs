using ApplicationCore.UserEntites.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SeedData.IdentitySeedData
{
    public class UserSeedData : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();

            var admin = new AppUser
            {
                Id = Guid.Parse("5db9b8aa-54c3-4b7a-a102-b21207d6646c"),
                FirstName = "Administrator",
                LastName = "Admin",
                BirthDate = new DateTime(1990, 01, 01),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@bilgeadam.com",
                NormalizedEmail = "ADMIN@BILGEADAM.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                HasPasswordChanged = true
            };

            var customerManager = new AppUser
            {
                Id = Guid.Parse("79c7f482-f112-4024-aa6c-05df190ce3ff"),
                FirstName = "Pelin",
                LastName = "Özer Serdar",
                BirthDate = new DateTime(1994, 05, 06),
                UserName = "pelin.ozerserdar",
                NormalizedUserName = "PELIN.OZERSERDAR",
                Email = "pelin.ozerserdar@bilgeadam.com",
                NormalizedEmail = "PELIN.OZERSERDAR@BILGEADAM.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                HasPasswordChanged = true
            };

            var teacher = new AppUser
            {
                Id = Guid.Parse("f2d17592-2c75-4a38-a8db-07e13fc4778f"),
                FirstName = "Sina Emre",
                LastName = "Bekar",
                BirthDate = new DateTime(1996, 01, 23),
                UserName = "sinaemre.bekar",
                NormalizedUserName = "SINAEMRE.BEKAR",
                Email = "sinaemre.bekar@bilgeadam.com",
                NormalizedEmail = "SINAEMRE.BEKAR@BILGEADAM.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                HasPasswordChanged = true
            };

            var student1 = new AppUser
            {
                Id = Guid.Parse("389a9486-374b-4a4b-85ef-b2faed25f907"),
                FirstName = "Perin",
                LastName = "Aycil Şahin",
                BirthDate = new DateTime(1996, 08, 08),
                UserName = "perin.aycilsahin",
                NormalizedUserName = "PERIN.AYCILSAHIN",
                Email = "perin.aycilsahin@bilgeadam.com",
                NormalizedEmail = "PERIN.AYCILSAHIN@BILGEADAM.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                HasPasswordChanged = true
            };

            var student2 = new AppUser
            {
                Id = Guid.Parse("ca21aa0d-b8b7-433c-89f6-bc2480a694d1"),
                FirstName = "Ahmet",
                LastName = "Çekiç",
                BirthDate = new DateTime(1985, 11, 03),
                UserName = "ahmet.cekic",
                NormalizedUserName = "AHMET.CEKIC",
                Email = "ahmet.cekic@bilgeadam.com",
                NormalizedEmail = "AHMET.CEKIC@BILGEADAM.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                HasPasswordChanged = true
            };

            builder.HasData(admin, customerManager, teacher, student1, student2);
        }
    }
}
