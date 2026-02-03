using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniTwit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedlikedByToCheeps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LikedBy",
                table: "Cheeps",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikedBy",
                table: "Cheeps");
        }
    }
}
