﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Context.ApplicationContext.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HireDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HireDate = table.Column<DateTime>(type: "date", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classrooms_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Exam1 = table.Column<double>(type: "double precision", nullable: true),
                    Exam2 = table.Column<double>(type: "double precision", nullable: true),
                    ProjectExam = table.Column<double>(type: "double precision", nullable: true),
                    ProjectPath = table.Column<string>(type: "text", nullable: true),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    ClassroomId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("a1c775f9-0097-4dec-ab1e-9437a81beaff"), new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(3800), null, ".NET", 1, null },
                    { new Guid("ed370602-3323-4299-87dd-e46f12b087b6"), new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(3824), null, "Java", 1, null }
                });

            migrationBuilder.InsertData(
                table: "CustomerManagers",
                columns: new[] { "Id", "AppUserId", "BirthDate", "CreatedDate", "DeletedDate", "Email", "FirstName", "HireDate", "LastName", "Status", "UpdatedDate" },
                values: new object[] { new Guid("b5e91485-819d-4684-8422-fdf4053d8857"), new Guid("79c7f482-f112-4024-aa6c-05df190ce3ff"), new DateTime(1994, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(4142), null, "pelin.ozerserdar@bilgeadam.com", "Pelin", new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Özer Serdar", 1, null });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "AppUserId", "BirthDate", "CourseId", "CreatedDate", "DeletedDate", "Email", "FirstName", "HireDate", "LastName", "Status", "UpdatedDate" },
                values: new object[] { new Guid("4b838da2-ec21-4d9b-8740-dc375130e3b0"), new Guid("f2d17592-2c75-4a38-a8db-07e13fc4778f"), new DateTime(1996, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1c775f9-0097-4dec-ab1e-9437a81beaff"), new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(4323), null, "sinaemre.bekar@bilgeadam.com", "Sina Emre", new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bekar", 1, null });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "Name", "Status", "TeacherId", "UpdatedDate" },
                values: new object[] { new Guid("4a7cbc57-034e-4511-8e42-ddc5ba586438"), new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(4463), null, "320 Saat Full Stack Developer Yetiştirme Programı", "YZL-8443", 1, new Guid("4b838da2-ec21-4d9b-8740-dc375130e3b0"), null });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AppUserId", "BirthDate", "ClassroomId", "CreatedDate", "DeletedDate", "Email", "Exam1", "Exam2", "FirstName", "ImagePath", "LastName", "ProjectExam", "ProjectName", "ProjectPath", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("257636f5-41e3-4401-9a31-7238f5d7b0af"), new Guid("ca21aa0d-b8b7-433c-89f6-bc2480a694d1"), new DateTime(1985, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4a7cbc57-034e-4511-8e42-ddc5ba586438"), new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(4628), null, "ahmet.cekic@bilgeadam.com", null, null, "Ahmet", null, "Çekiç", null, null, null, 1, null },
                    { new Guid("9286ae43-cab9-48fc-8183-421ead3232be"), new Guid("389a9486-374b-4a4b-85ef-b2faed25f907"), new DateTime(1996, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4a7cbc57-034e-4511-8e42-ddc5ba586438"), new DateTime(2024, 10, 26, 11, 21, 38, 922, DateTimeKind.Utc).AddTicks(4621), null, "perin.aycilsahin@bilgeadam.com", null, null, "Perin", null, "Aycil Şahin", null, null, null, 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_TeacherId",
                table: "Classrooms",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassroomId",
                table: "Students",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CourseId",
                table: "Teachers",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerManagers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
