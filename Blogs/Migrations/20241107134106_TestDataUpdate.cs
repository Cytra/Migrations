using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogs.Migrations
{
    /// <inheritdoc />
    public partial class TestDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TestData",
                keyColumn: "TestResult",
                keyValue: null,
                column: "TestResult",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TestResult",
                table: "TestData",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TestResult",
                table: "TestData",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
