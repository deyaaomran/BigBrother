using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBrother.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateStudentdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "students");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "students");

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "students",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "students");

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PhoneNumber",
                table: "students",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
