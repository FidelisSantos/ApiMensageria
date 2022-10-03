using ApiMensageria.Data;
using ApiMensageria.Interfaces;
using ApiMensageria.Model;
using ApiMensageria.validator;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Services
{
  public class UserServices : IUserServices
  {
    private readonly DataContext _context;

    private UserValidator validator = new UserValidator();
    private UserUpdateValidator validatorUpdate = new UserUpdateValidator();

    public UserServices(DataContext context)
    {
      _context = context;
    }

    public UserModel Create(UserModel newUser)
    {
      if (_context.Users.Any(u => u.Login.Email == newUser.Login.Email))
      {
        return null;
      }
      Validate(newUser);
      newUser.Created = DateTime.Now;
      _context.Users.Add(newUser);
      _context.SaveChanges();
      return newUser;

    }

    public bool Delete(int UserModelId)
    {
      var UserDelete = _context.Users.FirstOrDefault(U => U.UserModelId == UserModelId);
      if (UserDelete != null)
      {
        _context.UsersLogin.Remove(_context.UsersLogin.FirstOrDefault(l => l.UserModelId == UserDelete.UserModelId));
        _context.Users.Remove(UserDelete);
        _context.SaveChanges();
        return !_context.Users.Any(U => U.UserModelId == UserDelete.UserModelId);
      }
      return false;
    }

    public UserModel Find(int UserModelId)
    {
      return _context.Users.Include(U => U.Messages).FirstOrDefault(U => U.UserModelId == UserModelId);
    }

    public List<UserModel> FindAll()
    {
      return _context.Users.Include(u => u.Messages).ToList();
    }

    public UserModel Update(int UserModelId, UserModel updateUser)
    {
      var atualizar = _context.Users.FirstOrDefault(U => U.UserModelId == UserModelId);
      if (atualizar != null)
      {
        ValidateUpdate(updateUser);
        atualizar.Name = updateUser.Name;
        atualizar.Genre = updateUser.Genre;
        _context.SaveChanges();
        return (updateUser);
      }
      return null;
    }
    private void Validate(UserModel model)
    {
      var validationResult = validator.Validate(model);
      if (!validationResult.IsValid)
      {
        var erros = validationResult.Errors.Select(x => x.ErrorMessage);
        string erroFormatter = string.Join(" ", erros);
        throw new Exception(erroFormatter);
      }
    }

    private void ValidateUpdate(UserModel model)
    {
      var validationResult = validatorUpdate.Validate(model);
      if (!validationResult.IsValid)
      {
        var erros = validationResult.Errors.Select(x => x.ErrorMessage);
        string erroFormatter = string.Join(" ", erros);
        throw new Exception(erroFormatter);
      }
    }
  }
}