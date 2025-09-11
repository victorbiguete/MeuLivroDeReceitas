using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Application.UseCases.User.Profile;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Application.UseCases.User.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddUseCases(services);
            AddAutoMapper(services);
            
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase,DoLoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase,GetUserProfileUseCase>();
            services.AddScoped<IUpdateUserUseCase,UpdateUserUseCase>();
            services.AddScoped<IChangePasswordUseCase,ChangePasswordUseCase>();
        }


        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new Services.AutoMapper.AutoMapping());
            }).CreateMapper());
        }
    }
}
