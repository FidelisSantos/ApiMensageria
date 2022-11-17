using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface ILoginRepository
  {
    Task<LoginModel> Find(string Email, string Password);
    Task<LoginModel> Update(LoginModel loginUpdate, LoginModel updateUser);
    Task<LoginModel> FindUserLogin(int UserModelId);
    Task<bool> Exists(string email);
    Task Delete(LoginModel loginDelete);
    Task<List<LoginModel>> FindAll();
    Task<LoginModel> Created(LoginModel login);
  }
}