﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopManagement.Infrastructure.EfCore.Migrations
{
    public partial class CreateCourseGroupAndCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRemove = table.Column<bool>(type: "bit", nullable: false),
                    KeyWords = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseLevels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    File = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PictureAlt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PictureTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KeyWords = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourseGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CourseLevelId = table.Column<long>(type: "bigint", nullable: false),
                    CourseStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseGroups_CourseGroupId",
                        column: x => x.CourseGroupId,
                        principalTable: "CourseGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CourseLevels_CourseLevelId",
                        column: x => x.CourseLevelId,
                        principalTable: "CourseLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_CourseStatus_CourseStatusId",
                        column: x => x.CourseStatusId,
                        principalTable: "CourseStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CourseLevels",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1L, "مقدماتی" },
                    { 2L, "متوسط" },
                    { 3L, "پیشرفته" }
                });

            migrationBuilder.InsertData(
                table: "CourseStatus",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1L, "در حال برگذاری" },
                    { 2L, "تشکیل شده" },
                    { 3L, "اتمام" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseGroupId",
                table: "Courses",
                column: "CourseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseLevelId",
                table: "Courses",
                column: "CourseLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseStatusId",
                table: "Courses",
                column: "CourseStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "CourseGroups");

            migrationBuilder.DropTable(
                name: "CourseLevels");

            migrationBuilder.DropTable(
                name: "CourseStatus");
        }
    }
}
