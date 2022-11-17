using ApiMensageria.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMensageria.Mapping
{
  class LoginModelMapping : IEntityTypeConfiguration<LoginModel>
  {
    public void Configure(EntityTypeBuilder<LoginModel> builder)
    {
      builder.ToTable("login");

      builder.HasKey(l => l.LoginModelId);

      builder.Property(l => l.Email)
            .IsRequired()
            .HasMaxLength(30);

      builder.Property(l => l.Password)
            .IsRequired();

      builder.HasOne(u => u.User)
            .WithOne(l => l.Login)
            .HasForeignKey<LoginModel>(l => l.UserModelId);

      builder.Property(l => l.UserModelId)
            .IsRequired();
    }
  }
}