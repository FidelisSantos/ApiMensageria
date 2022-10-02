using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface ILoginServices
  {
    LoginModel Find(string Email, string Password);
    LoginModel Update(int UserModelId, LoginModel updateUser);
  }
}