using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagementAPI.Data
{
    /// <inheritdoc />
    public partial class AddShortContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortContent",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortContent",
                table: "Blog");
        }
    }
}
