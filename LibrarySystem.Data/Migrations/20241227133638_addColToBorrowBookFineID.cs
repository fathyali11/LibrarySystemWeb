using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addColToBorrowBookFineID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_BorrowedBooks_BorrowedBookId",
                table: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_Fines_BorrowedBookId",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "BorrowedBookId",
                table: "Fines");

            migrationBuilder.AddColumn<int>(
                name: "FineId",
                table: "BorrowedBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fines_BorrowBookId",
                table: "Fines",
                column: "BorrowBookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_BorrowedBooks_BorrowBookId",
                table: "Fines",
                column: "BorrowBookId",
                principalTable: "BorrowedBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_BorrowedBooks_BorrowBookId",
                table: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_Fines_BorrowBookId",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "FineId",
                table: "BorrowedBooks");

            migrationBuilder.AddColumn<int>(
                name: "BorrowedBookId",
                table: "Fines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fines_BorrowedBookId",
                table: "Fines",
                column: "BorrowedBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_BorrowedBooks_BorrowedBookId",
                table: "Fines",
                column: "BorrowedBookId",
                principalTable: "BorrowedBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
