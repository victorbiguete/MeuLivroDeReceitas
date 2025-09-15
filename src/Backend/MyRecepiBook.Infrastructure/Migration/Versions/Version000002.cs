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
        public override void Up()
        {
            CreateTable("Recipes")
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("CookingTime").AsInt32().Nullable()
                .WithColumn("Difficulty").AsInt32().Nullable()
                .WithColumn("UserId").AsInt64().ForeignKey("FK_Recipe_User_Id","Users", "Id").NotNullable();
                
        }
    }
}
