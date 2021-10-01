using Microsoft.EntityFrameworkCore.Migrations;

namespace CommentManagement.Infrastructure.EfCore.Migrations
{
    public partial class addButtonLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ButtonLink",
                table: "Sliders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButtonLink",
                table: "Sliders");
        }
    }
}
