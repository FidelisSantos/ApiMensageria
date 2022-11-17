using System.Net;
using ApiMensageria.Interfaces;
using ApiMensageria.Model;
using ApiMensageria.validator;

namespace ApiMensageria.Services
{
  public class UserServices : IUserServices
  {
    private readonly IUserRepository userRepository;
    private readonly ILoginRepository loginRepository;
    private UserValidator validator = new UserValidator();
    private UserUpdateValidator validatorUpdate = new UserUpdateValidator();

    public UserServices(IUserRepository userRepository, ILoginRepository loginRepository)
    {
      this.userRepository = userRepository;
      this.loginRepository = loginRepository;
    }

    public async Task<UserModel> Create(UserModel newUser, LoginModel newLogin)
    {
      if (await loginRepository.Exists(newUser.Login.Email)) throw new HttpRequestException("Email já existe", null, HttpStatusCode.BadRequest);
      Validate(newUser);
      var user = await userRepository.Create(newUser);
      newLogin.UserModelId = user.UserModelId;
      newLogin.User = user;
      var login = await loginRepository.Created(newLogin);

      return user;
    }

    public async Task Delete(int UserModelId)
    {
      var userDelete = await userRepository.Find(UserModelId);
      if (userDelete == null) throw new HttpRequestException("Usuário não existe", null, HttpStatusCode.NotFound);
      var loginDelete = await loginRepository.FindUserLogin(UserModelId);
      if (loginDelete != null) await loginRepository.Delete(loginDelete);
      await userRepository.Delete(userDelete);
    }

    public async Task<UserModel> Find(int UserModelId)
    {

      var user = await userRepository.Find(UserModelId);
      if (user == null) throw new HttpRequestException("Usuário não encontrado", null, HttpStatusCode.NotFound);

      return user;
    }

    public async Task<List<UserModel>> FindAll()
    {
      return await userRepository.FindAll(); ;
    }

    public async Task<UserModel> Update(int UserModelId, UserModel updateUser)
    {
      var atualizar = await userRepository.Find(UserModelId);
      if (atualizar == null) throw new HttpRequestException("Usuário não encontrado", null, HttpStatusCode.NotFound);
      ValidateUpdate(updateUser);
      return await userRepository.Update(atualizar, updateUser);
    }
    private void Validate(UserModel model)
    {
      var validationResult = validator.Validate(model);
      if (validationResult.IsValid) return;

      var erros = validationResult.Errors.Select(x => x.ErrorMessage);
      string erroFormatter = string.Join(" ", erros);
      throw new HttpRequestException(erroFormatter, null, HttpStatusCode.BadRequest);

    }

    private void ValidateUpdate(UserModel model)
    {
      var validationResult = validatorUpdate.Validate(model);
      if (validationResult.IsValid) return;

      var erros = validationResult.Errors.Select(x => x.ErrorMessage);
      string erroFormatter = string.Join(" ", erros);
      throw new HttpRequestException(erroFormatter, null, HttpStatusCode.BadRequest);

    }
  }
}