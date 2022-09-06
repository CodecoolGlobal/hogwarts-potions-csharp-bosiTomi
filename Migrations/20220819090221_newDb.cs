using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogwartsPotions.Migrations
{
    public partial class newDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Potions_Potionid",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_Recipeid",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_Potionid",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_Recipeid",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Potionid",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Recipeid",
                table: "Ingredients");

            migrationBuilder.AddColumn<byte>(
                name: "BrewingStatus",
                table: "Potions",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "IngredientPotion",
                columns: table => new
                {
                    Ingredientsid = table.Column<long>(type: "bigint", nullable: false),
                    Potionsid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPotion", x => new { x.Ingredientsid, x.Potionsid });
                    table.ForeignKey(
                        name: "FK_IngredientPotion_Ingredients_Ingredientsid",
                        column: x => x.Ingredientsid,
                        principalTable: "Ingredients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientPotion_Potions_Potionsid",
                        column: x => x.Potionsid,
                        principalTable: "Potions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRecipe",
                columns: table => new
                {
                    Ingredientsid = table.Column<long>(type: "bigint", nullable: false),
                    Recipesid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecipe", x => new { x.Ingredientsid, x.Recipesid });
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Ingredients_Ingredientsid",
                        column: x => x.Ingredientsid,
                        principalTable: "Ingredients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Recipes_Recipesid",
                        column: x => x.Recipesid,
                        principalTable: "Recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPotion_Potionsid",
                table: "IngredientPotion",
                column: "Potionsid");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipe_Recipesid",
                table: "IngredientRecipe",
                column: "Recipesid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientPotion");

            migrationBuilder.DropTable(
                name: "IngredientRecipe");

            migrationBuilder.DropColumn(
                name: "BrewingStatus",
                table: "Potions");

            migrationBuilder.AddColumn<long>(
                name: "Potionid",
                table: "Ingredients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Recipeid",
                table: "Ingredients",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Potionid",
                table: "Ingredients",
                column: "Potionid");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Recipeid",
                table: "Ingredients",
                column: "Recipeid");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Potions_Potionid",
                table: "Ingredients",
                column: "Potionid",
                principalTable: "Potions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_Recipeid",
                table: "Ingredients",
                column: "Recipeid",
                principalTable: "Recipes",
                principalColumn: "id");
        }
    }
}
