using ApiMensageria.Data;
using ApiMensageria.Interfaces;
using ApiMensageria.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Repository
{
  public class LoginRepository : ILoginRepository
  {
    private readonly DataContext _context;

    public LoginRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<LoginModel> Created(LoginModel login)
    {
      await _context.UsersLogin.AddAsync(login);
      await _context.SaveChangesAsync();
      return _context.UsersLogin.FirstOrDefault(l => l.Email == login.Email);
    }
    public async Task<LoginModel> Find(string Email, string Password)
    {
      return await _context.UsersLogin.FirstOrDefaultAsync(l => l.Email == Email && l.Password == Password);
    }

    public async Task<LoginModel> FindUserLogin(int UserModelId)
    {
      return await _context.UsersLogin.FirstOrDefaultAsync(l => l.UserModelId == UserModelId);
    }

    public async Task<bool> Exists(string email)
    {
      return await _context.UsersLogin.AnyAsync(l => l.Email == email);
    }

    public async Task<LoginModel> Update(LoginModel loginUpdate, LoginModel updateUser)
    {
      loginUpdate.Email = updateUser.Email;
      loginUpdate.Password = updateUser.Password;
      await _context.SaveChangesAsync();
      return await _context.UsersLogin.Include(l => l.User).FirstOrDefaultAsync(l => l.UserModelId == loginUpdate.UserModelId);
    }

    public async Task Delete(LoginModel loginDelete)
    {
      _context.UsersLogin.Remove(loginDelete);
      await _context.SaveChangesAsync();

    }

    public async Task<List<LoginModel>> FindAll()
    {
      return await _context.UsersLogin.ToListAsync();
    }
  }
}