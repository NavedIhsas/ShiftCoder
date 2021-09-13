using Microsoft.EntityFrameworkCore.Migrations;

namespace CommentManagement.Infrastructure.EfCore.Migrations
{
    public partial class UpdateVisitTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RecordOwnerId",
                table: "Visits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordOwnerId",
                table: "Visits");
        }
    }
}
