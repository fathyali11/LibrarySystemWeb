using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibrarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedDataIntoUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "37597bc6-ffee-45d1-ad20-b53b66651c86", 0, "Mansoura", "0955ece4-848c-4929-9bf1-dcd548d93f97", "man.8man010099@gmail.com", true, "Fathy", true, "Ali", false, null, "MAN.8MAN010099@GMAIL.COM", "SELLER", "AQAAAAIAAYagAAAAECp0CblxCfS3TWQasHMR4h/y5EzJBzByOUrsqA/AHQQJq6flZwscPgG5ibpjOuu/5g==", "01556788707", false, "94288f43-d8d2-4f7a-ba4d-d6893b2ad1fe", false, "Seller" },
                    { "db87724c-6e28-4a90-a1fd-3d8fe88475e6", 0, "Mansoura", "6a4590fd-cddc-4eed-b1c8-e01ddce5c064", "fathy.ali8ali@gmail.com", true, "Fathy", true, "Ali", false, null, "FATHY.ALI8ALI@GMAIL.COM", "MANAGER", "AQAAAAIAAYagAAAAEPk8Tp9pPMRjvVZsECPtKrAXV/6xr0RUk/hrgb24NM8WcUEXdyNeLPOj9cRbeZAWuQ==", "01009927286", false, "c5461f60-2df9-4235-a040-0ffcfb579c41", false, "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37597bc6-ffee-45d1-ad20-b53b66651c86");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "db87724c-6e28-4a90-a1fd-3d8fe88475e6");
        }
    }
}
