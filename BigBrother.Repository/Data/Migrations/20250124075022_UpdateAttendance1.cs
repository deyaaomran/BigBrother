using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBrother.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttendance1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_students_StudentId",
                table: "attendances");

            migrationBuilder.DropIndex(
                name: "IX_attendances_StudentId",
                table: "attendances");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "attendances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "attendances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "attendances",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_attendances_StudentId_CourseId",
                table: "attendances",
                columns: new[] { "StudentId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_studentCourses_StudentId_CourseId",
                table: "attendances",
                columns: new[] { "StudentId", "CourseId" },
                principalTable: "studentCourses",
                principalColumns: new[] { "StudentId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_students_StudentId",
                table: "attendances",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_studentCourses_StudentId_CourseId",
                table: "attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_attendances_students_StudentId",
                table: "attendances");

            migrationBuilder.DropIndex(
                name: "IX_attendances_StudentId_CourseId",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "attendances");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "attendances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_attendances_StudentId",
                table: "attendances",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_students_StudentId",
                table: "attendances",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
