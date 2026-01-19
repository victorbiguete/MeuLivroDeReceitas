using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Services.ServiceBus
{
    public class DeleteUserProcessor
    {
        private readonly ServiceBusProcessor _processor;

        public DeleteUserProcessor(ServiceBusProcessor processor)
        {
            _processor = processor;
        }

        public ServiceBusProcessor GetProcessor() => _processor;
    }
}
