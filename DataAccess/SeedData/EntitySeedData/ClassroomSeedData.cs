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
    public class ClassroomSeedData : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasData
                (
                    new Classroom
                    {
                        Id = Guid.Parse("4a7cbc57-034e-4511-8e42-ddc5ba586438"),
                        Name = "YZL-8443",
                        Description = "Yaz Dönemi Sınıfı",
                        StartDate = new DateTime(2024, 12, 02),
                        TeacherId = Guid.Parse("4b838da2-ec21-4d9b-8740-dc375130e3b0")
                    }
                );
        }
    }
}
