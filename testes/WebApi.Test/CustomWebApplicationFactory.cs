using CommomTestsUtilities.Entities;
using CommomTestsUtilities.IdEncryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enum;
using MyRecipeBook.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private MyRecipeBook.Domain.Entities.User _user = default!;
        private MyRecipeBook.Domain.Entities.Recipe _recipe = default!;
        private string _password = string.Empty;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                    if(descriptor is not null)
                        services.Remove(descriptor);

                    var provider = services
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                    using var scope = services.BuildServiceProvider().CreateScope();

                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    dbContext.Database.EnsureDeleted();

                    StartDatabase(dbContext);
                });
        }

        public string GetEmail() => _user.Email;
        public Guid GetUserIdentifier() => _user.UserIdentifier;
        public string GetPassword() => _password;
        public string GetName() => _user.Name;

        public string GetRecipeTitle() => _recipe.Title;
        public string GetRecipeId() => IdEncripterBuilder.Build().Encode(_recipe.Id);
        public CookingTime GetRecipeCookingTime() => _recipe.CookingTime!.Value;
        public Difficulty GetRecipeDifficulty() => _recipe.Difficulty!.Value;
        public IList<DishType> GetDishType() => _recipe.DishTypes.Select(c => c.Type).ToList();
        

        private void StartDatabase(AppDbContext dbContext)
        {
            (_user,_password) = UserBuilder.Build();
            _recipe = RecipeBuilder.Build(_user);

            dbContext.Users.Add(_user);

            dbContext.Recipes.Add(_recipe);

            dbContext.SaveChanges();
        }
    }
}
