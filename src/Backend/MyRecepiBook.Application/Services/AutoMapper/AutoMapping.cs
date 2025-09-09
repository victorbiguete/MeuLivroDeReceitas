using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Entities;

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


        }

        private void DomainToResponse()
        {
            CreateMap<User, ResponseUserProfileJson>();
        }
    }
}
