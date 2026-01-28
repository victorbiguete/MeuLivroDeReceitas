using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.OpenAI;
using MyRecipeBook.Domain.Services.ServiceBus;
using MyRecipeBook.Domain.Services.Storage;
using MyRecipeBook.Domain.ValueObjects;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using MyRecipeBook.Infrastructure.Extensions;
using MyRecipeBook.Infrastructure.Security.Cryptography;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Generator;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Validator;
using MyRecipeBook.Infrastructure.Services.LoggedUser;
using MyRecipeBook.Infrastructure.Services.OpenAI;
using MyRecipeBook.Infrastructure.Services.ServiceBus;
using MyRecipeBook.Infrastructure.Services.Storage;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Reflection;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepository(services);
            AddToken(services, configuration);
            AddLoggedUser(services);
            AddPasswordEncrypter(services);
            if (configuration.IsUnitTestEnviroment())
                return;
            
            AddDbContext_SqlServer(services,configuration);
            AddFluentMigrator(services,configuration);
            AddOpenAi(services,configuration);
            AddAzureStorage(services,configuration);
            AddQueue(services,configuration);
        }
        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.ConnectionString());
            }); 
        }
        private static void AddRepository(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
            services.AddScoped<IUserDeleteOnlyRepository, UserRepository>();
            services.AddScoped<IRecipeWriteOnlyRepository,RecipeRepository>();
            services.AddScoped<IRecipeReadOnlyRepository, RecipeRepository>();
            services.AddScoped<IRecipeUpdateOnlyRepository, RecipeRepository>();
            
        }

        private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options.AddSqlServer()
                .WithGlobalConnectionString(configuration.ConnectionString())
                .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
            });
        }

        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigninKey");

            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));

            services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
        }

        private static void AddLoggedUser(IServiceCollection services)
        {
            services.AddScoped<ILoggedUser, LoggedUser>();
        }

        private static void AddPasswordEncrypter(IServiceCollection services)
        {
            services.AddScoped<IPasswordEncripter,BCryptNet>();
        }

        private static void AddOpenAi(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGenerateRecipeAI, ChatGPTService>();

            var key = configuration.GetValue<string>("Settings:OpenAi:ApiKey");

            services.AddScoped(c => new ChatClient(MyRecipeBookRuleConstants.CHAT_MODEL_GPT,key));
        }

        private static void AddAzureStorage(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Settings:BlobStorage:Azure");

            if(!string.IsNullOrEmpty(connectionString))
            services.AddScoped<IBlobStorageService>(c => new AzureStorageService(new BlobServiceClient(connectionString)));
        }

        private static void AddQueue(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Settings:ServiceBus:DeleteUserAccount");

            if (!string.IsNullOrEmpty(connectionString))
                return;

            //criação do Client
            var client = new ServiceBusClient(connectionString, new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            });
            //classe de criação de mensagem
            var deleteQueue = new DeleteUserQueue(client.CreateSender("user"));

            //classe de recebimento das Mensagens
            var deleteUserProcessor = new DeleteUserProcessor(client.CreateProcessor("user",new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1
            }));

            services.AddSingleton(deleteUserProcessor);
            services.AddScoped<IDeleteUserQueue>(options => deleteQueue);
        }
    }
}
