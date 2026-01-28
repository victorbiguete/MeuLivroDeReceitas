using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Migration.Versions
{
    [Migration(DatabaseVersions.TABLE_REFRESH_TOKEN, ("Create table to save the refresh token"))]
    public class Version0000004 : VersionBase
    {
        public override void Up()
        {
            CreateTable("RefreshTokens")
                .WithColumn("Value").AsString().NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("Fk_RefreshTokens_User_Id", "Users", "Id");
        }
    }
}
