using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibrarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedDataIntoRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsMember", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a666d8a-96ae-45c5-9882-a1e043fdf49e", "346c0883-4e31-4c6b-a354-c107845d8135", true, "Member", "MEMBER" },
                    { "aeeb9374-30dd-4e80-9feb-000783afe3bc", "882a39d8-f900-4b8f-860a-664d03fc929d", false, "Admin", "ADMIN" },
                    { "dfb4eac8-3660-42ed-a013-2696776840d7", "d22530c5-5dec-4343-b16a-907ef4106029", false, "SuperAdmin", "SUPERADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a666d8a-96ae-45c5-9882-a1e043fdf49e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aeeb9374-30dd-4e80-9feb-000783afe3bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfb4eac8-3660-42ed-a013-2696776840d7");
        }
    }
}
