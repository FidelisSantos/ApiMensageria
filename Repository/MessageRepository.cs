using ApiMensageria.Data;
using ApiMensageria.Interfaces;
using ApiMensageria.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Repository
{
  public class MessageRepository : IMessageRepository
  {
    private readonly DataContext _context;
    public MessageRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<MessageModel> Create(UserModel emissor, UserModel receptor, MessageModel newMessage)
    {
      newMessage.Sent = DateTime.Now;
      newMessage.UserIssuerId = emissor.UserModelId;
      newMessage.UserIssuer = emissor;
      newMessage.UserReceiverId = receptor.UserModelId;
      newMessage.UserReceiver = receptor;
      await _context.UsersMessage.AddAsync(newMessage);
      await _context.SaveChangesAsync();
      return newMessage;
    }

    public async Task<bool> Delete(MessageModel message)
    {
      _context.UsersMessage.Remove(message);
      await _context.SaveChangesAsync();
      return !await _context.UsersMessage.AnyAsync(u => u.MessageModelId == message.MessageModelId);
    }

    public async Task<MessageModel> Find(int MessageModelId) => await _context.UsersMessage.Include(m => m.UserReceiver).Include(m => m.UserIssuer).FirstOrDefaultAsync(m => m.MessageModelId == MessageModelId);

    public async Task<List<MessageModel>> FindMessageReceived(int UserReceiverId) => await _context.UsersMessage.Include(m => m.UserIssuer).Where(m => m.UserReceiverId == UserReceiverId).ToListAsync();

    public Task<List<MessageModel>> Findall() => _context.UsersMessage.Include(m => m.UserReceiver).Include(m => m.UserIssuer).ToListAsync();

    public async Task<MessageModel> Update(MessageModel message, MessageModel updateMessage)
    {
      message.Message = updateMessage.Message;
      await _context.SaveChangesAsync();
      return updateMessage;
    }
  }
}