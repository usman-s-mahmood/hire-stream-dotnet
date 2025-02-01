using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HireStreamDotNetProject.Migrations
{
    /// <inheritdoc />
    public partial class FixedNameAttrInJobCatModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "JobCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "JobCategories");
        }
    }
}
