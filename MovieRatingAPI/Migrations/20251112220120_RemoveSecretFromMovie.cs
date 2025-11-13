using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRatingAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSecretFromMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secret",
                table: "Movies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "Movies",
                type: "text",
                nullable: true);
        }
    }
}
