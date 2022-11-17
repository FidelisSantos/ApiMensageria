using System.Net;
using ApiMensageria.Data;
using ApiMensageria.Interfaces;
using ApiMensageria.Model;
using ApiMensageria.validator;

namespace ApiMensageria.Services
{
  public class LoginServices : ILoginServices
  {
    private readonly ILoginRepository loginRepository;
    private LoginValidator validator = new LoginValidator();

    public LoginServices(DataContext context, ILoginRepository loginRepository)
    {
      this.loginRepository = loginRepository;
    }

    public async Task<LoginModel> Find(string Email, string Password)
    {
      try
      {
        var login = await loginRepository.Find(Email, Password);
        if (login == null) throw new HttpRequestException("Login ou Senha errados", null, HttpStatusCode.InternalServerError);
        return login;
      }
      catch
      {
        throw new HttpRequestException("Erro ao logar", null, HttpStatusCode.InternalServerError);
      }

    }

    public async Task<LoginModel> Update(int UserModelId, LoginModel updateUser)
    {
      try
      {
        var busca = await loginRepository.FindUserLogin(UserModelId);
        if (busca == null || busca.Email != updateUser.Email || await loginRepository.Exists(updateUser.Email)) return null;
        Validate(updateUser);
        return await loginRepository.Update(busca, updateUser);
      }
      catch
      {
        throw new HttpRequestException("Erro ao Atualizar", null, HttpStatusCode.InternalServerError);
      }


    }

    public async Task<List<LoginModel>> FindAll()
    {
      try
      {
        return await loginRepository.FindAll();
      }
      catch
      {
        throw new HttpRequestException("Erro ao Listar", null, HttpStatusCode.InternalServerError);
      }
    }

    private void Validate(LoginModel model)
    {
      var validationResult = validator.Validate(model);
      if (validationResult.IsValid) return;

      var erros = validationResult.Errors.Select(x => x.ErrorMessage);
      string erroFormatter = string.Join(" ", erros);
      throw new HttpRequestException(erroFormatter, null, HttpStatusCode.BadRequest);

    }
  }
}