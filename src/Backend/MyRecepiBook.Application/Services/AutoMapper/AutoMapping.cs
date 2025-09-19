using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Enum;

namespace MyRecipeBook.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<RequestRecipeJson, Recipe>()
                .ForMember(dest => dest.Instructions, opt => opt.Ignore())
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(source => source.Ingredients.Distinct()))
                .ForMember(dest => dest.DishTypes, opt => opt.MapFrom(source => source.DishTypes.Distinct()));

            CreateMap<string,Ingredient>()
                .ForMember(dest => dest.Item, opt => opt.MapFrom(source => source));

            CreateMap<Communication.Enum.DishType,Domain.Entities.DishType>()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(source => source));

            CreateMap<RequestInstructionJson, Instruction>();
        }

        private void DomainToResponse()
        {
            CreateMap<User, ResponseUserProfileJson>();
        }
    }
}
