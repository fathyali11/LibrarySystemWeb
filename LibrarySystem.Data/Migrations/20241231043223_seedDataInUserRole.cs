using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibrarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedDataInUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4b88c1d0-4256-4bb7-89d5-c0870ebeb512", "37597bc6-ffee-45d1-ad20-b53b66651c86" },
                    { "aeeb9374-30dd-4e80-9feb-000783afe3bc", "db87724c-6e28-4a90-a1fd-3d8fe88475e6" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4b88c1d0-4256-4bb7-89d5-c0870ebeb512", "37597bc6-ffee-45d1-ad20-b53b66651c86" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "aeeb9374-30dd-4e80-9feb-000783afe3bc", "db87724c-6e28-4a90-a1fd-3d8fe88475e6" });
        }
    }
}
