using MyRecipeBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Services.Storage
{
    public interface IBlobStorageService
    {
        Task Upload(User user, Stream file, string fileName);
        Task<string> GetFileUrl(User user, string fileName);
        Task Delete(User user,string fileName);
    }
}
