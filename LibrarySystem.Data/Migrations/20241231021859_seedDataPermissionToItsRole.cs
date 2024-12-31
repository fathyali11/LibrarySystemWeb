using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibrarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedDataPermissionToItsRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "books:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 2, "permissions", "books:create", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 3, "permissions", "books:update", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 4, "permissions", "books:delete", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 5, "permissions", "fines:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 6, "permissions", "fines:create", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 7, "permissions", "fines:update", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 8, "permissions", "fines:delete", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 9, "permissions", "borrowedBooks:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 10, "permissions", "borrowedBooks:create", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 11, "permissions", "borrowedBooks:return", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 12, "permissions", "borrowedBooks:delete", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 13, "permissions", "categories:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 14, "permissions", "categories:create", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 15, "permissions", "categories:update", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 16, "permissions", "categories:delete", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 17, "permissions", "authors:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 18, "permissions", "authors:create", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 19, "permissions", "authors:update", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 20, "permissions", "authors:delete", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 21, "permissions", "carts:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 22, "permissions", "carts:add", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 23, "permissions", "carts:remove", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 24, "permissions", "orders:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 25, "permissions", "orders:create", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 26, "permissions", "orders:update", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 27, "permissions", "orders:delete", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 28, "permissions", "payments:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 29, "permissions", "payments:create", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 30, "permissions", "payments:update", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 31, "permissions", "payments:delete", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 32, "permissions", "users:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 33, "permissions", "users:create", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 34, "permissions", "users:update", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 35, "permissions", "users:delete", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 36, "permissions", "roles:get", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 37, "permissions", "roles:create", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 38, "permissions", "roles:update", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 39, "permissions", "roles:delete", "aeeb9374-30dd-4e80-9feb-000783afe3bc" },
                    { 40, "permissions", "books:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 41, "permissions", "borrowedBooks:return", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 42, "permissions", "categories:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 43, "permissions", "authors:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 44, "permissions", "carts:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 45, "permissions", "carts:clear", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 46, "permissions", "carts:operation", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 47, "permissions", "orders:create", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 48, "permissions", "orders:cancel", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 49, "permissions", "orders:get", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 50, "permissions", "payments:create", "4a666d8a-96ae-45c5-9882-a1e043fdf49e" },
                    { 51, "permissions", "books:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 52, "permissions", "books:create", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 53, "permissions", "books:update", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 54, "permissions", "books:delete", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 55, "permissions", "borrowedBooks:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 56, "permissions", "borrowedBooks:return", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 57, "permissions", "categories:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 58, "permissions", "authors:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 59, "permissions", "carts:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 60, "permissions", "carts:add", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 61, "permissions", "carts:remove", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 62, "permissions", "orders:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 63, "permissions", "orders:update", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" },
                    { 64, "permissions", "payments:get", "4b88c1d0-4256-4bb7-89d5-c0870ebeb512" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37597bc6-ffee-45d1-ad20-b53b66651c86",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPklm/Ltfr+4YA6KCMrEy5jzHaBjk8O36UZjV2UMVTO+leHN8pBEMVJrNAOsZ+868g==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37597bc6-ffee-45d1-ad20-b53b66651c86",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOyLux7x01syagTROReU+2NIqlp5qX4NgEBtMuDAo+MNSepIsQ1VLxYZ0YzAeWL+Zw==");
        }
    }
}
