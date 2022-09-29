using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface ILoginServices
  {
    LoginModel Find(string Email);
    LoginModel Create(LoginModel newUser);
    LoginModel Update(string Email, LoginModel updateUser);
    bool Delete(string Email);
  }
}