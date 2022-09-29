using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface IUserServices
  {
    List<UserModel> FindAll();
    UserModel Find(string Email);
    UserModel Create(UserModel newUser);
    UserModel Update(string Email, UserModel updateUser);
    bool Delete(string Email);
  }
}