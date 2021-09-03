using Microsoft.EntityFrameworkCore.Migrations;

namespace CommentManagement.Infrastructure.EfCore.Migrations
{
    public partial class UpdateCommentTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PrId",
                table: "Comments",
                type: "bigint",
                nullable: true);
        }
    }
}
