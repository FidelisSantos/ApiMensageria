using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface IUserServices
  {
    List<UserModel> FindAll();
    UserModel Find(int UserModelId);
    UserModel Create(UserModel newUser);
    UserModel Update(int UserModelId, UserModel updateUser);
    bool Delete(int UserModelId);
  }
}