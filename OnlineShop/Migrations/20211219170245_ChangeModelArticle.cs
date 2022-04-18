using Microsoft.EntityFrameworkCore.Migrations;

namespace Lista10.Migrations
{
    public partial class ChangeModelArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Articles",
                newName: "ImageUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Articles",
                newName: "Image");
        }
    }
}
