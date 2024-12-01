using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.IdentityContext
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FirstPassword = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("5ba59a20-2057-4a8a-a417-80c119f79971"), null, new DateTime(2024, 12, 1, 13, 27, 18, 819, DateTimeKind.Local).AddTicks(7850), null, "admin", "ADMIN", 1, null },
                    { new Guid("754ee8ce-7cd4-4ebb-989f-36d3de20772e"), null, new DateTime(2024, 12, 1, 13, 27, 18, 819, DateTimeKind.Local).AddTicks(7894), null, "student", "STUDENT", 1, null },
                    { new Guid("87fb18de-280e-48bc-abc7-80eef7448fe4"), null, new DateTime(2024, 12, 1, 13, 27, 18, 819, DateTimeKind.Local).AddTicks(7892), null, "teacher", "TEACHER", 1, null },
                    { new Guid("bf6a5a19-2bc4-4e47-af9a-c52f4936cc4c"), null, new DateTime(2024, 12, 1, 13, 27, 18, 819, DateTimeKind.Local).AddTicks(7889), null, "customerManager", "CUSTOMERMANAGER", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "FirstName", "FirstPassword", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("389a9486-374b-4a4b-85ef-b2faed25f907"), 0, new DateTime(1996, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "6ad2c0d9-a91a-4632-a182-69b91613d242", new DateTime(2024, 12, 1, 13, 27, 18, 574, DateTimeKind.Local).AddTicks(2037), null, "perin.aycilsahin@bilgeadam.com", false, "Perin", null, "Aycil Şahin", false, null, "PERIN.AYCILSAHIN@BILGEADAM.COM", "PERIN.AYCILSAHIN", "AQAAAAIAAYagAAAAELY9R5d53ozHEbhyOetYoExbj8iNkmoV2EclNHqr1xnJSihos5zA3dvVeJsVFe0yHQ==", null, false, "0ffa9a7e-6bad-4539-9300-39bc30993c5f", 1, false, null, "perin.aycilsahin" },
                    { new Guid("5db9b8aa-54c3-4b7a-a102-b21207d6646c"), 0, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "afc7b942-1e3c-44f6-9c80-5aa8dcb7fac7", new DateTime(2024, 12, 1, 13, 27, 18, 208, DateTimeKind.Local).AddTicks(8968), null, "admin@bilgeadam.com", false, "Administrator", null, "Admin", false, null, "ADMIN@BILGEADAM.COM", "ADMIN", "AQAAAAIAAYagAAAAELrHV2vn6a2i4vicpUPqar3AMEz9dLMPLD7wD3jhW/vwaGaehjgvajr1309ITxResQ==", null, false, "085f5d3c-d671-4114-9f4b-8bcba9fa26fb", 1, false, null, "admin" },
                    { new Guid("79c7f482-f112-4024-aa6c-05df190ce3ff"), 0, new DateTime(1994, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "f7279363-769d-457c-bfe9-e8b447218518", new DateTime(2024, 12, 1, 13, 27, 18, 331, DateTimeKind.Local).AddTicks(1752), null, "pelin.ozerserdar@bilgeadam.com", false, "Pelin", null, "Özer Serdar", false, null, "PELIN.OZERSERDAR@BILGEADAM.COM", "PELIN.OZERSERDAR", "AQAAAAIAAYagAAAAEMNeDA6bYadVdkQSfhh3Kb/oFpcGiJX+hGeCwUVHMcH/jJBEt2yGHrk2gmPE2/mIRw==", null, false, "c9a4437b-916e-4984-86c3-c8fd66f7ef48", 1, false, null, "pelin.ozerserdar" },
                    { new Guid("ca21aa0d-b8b7-433c-89f6-bc2480a694d1"), 0, new DateTime(1985, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "a27579ba-4b86-4364-9e46-cc78c378527e", new DateTime(2024, 12, 1, 13, 27, 18, 696, DateTimeKind.Local).AddTicks(3972), null, "ahmet.cekic@bilgeadam.com", false, "Ahmet", null, "Çekiç", false, null, "AHMET.CEKIC@BILGEADAM.COM", "AHMET.CEKIC", "AQAAAAIAAYagAAAAEIbuzuBQoabsWpyXis2E5J3Aw5DupNNr7pB6Wd0su5yhLSP4rwCMTAyc7EOKheH21g==", null, false, "37890170-9458-4501-bb96-ed6cb942de0e", 1, false, null, "ahmet.cekic" },
                    { new Guid("f2d17592-2c75-4a38-a8db-07e13fc4778f"), 0, new DateTime(1996, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "1150c1af-19e6-487e-b9bc-e5d6e1ab80c6", new DateTime(2024, 12, 1, 13, 27, 18, 451, DateTimeKind.Local).AddTicks(6622), null, "sinaemre.bekar@bilgeadam.com", false, "Sina Emre", null, "Bekar", false, null, "SINAEMRE.BEKAR@BILGEADAM.COM", "SINAEMRE.BEKAR", "AQAAAAIAAYagAAAAEG4ZNBLEaeLadBntnmLJAqqp+daE48dRJ3Mu9B/sRkOatVUtj4nqWmaimK9ThSSHUg==", null, false, "a76132a6-e582-40f3-9009-fea525e1f93f", 1, false, null, "sinaemre.bekar" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("754ee8ce-7cd4-4ebb-989f-36d3de20772e"), new Guid("389a9486-374b-4a4b-85ef-b2faed25f907") },
                    { new Guid("5ba59a20-2057-4a8a-a417-80c119f79971"), new Guid("5db9b8aa-54c3-4b7a-a102-b21207d6646c") },
                    { new Guid("bf6a5a19-2bc4-4e47-af9a-c52f4936cc4c"), new Guid("79c7f482-f112-4024-aa6c-05df190ce3ff") },
                    { new Guid("754ee8ce-7cd4-4ebb-989f-36d3de20772e"), new Guid("ca21aa0d-b8b7-433c-89f6-bc2480a694d1") },
                    { new Guid("87fb18de-280e-48bc-abc7-80eef7448fe4"), new Guid("f2d17592-2c75-4a38-a8db-07e13fc4778f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
