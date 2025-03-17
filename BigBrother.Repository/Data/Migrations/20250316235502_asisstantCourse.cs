using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBrother.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class asisstantCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asisstants_courses_CourseId",
                table: "asisstants");

            migrationBuilder.DropIndex(
                name: "IX_asisstants_CourseId",
                table: "asisstants");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "asisstants");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "asisstants");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "asisstants");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "asisstants");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "asisstants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "AsisstantCourse",
                columns: table => new
                {
                    asisstantsId = table.Column<int>(type: "int", nullable: false),
                    coursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsisstantCourse", x => new { x.asisstantsId, x.coursesId });
                    table.ForeignKey(
                        name: "FK_AsisstantCourse_asisstants_asisstantsId",
                        column: x => x.asisstantsId,
                        principalTable: "asisstants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsisstantCourse_courses_coursesId",
                        column: x => x.coursesId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asisstantCourses",
                columns: table => new
                {
                    AsisstantId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asisstantCourses", x => new { x.AsisstantId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_asisstantCourses_asisstants_AsisstantId",
                        column: x => x.AsisstantId,
                        principalTable: "asisstants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_asisstantCourses_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsisstantCourse_coursesId",
                table: "AsisstantCourse",
                column: "coursesId");

            migrationBuilder.CreateIndex(
                name: "IX_asisstantCourses_CourseId",
                table: "asisstantCourses",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsisstantCourse");

            migrationBuilder.DropTable(
                name: "asisstantCourses");

            migrationBuilder.AlterColumn<long>(
                name: "PhoneNumber",
                table: "asisstants",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "asisstants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "asisstants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "asisstants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "asisstants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_asisstants_CourseId",
                table: "asisstants",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_asisstants_courses_CourseId",
                table: "asisstants",
                column: "CourseId",
                principalTable: "courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
