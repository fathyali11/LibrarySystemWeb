using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibrarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedReviewsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "reviews:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "reviews:getAll", "aeeb9374-30dd-4e80-9feb-000783afe3bc" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "reviews:add", "aeeb9374-30dd-4e80-9feb-000783afe3bc" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "reviews:update", "aeeb9374-30dd-4e80-9feb-000783afe3bc" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "reviews:delete", "aeeb9374-30dd-4e80-9feb-000783afe3bc" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45,
                column: "ClaimValue",
                value: "books:get");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46,
                column: "ClaimValue",
                value: "borrowedBooks:return");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47,
                column: "ClaimValue",
                value: "categories:get");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48,
                column: "ClaimValue",
                value: "authors:get");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49,
                column: "ClaimValue",
                value: "carts:get");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50,
                column: "ClaimValue",
                value: "carts:clear");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51,
                column: "ClaimValue",
                value: "carts:operation");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "orders:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "orders:create", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "orders:update", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "orders:delete", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "payments:create", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "reviews:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "reviews:add", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "reviews:update", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "reviews:delete", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 61,
                column: "ClaimValue",
                value: "books:get");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 62,
                column: "ClaimValue",
                value: "books:create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 63,
                column: "ClaimValue",
                value: "books:update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 64,
                column: "ClaimValue",
                value: "books:delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 65,
                column: "ClaimValue",
                value: "borrowedBooks:get");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 66, "permissions", "borrowedBooks:return", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 67, "permissions", "categories:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 68, "permissions", "authors:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 69, "permissions", "carts:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 70, "permissions", "carts:add", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 71, "permissions", "carts:remove", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 72, "permissions", "orders:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 73, "permissions", "orders:update", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 74, "permissions", "payments:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 75, "permissions", "reviews:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 76, "permissions", "reviews:getAll", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 77, "permissions", "reviews:add", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 78, "permissions", "reviews:update", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 79, "permissions", "reviews:delete", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "books:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "borrowedBooks:return", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "categories:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "authors:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "carts:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45,
                column: "ClaimValue",
                value: "carts:clear");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46,
                column: "ClaimValue",
                value: "carts:operation");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47,
                column: "ClaimValue",
                value: "orders:get");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48,
                column: "ClaimValue",
                value: "orders:create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49,
                column: "ClaimValue",
                value: "orders:update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50,
                column: "ClaimValue",
                value: "orders:delete");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51,
                column: "ClaimValue",
                value: "payments:create");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "books:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "books:create", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "books:update", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "books:delete", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "borrowedBooks:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "borrowedBooks:return", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "categories:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "authors:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "carts:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 61,
                column: "ClaimValue",
                value: "carts:add");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 62,
                column: "ClaimValue",
                value: "carts:remove");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 63,
                column: "ClaimValue",
                value: "orders:get");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 64,
                column: "ClaimValue",
                value: "orders:update");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 65,
                column: "ClaimValue",
                value: "payments:get");
        }
    }
}
