using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieLibrary.Data.Migrations
{
    public partial class ChangesToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategories_Categories_CategoryId",
                table: "MovieCategories");

            migrationBuilder.EnsureSchema(
                name: "MovieLibrary");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "Movies",
                newSchema: "MovieLibrary");

            migrationBuilder.RenameTable(
                name: "MovieCategories",
                newName: "MovieCategories",
                newSchema: "MovieLibrary");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories",
                newSchema: "MovieLibrary");

            migrationBuilder.AlterDatabase(
                collation: "NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "MovieLibrary",
                table: "Movies",
                type: "TEXT",
                maxLength: 150,
                nullable: true,
                collation: "NOCASE",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ImdbRating",
                schema: "MovieLibrary",
                table: "Movies",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Title",
                schema: "MovieLibrary",
                table: "Movies",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "MovieLibrary",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategories_Categories_CategoryId",
                schema: "MovieLibrary",
                table: "MovieCategories",
                column: "CategoryId",
                principalSchema: "MovieLibrary",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategories_Categories_CategoryId",
                schema: "MovieLibrary",
                table: "MovieCategories");

            migrationBuilder.DropIndex(
                name: "IX_Movies_Title",
                schema: "MovieLibrary",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                schema: "MovieLibrary",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Movies",
                schema: "MovieLibrary",
                newName: "Movies");

            migrationBuilder.RenameTable(
                name: "MovieCategories",
                schema: "MovieLibrary",
                newName: "MovieCategories");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "MovieLibrary",
                newName: "Categories");

            migrationBuilder.AlterDatabase(
                oldCollation: "NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Movies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 150,
                oldNullable: true,
                oldCollation: "NOCASE");

            migrationBuilder.AlterColumn<decimal>(
                name: "ImdbRating",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategories_Categories_CategoryId",
                table: "MovieCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
