using Microsoft.EntityFrameworkCore.Migrations;

namespace Lista10.Migrations
{
    public partial class AddArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Articles",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Articles",
                newName: "ArticleID");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                newName: "IX_Articles_CategoryID");

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleID", "CategoryID", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 2, null, "Black Jeans", 99.989999999999995 },
                    { 2, 1, null, "Red Lipstick", 20.0 },
                    { 4, 2, null, "Baseball Cap", 23.190000000000001 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[] { 3, "Flowers" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleID", "CategoryID", "Image", "Name", "Price" },
                values: new object[] { 3, 3, null, "Rose", 12.5 });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Categories_CategoryID",
                table: "Articles",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Categories_CategoryID",
                table: "Articles");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Articles",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ArticleID",
                table: "Articles",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_CategoryID",
                table: "Articles",
                newName: "IX_Articles_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
