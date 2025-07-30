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
    public class Version000001 : ForwardOnlyMigration
    {
        public override void Up()
        {
            throw new NotImplementedException();
        }
    }
}
