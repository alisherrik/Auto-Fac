using Microsoft.EntityFrameworkCore.Migrations;

namespace Auto_Fac.Migrations
{
    public partial class addedCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idCourse",
                table: "LessonSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idCourse",
                table: "LessonSchedule");
        }
    }
}
