using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HireStreamDotNetProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserModelForProfileAttrs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutUser",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SocialLink",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SocialLink",
                table: "Users");
        }
    }
}
