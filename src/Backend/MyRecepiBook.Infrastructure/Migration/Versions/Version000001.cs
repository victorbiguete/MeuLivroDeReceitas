using FluentMigrator;
using FluentMigrator.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Migration.Versions
{
    [Migration(DatabaseVersions.TABLE_USER,"Create table to save the User's information")]
    public class Version000001 : VersionBase
    {
        public override void Up()
        {
                CreateTable("Users")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable();
        }
    }
}
