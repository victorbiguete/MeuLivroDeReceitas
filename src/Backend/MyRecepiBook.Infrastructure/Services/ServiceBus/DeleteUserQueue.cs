using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Services.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Services.ServiceBus
{
    public class DeleteUserQueue : IDeleteUserQueue
    {
        public Task SendMessage(User user)
        {
            throw new NotImplementedException();
        }
    }
}
