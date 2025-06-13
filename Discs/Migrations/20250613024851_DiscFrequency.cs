using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discs.Migrations
{
    /// <inheritdoc />
    public partial class DiscFrequency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesUsed",
                table: "Discs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesUsed",
                table: "Discs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
