using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBrother.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AsisstantId",
                table: "attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "asisstants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asisstants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_asisstants_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendances_AsisstantId",
                table: "attendances",
                column: "AsisstantId");

            migrationBuilder.CreateIndex(
                name: "IX_asisstants_CourseId",
                table: "asisstants",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_asisstants_AsisstantId",
                table: "attendances",
                column: "AsisstantId",
                principalTable: "asisstants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_asisstants_AsisstantId",
                table: "attendances");

            migrationBuilder.DropTable(
                name: "asisstants");

            migrationBuilder.DropIndex(
                name: "IX_attendances_AsisstantId",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "AsisstantId",
                table: "attendances");
        }
    }
}
