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

      var login = await loginRepository.Find(Email, Password);
      if (login == null) throw new HttpRequestException("Login ou Senha errados", null, HttpStatusCode.InternalServerError);
      return login;
    }

    public async Task<LoginModel> Update(int UserModelId, LoginModel updateLogin)
    {

      var busca = await loginRepository.FindUserLogin(UserModelId);
      Console.Write("entrei aqui");
      if (busca == null || busca.Email != updateLogin.Email || await loginRepository.Exists(updateLogin.Email)) return null;
      Console.Write("entrei");
      Validate(updateLogin);
      var result = await loginRepository.Update(busca, updateLogin);
      Console.Write(result);
      return result;

    }

    public async Task<List<LoginModel>> FindAll()
    {
      return await loginRepository.FindAll();
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