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
    public class CustomerManagerSeedData : IEntityTypeConfiguration<CustomerManager>
    {
        public void Configure(EntityTypeBuilder<CustomerManager> builder)
        {
            builder.HasData
                (
                    new CustomerManager
                    {
                        Id = Guid.Parse("b5e91485-819d-4684-8422-fdf4053d8857"),
                        FirstName = "Pelin",
                        LastName = "Özer Serdar",
                        BirthDate = new DateTime(1994, 05, 06),
                        Email = "pelin.ozerserdar@bilgeadam.com",
                        HireDate = new DateTime(2023, 05, 05)
                    }
                );
        }
    }
}
