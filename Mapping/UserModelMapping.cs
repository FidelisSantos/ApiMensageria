using ApiMensageria.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMensageria.Mapping
{
  public class UserModelMapping : IEntityTypeConfiguration<UserModel>
  {
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
      builder.ToTable("users");
      builder.HasKey(u => u.UserModelId);

      builder.Property(u => u.Genre)
            .HasMaxLength(1)
            .IsRequired();

      builder.Property(u => u.Name)
            .IsRequired();

      builder.Property(u => u.Created)
            .HasMaxLength(100)
            .IsRequired();


      builder.HasOne(l => l.Login)
            .WithOne(u => u.User);

      builder.HasMany(m => m.Messages)
            .WithOne(u => u.UserReceiver);
    }
  }
}