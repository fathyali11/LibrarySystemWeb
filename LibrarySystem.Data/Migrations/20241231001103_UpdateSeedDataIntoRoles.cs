using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataIntoRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfb4eac8-3660-42ed-a013-2696776840d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aeeb9374-30dd-4e80-9feb-000783afe3bc",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsMember", "Name", "NormalizedName" },
                values: new object[] { "4b88c1d0-4256-4bb7-89d5-c0870ebeb512", "a35b1f95-30e6-4672-a3ce-e79528a4ccb8", false, "Seller", "SELLER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37597bc6-ffee-45d1-ad20-b53b66651c86",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOyLux7x01syagTROReU+2NIqlp5qX4NgEBtMuDAo+MNSepIsQ1VLxYZ0YzAeWL+Zw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "db87724c-6e28-4a90-a1fd-3d8fe88475e6",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPklm/Ltfr+4YA6KCMrEy5jzHaBjk8O36UZjV2UMVTO+leHN8pBEMVJrNAOsZ+868g==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b88c1d0-4256-4bb7-89d5-c0870ebeb512");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aeeb9374-30dd-4e80-9feb-000783afe3bc",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsMember", "Name", "NormalizedName" },
                values: new object[] { "dfb4eac8-3660-42ed-a013-2696776840d7", "d22530c5-5dec-4343-b16a-907ef4106029", false, "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37597bc6-ffee-45d1-ad20-b53b66651c86",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECp0CblxCfS3TWQasHMR4h/y5EzJBzByOUrsqA/AHQQJq6flZwscPgG5ibpjOuu/5g==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "db87724c-6e28-4a90-a1fd-3d8fe88475e6",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPk8Tp9pPMRjvVZsECPtKrAXV/6xr0RUk/hrgb24NM8WcUEXdyNeLPOj9cRbeZAWuQ==");
        }
    }
}
