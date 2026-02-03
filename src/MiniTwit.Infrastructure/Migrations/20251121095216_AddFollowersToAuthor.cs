using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniTwit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFollowersToAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Following",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Following",
                table: "AspNetUsers");
        }
    }
}
