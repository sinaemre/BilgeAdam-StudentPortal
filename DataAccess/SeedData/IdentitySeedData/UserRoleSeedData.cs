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
    public class UserRoleSeedData : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData
                (
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse("5ba59a20-2057-4a8a-a417-80c119f79971"),
                        UserId = Guid.Parse("5db9b8aa-54c3-4b7a-a102-b21207d6646c")
                    },
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse("bf6a5a19-2bc4-4e47-af9a-c52f4936cc4c"),
                        UserId = Guid.Parse("79c7f482-f112-4024-aa6c-05df190ce3ff")
                    },
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse("87fb18de-280e-48bc-abc7-80eef7448fe4"),
                        UserId = Guid.Parse("f2d17592-2c75-4a38-a8db-07e13fc4778f")
                    },
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse("754ee8ce-7cd4-4ebb-989f-36d3de20772e"),
                        UserId = Guid.Parse("389a9486-374b-4a4b-85ef-b2faed25f907")
                    },
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Guid.Parse("754ee8ce-7cd4-4ebb-989f-36d3de20772e"),
                        UserId = Guid.Parse("ca21aa0d-b8b7-433c-89f6-bc2480a694d1")
                    }
                );
        }
    }
}
