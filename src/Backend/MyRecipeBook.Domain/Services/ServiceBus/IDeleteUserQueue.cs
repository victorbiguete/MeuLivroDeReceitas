using MyRecipeBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Services.ServiceBus
{
    public interface IDeleteUserQueue
    {
        Task SendMessage(User user);
    }
}
