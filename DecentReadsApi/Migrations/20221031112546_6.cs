using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecentReadsApi.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AvgRate",
                table: "Books",
                type: "float",
                nullable: false,
                computedColumnSql: "dbo.CalcAvgRatingByBookId([Id])");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgRate",
                table: "Books");
        }
    }
}
