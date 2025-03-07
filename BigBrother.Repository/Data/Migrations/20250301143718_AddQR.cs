using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBrother.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddQR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QRCodeBase64",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCodeBase64",
                table: "students");
        }
    }
}
