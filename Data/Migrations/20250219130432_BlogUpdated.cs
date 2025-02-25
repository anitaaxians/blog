using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagementAPI.Data
{
    /// <inheritdoc />
    public partial class BlogUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Blogs");
        }
    }
}
