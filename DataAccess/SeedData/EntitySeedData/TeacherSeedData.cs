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
                        CourseId = Guid.Parse("a1c775f9-0097-4dec-ab1e-9437a81beaff")
                    }
                );
        }
    }
}
