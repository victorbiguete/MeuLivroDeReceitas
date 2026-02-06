using AutoMapper;
using CommomTestsUtilities.IdEncryption;
using MyRecipeBook.Application.Services.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Mapper
{
    public static class MapperBuilder
    {
        public static IMapper Build()
        {
            var idEncripter = IdEncripterBuilder.Build();

            return new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping(idEncripter));
            }).CreateMapper();
        }
    }
}
