using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            //Validar Request

            //Mappear a Request em Entidade

            //Criptografar a Senha

            //Salvar no banco

            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
        }
    }
}
