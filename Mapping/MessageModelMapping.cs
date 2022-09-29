using ApiMensageria.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiMensageria.Data
{
  public class MessageModelMapping : IEntityTypeConfiguration<MessageModel>
  {
    public void Configure(EntityTypeBuilder<MessageModel> builder)
    {
      builder.ToTable("messagens");

      builder.HasKey(m => m.MessageModelId);

      builder.Property(m => m.Message)
            .IsRequired();

      builder.Property(m => m.Sent)
            .IsRequired();

      builder.Property(m => m.UserReceiverId)
            .IsRequired();

      builder.HasOne(m => m.UserReceiver)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserReceiverId)
            .IsRequired();
    }
  }
}