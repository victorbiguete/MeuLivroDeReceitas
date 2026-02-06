using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Migration.Versions
{
    [Migration(DatabaseVersions.TABLE_RECIPES, "Create table to save the recipes' information")]
    public class Version000002 : VersionBase
    {
        private const string RECIPE_TABLE_NAME = "Recipe";
        public override void Up()
        {
            CreateTable(RECIPE_TABLE_NAME)
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("CookingTime").AsInt32().Nullable()
                .WithColumn("Difficulty").AsInt32().Nullable()
                .WithColumn("UserId").AsInt64().ForeignKey("FK_Recipe_User_Id","Users", "Id").NotNullable();

            CreateTable("Ingredients")
                .WithColumn("Item").AsString().NotNullable()
                .WithColumn("RecipesId").AsInt64().NotNullable().ForeignKey("FK_Ingredient_Recipe_Id", RECIPE_TABLE_NAME, "Id").OnDelete(System.Data.Rule.Cascade);

            CreateTable("Instructions")
                .WithColumn("Step").AsInt32().NotNullable()
                .WithColumn("Text").AsString(2000).NotNullable()
                .WithColumn("RecipesId").AsInt64().NotNullable().ForeignKey("FK_Instruction_Recipe_Id", RECIPE_TABLE_NAME, "Id").OnDelete(System.Data.Rule.Cascade);

            CreateTable("DishTypes")
                .WithColumn("Type").AsInt32().NotNullable()
                .WithColumn("RecipesId").AsInt64().NotNullable().ForeignKey("FK_DishType_Recipe_Id", RECIPE_TABLE_NAME, "Id").OnDelete(System.Data.Rule.Cascade);
        }
    }
}
