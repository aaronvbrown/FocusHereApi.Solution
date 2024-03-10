using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusHereApi.Migrations
{
    public partial class addCanvasIdtoCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CanvasId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CanvasId", "Name", "Period", "Teacher" },
                values: new object[] { 1, 0, "Math", 1, "Mr. Smith" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CanvasId", "Name", "Period", "Teacher" },
                values: new object[] { 2, 0, "Science", 2, "Mrs. Johnson" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CanvasId", "Name", "Period", "Teacher" },
                values: new object[] { 3, 0, "History", 3, "Mr. Johnson" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "CanvasId",
                table: "Courses");
        }
    }
}
