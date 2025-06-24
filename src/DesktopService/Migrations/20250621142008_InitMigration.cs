using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesktopService.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "foreign_sentence_id_seq");

            migrationBuilder.CreateTable(
                name: "ForeignSentences",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('foreign_sentence_id_seq')"),
                    sentence = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignSentences", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForeignSentences");

            migrationBuilder.DropSequence(
                name: "foreign_sentence_id_seq");
        }
    }
}
