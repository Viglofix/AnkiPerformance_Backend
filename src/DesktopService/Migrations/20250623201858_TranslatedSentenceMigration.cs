using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesktopService.Migrations
{
    /// <inheritdoc />
    public partial class TranslatedSentenceMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TranslatedSentence",
                table: "ForeignSentences",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TranslatedSentence",
                table: "ForeignSentences");
        }
    }
}
