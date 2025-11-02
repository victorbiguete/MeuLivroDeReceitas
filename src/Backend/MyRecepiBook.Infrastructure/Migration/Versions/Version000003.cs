using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Migration.Versions
{
    [Migration(DatabaseVersions.IMAGES_FOR_RECIPES, "Add collumn on recipe table to save images")]
    public class Version000003 : VersionBase
    {
        public override void Up()
        {
            Alter.Table("Recipes").AddColumn("ImageIdentifier").AsString().Nullable();
        }
    }
}
