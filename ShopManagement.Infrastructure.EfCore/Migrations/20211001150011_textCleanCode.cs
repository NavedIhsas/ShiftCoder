using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopManagement.Infrastructure.EfCore.Migrations
{
    public partial class textCleanCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderDetails_Courses_CourseId",
            //    table: "OrderDetails");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderDetails_Orders_OrderId",
            //    table: "OrderDetails");

            //migrationBuilder.DropIndex(
            //    name: "IX_OrderDetails_CourseId",
            //    table: "OrderDetails");

            //migrationBuilder.DropIndex(
            //    name: "IX_OrderDetails_OrderId",
            //    table: "OrderDetails");

            //migrationBuilder.DropColumn(
            //    name: "AccountId",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "CourseId",
            //    table: "OrderDetails");

            //migrationBuilder.DropColumn(
            //    name: "OrderId",
            //    table: "OrderDetails");

            //migrationBuilder.DropColumn(
            //    name: "Price",
            //    table: "OrderDetails");

            //migrationBuilder.AddColumn<long>(
            //    name: "CourseId1",
            //    table: "OrderDetails",
            //    type: "bigint",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "OrderId1",
            //    table: "OrderDetails",
            //    type: "bigint",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderDetails_CourseId1",
            //    table: "OrderDetails",
            //    column: "CourseId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderDetails_OrderId1",
            //    table: "OrderDetails",
            //    column: "OrderId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderDetails_Courses_CourseId1",
            //    table: "OrderDetails",
            //    column: "CourseId1",
            //    principalTable: "Courses",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderDetails_Orders_OrderId1",
            //    table: "OrderDetails",
            //    column: "OrderId1",
            //    principalTable: "Orders",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderDetails_Courses_CourseId1",
            //    table: "OrderDetails");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderDetails_Orders_OrderId1",
            //    table: "OrderDetails");

            //migrationBuilder.DropIndex(
            //    name: "IX_OrderDetails_CourseId1",
            //    table: "OrderDetails");

            //migrationBuilder.DropIndex(
            //    name: "IX_OrderDetails_OrderId1",
            //    table: "OrderDetails");

            //migrationBuilder.DropColumn(
            //    name: "CourseId1",
            //    table: "OrderDetails");

            //migrationBuilder.DropColumn(
            //    name: "OrderId1",
            //    table: "OrderDetails");

            //migrationBuilder.AddColumn<long>(
            //    name: "AccountId",
            //    table: "Orders",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AddColumn<long>(
            //    name: "CourseId",
            //    table: "OrderDetails",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AddColumn<long>(
            //    name: "OrderId",
            //    table: "OrderDetails",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AddColumn<double>(
            //    name: "Price",
            //    table: "OrderDetails",
            //    type: "float",
            //    nullable: false,
            //    defaultValue: 0.0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderDetails_CourseId",
            //    table: "OrderDetails",
            //    column: "CourseId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderDetails_OrderId",
            //    table: "OrderDetails",
            //    column: "OrderId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderDetails_Courses_CourseId",
            //    table: "OrderDetails",
            //    column: "CourseId",
            //    principalTable: "Courses",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderDetails_Orders_OrderId",
            //    table: "OrderDetails",
            //    column: "OrderId",
            //    principalTable: "Orders",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
