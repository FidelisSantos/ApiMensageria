using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface IUserRepository
  {
    Task<List<UserModel>> FindAll();
    Task<UserModel> Find(int UserModelId);
    Task<UserModel> Create(UserModel newUser);
    Task<UserModel> Update(UserModel userUpdate, UserModel updateUser);
    Task Delete(UserModel userDelete);
    Task<bool> Exists(string email);
  }
}