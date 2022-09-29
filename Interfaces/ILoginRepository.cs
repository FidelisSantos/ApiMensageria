
using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface ILoginRepository
  {
    LoginModel Find(string Email);
    LoginModel Create(LoginModel newUser);
    LoginModel Update(string Email, LoginModel updateUser);
    bool Delete(string Email);
  }
}