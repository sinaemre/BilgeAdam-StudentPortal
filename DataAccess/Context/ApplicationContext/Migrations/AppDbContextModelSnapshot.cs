﻿// <auto-generated />
using System;
using DataAccess.Context.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Context.ApplicationContext.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApplicationCore.Entities.Concrete.Classroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Classrooms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4a7cbc57-034e-4511-8e42-ddc5ba586438"),
                            CreatedDate = new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(4463),
                            Description = "320 Saat Full Stack Developer Yetiştirme Programı",
                            Name = "YZL-8443",
                            Status = 1,
                            TeacherId = new Guid("4b838da2-ec21-4d9b-8740-dc375130e3b0")
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entities.Concrete.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a1c775f9-0097-4dec-ab1e-9437a81beaff"),
                            CreatedDate = new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(3800),
                            Name = ".NET",
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("ed370602-3323-4299-87dd-e46f12b087b6"),
                            CreatedDate = new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(3824),
                            Name = "Java",
                            Status = 1
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entities.Concrete.CustomerManager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("date");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("CustomerManagers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b5e91485-819d-4684-8422-fdf4053d8857"),
                            AppUserId = new Guid("79c7f482-f112-4024-aa6c-05df190ce3ff"),
                            BirthDate = new DateTime(1994, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(4142),
                            Email = "pelin.ozerserdar@bilgeadam.com",
                            FirstName = "Pelin",
                            HireDate = new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Özer Serdar",
                            Status = 1
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entities.Concrete.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<Guid>("ClassroomId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("Exam1")
                        .HasColumnType("double precision");

                    b.Property<double?>("Exam2")
                        .HasColumnType("double precision");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<double?>("ProjectExam")
                        .HasColumnType("double precision");

                    b.Property<string>("ProjectName")
                        .HasColumnType("text");

                    b.Property<string>("ProjectPath")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9286ae43-cab9-48fc-8183-421ead3232be"),
                            AppUserId = new Guid("389a9486-374b-4a4b-85ef-b2faed25f907"),
                            BirthDate = new DateTime(1996, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ClassroomId = new Guid("4a7cbc57-034e-4511-8e42-ddc5ba586438"),
                            CreatedDate = new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(4621),
                            Email = "perin.aycilsahin@bilgeadam.com",
                            FirstName = "Perin",
                            LastName = "Aycil Şahin",
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("257636f5-41e3-4401-9a31-7238f5d7b0af"),
                            AppUserId = new Guid("ca21aa0d-b8b7-433c-89f6-bc2480a694d1"),
                            BirthDate = new DateTime(1985, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ClassroomId = new Guid("4a7cbc57-034e-4511-8e42-ddc5ba586438"),
                            CreatedDate = new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(4628),
                            Email = "ahmet.cekic@bilgeadam.com",
                            FirstName = "Ahmet",
                            LastName = "Çekiç",
                            Status = 1
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entities.Concrete.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("date");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Teachers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4b838da2-ec21-4d9b-8740-dc375130e3b0"),
                            AppUserId = new Guid("f2d17592-2c75-4a38-a8db-07e13fc4778f"),
                            BirthDate = new DateTime(1996, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CourseId = new Guid("a1c775f9-0097-4dec-ab1e-9437a81beaff"),
                            CreatedDate = new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(4323),
                            Email = "sinaemre.bekar@bilgeadam.com",
                            FirstName = "Sina Emre",
                            HireDate = new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Bekar",
                            Status = 1
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entities.Concrete.Classroom", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Concrete.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Concrete.Student", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Concrete.Classroom", "Classroom")
                        .WithMany("Students")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Concrete.Teacher", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Concrete.Course", "Course")
                        .WithMany("Teachers")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Concrete.Classroom", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Concrete.Course", b =>
                {
                    b.Navigation("Teachers");
                });
#pragma warning restore 612, 618
        }
    }
}
