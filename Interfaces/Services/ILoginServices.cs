using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface ILoginServices
  {
    Task<LoginModel> Find(string Email, string Password);
    Task<LoginModel> Update(int UserModelId, LoginModel updateUser);
    Task<List<LoginModel>> FindAll();
  }
}