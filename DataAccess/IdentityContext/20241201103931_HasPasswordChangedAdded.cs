using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.IdentityContext
{
    /// <inheritdoc />
    public partial class HasPasswordChangedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPasswordChanged",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5ba59a20-2057-4a8a-a417-80c119f79971"),
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 39, 30, 539, DateTimeKind.Local).AddTicks(6120));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("754ee8ce-7cd4-4ebb-989f-36d3de20772e"),
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 39, 30, 539, DateTimeKind.Local).AddTicks(6166));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("87fb18de-280e-48bc-abc7-80eef7448fe4"),
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 39, 30, 539, DateTimeKind.Local).AddTicks(6164));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bf6a5a19-2bc4-4e47-af9a-c52f4936cc4c"),
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 39, 30, 539, DateTimeKind.Local).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("389a9486-374b-4a4b-85ef-b2faed25f907"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "HasPasswordChanged", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c99a4089-d351-4dd3-ae08-9a038605a648", new DateTime(2024, 12, 1, 13, 39, 30, 287, DateTimeKind.Local).AddTicks(8106), false, "AQAAAAIAAYagAAAAEEQMblyw7oXySI93SlR0yJqbaCnKLrvdiodGi5QRaiwNIk0z7D+38QfVlS7vAGTkLw==", "4924206f-d7ca-463a-b358-8161d9f03407" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5db9b8aa-54c3-4b7a-a102-b21207d6646c"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "HasPasswordChanged", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cc47dc0-e517-4831-923b-e438e5285151", new DateTime(2024, 12, 1, 13, 39, 29, 909, DateTimeKind.Local).AddTicks(4868), false, "AQAAAAIAAYagAAAAEBGSi6ubbiZuPjiqgBmFkWEZ3+VuVLWJhI6G6hs0oK6FK2ZTLLjqTLOnv108xurnxg==", "8384806e-79c6-410c-8dce-2a2696191ae4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("79c7f482-f112-4024-aa6c-05df190ce3ff"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "HasPasswordChanged", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a220282-ac35-4216-94ae-5af890cf536b", new DateTime(2024, 12, 1, 13, 39, 30, 35, DateTimeKind.Local).AddTicks(3668), false, "AQAAAAIAAYagAAAAEB23BkHkvo2cyiCCKUWHbCXQiuQ/f2/kQ9el3KcdKHgO/HqvwsGWSgHoxIPP3lJb7w==", "c3e7ffbe-d109-491f-9409-d177c2e73192" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ca21aa0d-b8b7-433c-89f6-bc2480a694d1"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "HasPasswordChanged", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8970189e-5071-4384-a9fd-38949ce44330", new DateTime(2024, 12, 1, 13, 39, 30, 412, DateTimeKind.Local).AddTicks(2445), false, "AQAAAAIAAYagAAAAEN7QjwnvApeZSmYWMzaAnLOgwvkODmiffSkiOgdbabXu8pa1TbXXCzlGoqawXnL/4Q==", "9bc30ef0-5377-4c2d-8c52-f15042d8dfb1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f2d17592-2c75-4a38-a8db-07e13fc4778f"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "HasPasswordChanged", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33ccfc0a-ab5f-455c-af4e-637701364439", new DateTime(2024, 12, 1, 13, 39, 30, 164, DateTimeKind.Local).AddTicks(8737), false, "AQAAAAIAAYagAAAAEHATJzGNvacQBXqRFB23U3L+c9aypst8UAv1ixHw9+udSFGyTA1p2+9eR8fjVYrjOA==", "3b75ab27-8d78-4415-b76b-a4e8698534bd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPasswordChanged",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5ba59a20-2057-4a8a-a417-80c119f79971"),
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 27, 18, 819, DateTimeKind.Local).AddTicks(7850));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("754ee8ce-7cd4-4ebb-989f-36d3de20772e"),
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 27, 18, 819, DateTimeKind.Local).AddTicks(7894));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("87fb18de-280e-48bc-abc7-80eef7448fe4"),
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 27, 18, 819, DateTimeKind.Local).AddTicks(7892));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bf6a5a19-2bc4-4e47-af9a-c52f4936cc4c"),
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 27, 18, 819, DateTimeKind.Local).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("389a9486-374b-4a4b-85ef-b2faed25f907"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ad2c0d9-a91a-4632-a182-69b91613d242", new DateTime(2024, 12, 1, 13, 27, 18, 574, DateTimeKind.Local).AddTicks(2037), "AQAAAAIAAYagAAAAELY9R5d53ozHEbhyOetYoExbj8iNkmoV2EclNHqr1xnJSihos5zA3dvVeJsVFe0yHQ==", "0ffa9a7e-6bad-4539-9300-39bc30993c5f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5db9b8aa-54c3-4b7a-a102-b21207d6646c"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afc7b942-1e3c-44f6-9c80-5aa8dcb7fac7", new DateTime(2024, 12, 1, 13, 27, 18, 208, DateTimeKind.Local).AddTicks(8968), "AQAAAAIAAYagAAAAELrHV2vn6a2i4vicpUPqar3AMEz9dLMPLD7wD3jhW/vwaGaehjgvajr1309ITxResQ==", "085f5d3c-d671-4114-9f4b-8bcba9fa26fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("79c7f482-f112-4024-aa6c-05df190ce3ff"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7279363-769d-457c-bfe9-e8b447218518", new DateTime(2024, 12, 1, 13, 27, 18, 331, DateTimeKind.Local).AddTicks(1752), "AQAAAAIAAYagAAAAEMNeDA6bYadVdkQSfhh3Kb/oFpcGiJX+hGeCwUVHMcH/jJBEt2yGHrk2gmPE2/mIRw==", "c9a4437b-916e-4984-86c3-c8fd66f7ef48" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ca21aa0d-b8b7-433c-89f6-bc2480a694d1"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a27579ba-4b86-4364-9e46-cc78c378527e", new DateTime(2024, 12, 1, 13, 27, 18, 696, DateTimeKind.Local).AddTicks(3972), "AQAAAAIAAYagAAAAEIbuzuBQoabsWpyXis2E5J3Aw5DupNNr7pB6Wd0su5yhLSP4rwCMTAyc7EOKheH21g==", "37890170-9458-4501-bb96-ed6cb942de0e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f2d17592-2c75-4a38-a8db-07e13fc4778f"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1150c1af-19e6-487e-b9bc-e5d6e1ab80c6", new DateTime(2024, 12, 1, 13, 27, 18, 451, DateTimeKind.Local).AddTicks(6622), "AQAAAAIAAYagAAAAEG4ZNBLEaeLadBntnmLJAqqp+daE48dRJ3Mu9B/sRkOatVUtj4nqWmaimK9ThSSHUg==", "a76132a6-e582-40f3-9009-fea525e1f93f" });
        }
    }
}
