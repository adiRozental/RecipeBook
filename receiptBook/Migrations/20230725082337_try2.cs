using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace receiptBook.Migrations
{
    public partial class try2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_recipes_RecipeId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_recipes_RecipeId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_recipes_RecipeId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageDates_recipes_RecipeId",
                table: "UsageDates");

            migrationBuilder.DropIndex(
                name: "IX_UsageDates_RecipeId",
                table: "UsageDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipes",
                table: "recipes");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RecipeId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Images_RecipeId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Comments_RecipeId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "recipes",
                newName: "Recipes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "recipes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipes",
                table: "recipes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsageDates_RecipeId",
                table: "UsageDates",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RecipeId",
                table: "Ratings",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RecipeId",
                table: "Images",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RecipeId",
                table: "Comments",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_recipes_RecipeId",
                table: "Comments",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_recipes_RecipeId",
                table: "Images",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_recipes_RecipeId",
                table: "Ratings",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageDates_recipes_RecipeId",
                table: "UsageDates",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
