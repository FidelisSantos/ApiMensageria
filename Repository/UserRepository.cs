using ApiMensageria.Data;
using ApiMensageria.Interfaces;
using ApiMensageria.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Repository
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    public UserRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<UserModel> Create(UserModel newUser)
    {
      newUser.Created = DateTime.Now;
      await _context.Users.AddAsync(newUser);
      await _context.SaveChangesAsync();
      return newUser;
    }

    public async Task Delete(UserModel userDelete)
    {
      _context.Users.Remove(userDelete);
      await _context.SaveChangesAsync();
    }

    public async Task<UserModel> Find(int UserModelId)
    {
      return await _context.Users.FirstOrDefaultAsync(U => U.UserModelId == UserModelId);
    }

    public async Task<List<UserModel>> FindAll()
    {
      return await _context.Users.Include(u => u.Messages).ToListAsync();
    }

    public async Task<UserModel> Update(UserModel userUpdate, UserModel updateUser)
    {
      userUpdate.Name = updateUser.Name;
      userUpdate.Genre = updateUser.Genre;
      await _context.SaveChangesAsync();
      return userUpdate;
    }

    public async Task<bool> Exists(string email)
    {
      return await _context.Users.Include(x => x.Login).AnyAsync(u => u.Login.Email == email);
    }
  }
}