using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBrother.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class Countattend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseAttendace",
                table: "studentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "students");

            migrationBuilder.DropColumn(
                name: "CourseAttendace",
                table: "studentCourses");
        }
    }
}
