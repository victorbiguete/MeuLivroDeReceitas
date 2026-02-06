using Azure.Messaging.ServiceBus;
using MyRecipeBook.Application.UseCases.User.Delete.Delete;
using MyRecipeBook.Infrastructure.Services.ServiceBus;

namespace MyRecipeBook.API.BackgroundServices
{
    public class DeleteUserService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ServiceBusProcessor _processor;

        public DeleteUserService(IServiceProvider services, DeleteUserProcessor processor)
        {
            _services = services;
            _processor = processor.GetProcessor();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += ProcessMessageAsync;

            _processor.ProcessErrorAsync += ExceptionReceivedhandler;

            await _processor.StartProcessingAsync(stoppingToken);
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs eventArgs)
        {
            var message = eventArgs.Message.Body.ToString();

            var userIdentifier = Guid.Parse(message);

            var scope = _services.CreateScope();

            var deleteUserCase = scope.ServiceProvider.GetRequiredService<IDeleteUserAccountUseCase>();

            await deleteUserCase.Execute(userIdentifier);
        }

        private Task ExceptionReceivedhandler(ProcessErrorEventArgs _) => Task.CompletedTask;

        ~DeleteUserService() => Dispose();

        public override void Dispose()
        {
            base.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
