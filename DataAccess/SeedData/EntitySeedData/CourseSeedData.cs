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
    public class CourseSeedData : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData
                (
                    new Course
                    {
                        Id = Guid.Parse("a1c775f9-0097-4dec-ab1e-9437a81beaff"),
                        Name = ".NET",
                        TotalHour = 320
                    },
                    new Course
                    {
                        Id = Guid.Parse("ed370602-3323-4299-87dd-e46f12b087b6"),
                        Name = "Java",
                        TotalHour = 250
                    }
                );
        }
    }
}
