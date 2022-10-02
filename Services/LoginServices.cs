using ApiMensageria.Data;
using ApiMensageria.Interfaces;
using ApiMensageria.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly DataContext _context;

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
                    busca.Email = updateUser.Email;
                    busca.Password = updateUser.Password;
                    _context.SaveChanges();
                    return _context.UsersLogin.Include(l => l.User).FirstOrDefault(l => l.UserModelId == UserModelId);
                }
                return null;
        }
    }
}