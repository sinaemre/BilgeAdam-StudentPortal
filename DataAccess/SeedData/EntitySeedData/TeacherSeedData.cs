using ApplicationCore.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SeedData.EntitySeedData
{
    public class TeacherSeedData : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasData
                (
                    new Teacher
                    {
                        Id = Guid.Parse("4b838da2-ec21-4d9b-8740-dc375130e3b0"),
                        FirstName = "Sina Emre",
                        LastName = "Bekar",
                        BirthDate = new DateTime(1996, 01, 23),
                        Email = "sinaemre.bekar@bilgeadam.com",
                        HireDate = new DateTime(2022, 06, 20),
                        AppUserId = Guid.Parse("f2d17592-2c75-4a38-a8db-07e13fc4778f")
                    }
                );
        }
    }
}
