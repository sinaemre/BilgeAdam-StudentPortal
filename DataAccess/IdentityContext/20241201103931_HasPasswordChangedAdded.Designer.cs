﻿// <auto-generated />
using System;
using DataAccess.Context.IdentityContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.IdentityContext
{
    [DbContext(typeof(AppIdentityDbContext))]
    [Migration("20241201103931_HasPasswordChangedAdded")]
    partial class HasPasswordChangedAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApplicationCore.UserEntites.Concrete.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5ba59a20-2057-4a8a-a417-80c119f79971"),
                            CreatedDate = new DateTime(2024, 12, 1, 13, 39, 30, 539, DateTimeKind.Local).AddTicks(6120),
                            Name = "admin",
                            NormalizedName = "ADMIN",
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("bf6a5a19-2bc4-4e47-af9a-c52f4936cc4c"),
                            CreatedDate = new DateTime(2024, 12, 1, 13, 39, 30, 539, DateTimeKind.Local).AddTicks(6160),
                            Name = "customerManager",
                            NormalizedName = "CUSTOMERMANAGER",
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("87fb18de-280e-48bc-abc7-80eef7448fe4"),
                            CreatedDate = new DateTime(2024, 12, 1, 13, 39, 30, 539, DateTimeKind.Local).AddTicks(6164),
                            Name = "teacher",
                            NormalizedName = "TEACHER",
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("754ee8ce-7cd4-4ebb-989f-36d3de20772e"),
                            CreatedDate = new DateTime(2024, 12, 1, 13, 39, 30, 539, DateTimeKind.Local).AddTicks(6166),
                            Name = "student",
                            NormalizedName = "STUDENT",
                            Status = 1
                        });
                });

            modelBuilder.Entity("ApplicationCore.UserEntites.Concrete.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstPassword")
                        .HasColumnType("text");

                    b.Property<bool>("HasPasswordChanged")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5db9b8aa-54c3-4b7a-a102-b21207d6646c"),
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "9cc47dc0-e517-4831-923b-e438e5285151",
                            CreatedDate = new DateTime(2024, 12, 1, 13, 39, 29, 909, DateTimeKind.Local).AddTicks(4868),
                            Email = "admin@bilgeadam.com",
                            EmailConfirmed = false,
                            FirstName = "Administrator",
                            HasPasswordChanged = false,
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@BILGEADAM.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEBGSi6ubbiZuPjiqgBmFkWEZ3+VuVLWJhI6G6hs0oK6FK2ZTLLjqTLOnv108xurnxg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8384806e-79c6-410c-8dce-2a2696191ae4",
                            Status = 1,
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("79c7f482-f112-4024-aa6c-05df190ce3ff"),
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1994, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "0a220282-ac35-4216-94ae-5af890cf536b",
                            CreatedDate = new DateTime(2024, 12, 1, 13, 39, 30, 35, DateTimeKind.Local).AddTicks(3668),
                            Email = "pelin.ozerserdar@bilgeadam.com",
                            EmailConfirmed = false,
                            FirstName = "Pelin",
                            HasPasswordChanged = false,
                            LastName = "Özer Serdar",
                            LockoutEnabled = false,
                            NormalizedEmail = "PELIN.OZERSERDAR@BILGEADAM.COM",
                            NormalizedUserName = "PELIN.OZERSERDAR",
                            PasswordHash = "AQAAAAIAAYagAAAAEB23BkHkvo2cyiCCKUWHbCXQiuQ/f2/kQ9el3KcdKHgO/HqvwsGWSgHoxIPP3lJb7w==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c3e7ffbe-d109-491f-9409-d177c2e73192",
                            Status = 1,
                            TwoFactorEnabled = false,
                            UserName = "pelin.ozerserdar"
                        },
                        new
                        {
                            Id = new Guid("f2d17592-2c75-4a38-a8db-07e13fc4778f"),
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1996, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "33ccfc0a-ab5f-455c-af4e-637701364439",
                            CreatedDate = new DateTime(2024, 12, 1, 13, 39, 30, 164, DateTimeKind.Local).AddTicks(8737),
                            Email = "sinaemre.bekar@bilgeadam.com",
                            EmailConfirmed = false,
                            FirstName = "Sina Emre",
                            HasPasswordChanged = false,
                            LastName = "Bekar",
                            LockoutEnabled = false,
                            NormalizedEmail = "SINAEMRE.BEKAR@BILGEADAM.COM",
                            NormalizedUserName = "SINAEMRE.BEKAR",
                            PasswordHash = "AQAAAAIAAYagAAAAEHATJzGNvacQBXqRFB23U3L+c9aypst8UAv1ixHw9+udSFGyTA1p2+9eR8fjVYrjOA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3b75ab27-8d78-4415-b76b-a4e8698534bd",
                            Status = 1,
                            TwoFactorEnabled = false,
                            UserName = "sinaemre.bekar"
                        },
                        new
                        {
                            Id = new Guid("389a9486-374b-4a4b-85ef-b2faed25f907"),
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1996, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "c99a4089-d351-4dd3-ae08-9a038605a648",
                            CreatedDate = new DateTime(2024, 12, 1, 13, 39, 30, 287, DateTimeKind.Local).AddTicks(8106),
                            Email = "perin.aycilsahin@bilgeadam.com",
                            EmailConfirmed = false,
                            FirstName = "Perin",
                            HasPasswordChanged = false,
                            LastName = "Aycil Şahin",
                            LockoutEnabled = false,
                            NormalizedEmail = "PERIN.AYCILSAHIN@BILGEADAM.COM",
                            NormalizedUserName = "PERIN.AYCILSAHIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEEQMblyw7oXySI93SlR0yJqbaCnKLrvdiodGi5QRaiwNIk0z7D+38QfVlS7vAGTkLw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "4924206f-d7ca-463a-b358-8161d9f03407",
                            Status = 1,
                            TwoFactorEnabled = false,
                            UserName = "perin.aycilsahin"
                        },
                        new
                        {
                            Id = new Guid("ca21aa0d-b8b7-433c-89f6-bc2480a694d1"),
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1985, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "8970189e-5071-4384-a9fd-38949ce44330",
                            CreatedDate = new DateTime(2024, 12, 1, 13, 39, 30, 412, DateTimeKind.Local).AddTicks(2445),
                            Email = "ahmet.cekic@bilgeadam.com",
                            EmailConfirmed = false,
                            FirstName = "Ahmet",
                            HasPasswordChanged = false,
                            LastName = "Çekiç",
                            LockoutEnabled = false,
                            NormalizedEmail = "AHMET.CEKIC@BILGEADAM.COM",
                            NormalizedUserName = "AHMET.CEKIC",
                            PasswordHash = "AQAAAAIAAYagAAAAEN7QjwnvApeZSmYWMzaAnLOgwvkODmiffSkiOgdbabXu8pa1TbXXCzlGoqawXnL/4Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9bc30ef0-5377-4c2d-8c52-f15042d8dfb1",
                            Status = 1,
                            TwoFactorEnabled = false,
                            UserName = "ahmet.cekic"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("5db9b8aa-54c3-4b7a-a102-b21207d6646c"),
                            RoleId = new Guid("5ba59a20-2057-4a8a-a417-80c119f79971")
                        },
                        new
                        {
                            UserId = new Guid("79c7f482-f112-4024-aa6c-05df190ce3ff"),
                            RoleId = new Guid("bf6a5a19-2bc4-4e47-af9a-c52f4936cc4c")
                        },
                        new
                        {
                            UserId = new Guid("f2d17592-2c75-4a38-a8db-07e13fc4778f"),
                            RoleId = new Guid("87fb18de-280e-48bc-abc7-80eef7448fe4")
                        },
                        new
                        {
                            UserId = new Guid("389a9486-374b-4a4b-85ef-b2faed25f907"),
                            RoleId = new Guid("754ee8ce-7cd4-4ebb-989f-36d3de20772e")
                        },
                        new
                        {
                            UserId = new Guid("ca21aa0d-b8b7-433c-89f6-bc2480a694d1"),
                            RoleId = new Guid("754ee8ce-7cd4-4ebb-989f-36d3de20772e")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("ApplicationCore.UserEntites.Concrete.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("ApplicationCore.UserEntites.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("ApplicationCore.UserEntites.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("ApplicationCore.UserEntites.Concrete.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.UserEntites.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("ApplicationCore.UserEntites.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
