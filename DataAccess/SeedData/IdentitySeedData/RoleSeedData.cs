using ApplicationCore.UserEntites.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SeedData.IdentitySeedData
{
    public class RoleSeedData : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            var admin = new AppRole
            {
                Id = Guid.Parse("5ba59a20-2057-4a8a-a417-80c119f79971"),
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            var customerManager = new AppRole
            {
                Id = Guid.Parse("bf6a5a19-2bc4-4e47-af9a-c52f4936cc4c"),
                Name = "customerManager",
                NormalizedName = "CUSTOMERMANAGER"
            };

            var teacher = new AppRole
            {
                Id = Guid.Parse("87fb18de-280e-48bc-abc7-80eef7448fe4"),
                Name = "teacher",
                NormalizedName = "TEACHER"
            };

            var student = new AppRole
            {
                Id = Guid.Parse("754ee8ce-7cd4-4ebb-989f-36d3de20772e"),
                Name = "student",
                NormalizedName = "STUDENT"
            };

            builder.HasData(admin, customerManager, teacher, student);
        }
    }
}
