using ApiMensageria.Data;
using ApiMensageria.Interfaces;
using ApiMensageria.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Services
{
  public class MessageServices : IMessageServices
  {
    private readonly DataContext _context;
    public MessageServices(DataContext context)
    {
      _context = context;
    }

    public bool Create(int UserIssuerId, int UserReceiverId, MessageModel newMessage)
    {
      if (UserIssuerId != UserReceiverId)
      {
        var emissor = _context.Users.Find(UserIssuerId);
        var receptor = _context.Users.Find(UserReceiverId);
        if (emissor != null && receptor != null && newMessage.Message != "")
        {
          newMessage.Sent = DateTime.Now;
          newMessage.UserIssuerId = UserIssuerId;
          newMessage.UserIssuer = emissor;
          newMessage.UserReceiverId = UserReceiverId;
          newMessage.UserReceiver = receptor;
          _context.UsersMessage.Add(newMessage);
          _context.SaveChanges();
          return true;
        }
        return false;
      }
      return false;
    }

    public bool Delete(int MessageModelId)
    {
      var message = _context.UsersMessage.Find(MessageModelId);
      if (message != null)
      {
        _context.UsersMessage.Remove(message);
        _context.SaveChanges();
        return !_context.UsersMessage.Any(u => u.MessageModelId == MessageModelId);
      }
      return false;
    }

    public MessageModel Find(int UserIssuerId) => _context.UsersMessage.Include(m => m.UserReceiver).Include(m => m.UserIssuer).FirstOrDefault(u => u.UserIssuerId == UserIssuerId);

    public List<MessageModel> Findall() => _context.UsersMessage.Include(m => m.UserReceiver).Include(m => m.UserIssuer).ToList();

    public bool Update(int MessageModelId, MessageModel updateMessage)
    {
      var message = _context.UsersMessage.Find(MessageModelId);
      if (message != null && updateMessage.Message != "")
      {
        message.Message = updateMessage.Message;
        _context.SaveChanges();
        return true;
      }
      return false;
    }
  }
}