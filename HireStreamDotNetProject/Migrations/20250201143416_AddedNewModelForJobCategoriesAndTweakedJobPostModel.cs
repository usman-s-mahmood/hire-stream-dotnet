using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HireStreamDotNetProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewModelForJobCategoriesAndTweakedJobPostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "JobPosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "JobPosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "JobCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AddedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "JobPosts");
        }
    }
}
