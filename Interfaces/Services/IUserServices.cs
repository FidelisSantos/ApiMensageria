using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface IUserServices
  {
    Task<List<UserModel>> FindAll();
    Task<UserModel> Find(int UserModelId);
    Task<UserModel> Create(UserModel newUser, LoginModel Login);
    Task<UserModel> Update(int UserModelId, UserModel updateUser);
    Task Delete(int UserModelId);
  }
}