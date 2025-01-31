using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HireStreamDotNetProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedJobPostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobType",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "JobPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "JobPosts",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobType",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "JobPosts");
        }
    }
}
