using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBrother.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class Editphonenumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<long>(
                name: "PhoneNumber",
                table: "students",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "students",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

           
        }
    }
}
