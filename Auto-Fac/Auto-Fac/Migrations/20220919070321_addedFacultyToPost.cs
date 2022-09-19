using Microsoft.EntityFrameworkCore.Migrations;

namespace Auto_Fac.Migrations
{
    public partial class addedFacultyToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idFaculty",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idFaculty",
                table: "Posts");
        }
    }
}
