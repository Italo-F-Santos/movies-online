using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesOnline.Migrations
{
    public partial class AlteracaoNomes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Users",
                newName: "UserMiddleName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Titles",
                newName: "TitleName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserMiddleName",
                table: "Users",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "TitleName",
                table: "Titles",
                newName: "Name");
        }
    }
}
