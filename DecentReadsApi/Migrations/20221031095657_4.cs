using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecentReadsApi.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddedBy",
                table: "FavoriteBooks",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteBooks_BookId",
                table: "FavoriteBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteBooks_UserId",
                table: "FavoriteBooks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteBooks_Books_BookId",
                table: "FavoriteBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteBooks_Users_UserId",
                table: "FavoriteBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteBooks_Books_BookId",
                table: "FavoriteBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteBooks_Users_UserId",
                table: "FavoriteBooks");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteBooks_BookId",
                table: "FavoriteBooks");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteBooks_UserId",
                table: "FavoriteBooks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoriteBooks",
                newName: "AddedBy");
        }
    }
}
