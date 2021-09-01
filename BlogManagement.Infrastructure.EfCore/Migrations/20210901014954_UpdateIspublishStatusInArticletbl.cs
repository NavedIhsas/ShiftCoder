using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Infrastructure.EfCore.Migrations
{
    public partial class UpdateIspublishStatusInArticletbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublish",
                table: "Articles",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublish",
                table: "Articles");
        }
    }
}
