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
    public class StudentSeedData : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData
                (
                    new Student
                    {
                        Id = Guid.Parse("9286ae43-cab9-48fc-8183-421ead3232be"),
                        FirstName = "Perin",
                        LastName = "Aycil Şahin",
                        BirthDate = new DateTime(1996, 08, 08),
                        Email = "perin.aycilsahin@bilgeadam.com",
                        RegisterPrice = 120000,
                        ClassroomId = Guid.Parse("4a7cbc57-034e-4511-8e42-ddc5ba586438")
                    },
                    new Student
                    {
                        Id = Guid.Parse("257636f5-41e3-4401-9a31-7238f5d7b0af"),
                        FirstName = "Ahmet",
                        LastName = "Çekiç",
                        BirthDate = new DateTime(1985, 11, 03),
                        RegisterPrice = 110000,
                        Email = "ahmet.cekic@bilgeadam.com",
                        ClassroomId = Guid.Parse("4a7cbc57-034e-4511-8e42-ddc5ba586438")
                    }
                );
        }
    }
}
