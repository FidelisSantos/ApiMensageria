using ApiMensageria.Data;
using ApiMensageria.Interfaces;
using ApiMensageria.Model;
using ApiMensageria.validator;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Services
{
  public class LoginServices : ILoginServices
  {
    private readonly DataContext _context;
    private LoginValidator validator = new LoginValidator();

    public LoginServices(DataContext context)
    {
      _context = context;
    }

    public LoginModel Find(string Email, string Password)
    {
      return _context.UsersLogin.FirstOrDefault(l => l.Email == Email && l.Password == Password);
    }

    public LoginModel Update(int UserModelId, LoginModel updateUser)
    {
      var busca = _context.UsersLogin.FirstOrDefault(l => l.UserModelId == UserModelId);
      if (busca != null)
      {
        if (busca.Email == updateUser.Email || !_context.UsersLogin.Any(l => l.Email == updateUser.Email))
        {
          Validate(updateUser);
          busca.Email = updateUser.Email;
          busca.Password = updateUser.Password;
          _context.SaveChanges();
          return _context.UsersLogin.Include(l => l.User).FirstOrDefault(l => l.UserModelId == UserModelId);
        }
        return null;
      }
      return null;
    }

    private void Validate(LoginModel model)
    {
      var validationResult = validator.Validate(model);
      if (!validationResult.IsValid)
      {
        var erros = validationResult.Errors.Select(x => x.ErrorMessage);
        string erroFormatter = string.Join(" ", erros);
        throw new Exception(erroFormatter);
      }
    }
  }
}