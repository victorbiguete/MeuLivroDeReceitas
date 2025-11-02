using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Migration
{
    public abstract record DatabaseVersions
    {
        public const int TABLE_USER = 1;
        public const int TABLE_RECIPES = 2;
        public const int IMAGES_FOR_RECIPES = 3;
    }
}
