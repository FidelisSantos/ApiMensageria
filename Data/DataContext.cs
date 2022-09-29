using ApiMensageria.Mapping;
using ApiMensageria.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder mb)
    {
      base.OnModelCreating(mb);

      mb.ApplyConfiguration(new UserModelMapping());
      mb.ApplyConfiguration(new LoginModelMapping());
      mb.ApplyConfiguration(new MessageModelMapping());
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<LoginModel> UsersLogin { get; set; }
    public DbSet<MessageModel> UsersMessage { get; set; }
  }
}