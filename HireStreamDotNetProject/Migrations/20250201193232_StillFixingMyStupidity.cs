using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HireStreamDotNetProject.Migrations
{
    /// <inheritdoc />
    public partial class StillFixingMyStupidity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobCategory_Users_UserId",
                table: "JobCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_JobCategory_CategoryId",
                table: "JobPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategory",
                table: "JobCategory");

            migrationBuilder.RenameTable(
                name: "JobCategory",
                newName: "JobCategories");

            migrationBuilder.RenameIndex(
                name: "IX_JobCategory_UserId",
                table: "JobCategories",
                newName: "IX_JobCategories_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategories",
                table: "JobCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobCategories_Users_UserId",
                table: "JobCategories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_JobCategories_CategoryId",
                table: "JobPosts",
                column: "CategoryId",
                principalTable: "JobCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobCategories_Users_UserId",
                table: "JobCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_JobCategories_CategoryId",
                table: "JobPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategories",
                table: "JobCategories");

            migrationBuilder.RenameTable(
                name: "JobCategories",
                newName: "JobCategory");

            migrationBuilder.RenameIndex(
                name: "IX_JobCategories_UserId",
                table: "JobCategory",
                newName: "IX_JobCategory_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategory",
                table: "JobCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobCategory_Users_UserId",
                table: "JobCategory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_JobCategory_CategoryId",
                table: "JobPosts",
                column: "CategoryId",
                principalTable: "JobCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
